using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using api.DTO;
using api.Error;
using api.Extensions;
using AutoMapper;
using Domains.Entities;
using Domains.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [Authorize]
    public class AccountController : BaseController {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToken _tokenService;
        private readonly IMapper _mapper;
        public AccountController (UserManager<User> userManager, SignInManager<User> signInManager, IToken tokenService, IMapper mapper) {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<ReturnUserDTO>> GetCurrentUser () {
            var user = await _userManager.FindByEmailFromClaimsPrinciple (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));
            return new ReturnUserDTO {
                Email = user.Email,
                    Token = _tokenService.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }

        [HttpGet ("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress () {
            var user = await _userManager.FindByUserByClaimsPrincipleWithAddressAsync (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));
            var useraddress = _mapper.Map<Address, AddressDTO> (user.address);
            if (useraddress == null) {
                return Ok (new AddressDTO ());
            }
            return Ok (useraddress);
        }

        [HttpPut ("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress (AddressDTO address) {
            var user = await _userManager.FindByUserByClaimsPrincipleWithAddressAsync (HttpContext.User);
            if (user == null) return Unauthorized (new ErrorRes (401));
            user.address = _mapper.Map<AddressDTO, Address> (address);
            var result = await _userManager.UpdateAsync (user);
            if (result.Succeeded) return Ok ();
            return BadRequest ("Problem updating the user");
        }

        [AllowAnonymous]
        [HttpPost ("login")]
        public async Task<ActionResult<ReturnUserDTO>> Login (LoginDTO loginDto) {
            var user = await _userManager.FindByEmailAsync (loginDto.Email);

            if (user == null) return Unauthorized (new ErrorRes (401));

            var result = await _signInManager.CheckPasswordSignInAsync (user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized (new ErrorRes (401));

            return new ReturnUserDTO {
                Email = user.Email,
                    Token = _tokenService.CreateToken (user),
                    DisplayName = user.DisplayName
            };
        }

        [AllowAnonymous]
        [HttpGet ("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync ([FromQuery][RequiredAttribute] string email) {
            return await _userManager.FindByEmailAsync (email) != null;
        }

        [AllowAnonymous]
        [HttpPost ("register")]
        public async Task<ActionResult<ReturnUserDTO>> Register (RegisterDTO registerDto) {
            if (CheckEmailExistsAsync (registerDto.Email).Result.Value) {
                return new BadRequestObjectResult (new ErrorValidation { Errors = new [] { "Email address is already exists" } });
            }

            var user = new User {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync (user, registerDto.Password);

            if (!result.Succeeded) return BadRequest (new ErrorRes (400));

            return new ReturnUserDTO {
                DisplayName = user.DisplayName,
                    Token = _tokenService.CreateToken (user),
                    Email = user.Email
            };
        }
    }
}