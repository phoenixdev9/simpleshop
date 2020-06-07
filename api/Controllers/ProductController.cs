using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using AutoMapper;
using Domains.Entities;
using Domains.IRepo;
using Domains.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericsRepo<ProductType> _productTypes;
        private readonly IProductRepo _productRepo;
        private readonly IGenericsRepo<ProductBrand> _productBrand;
        private readonly IMapper _mapper;

        public ProductController(IGenericsRepo<ProductType> productTypes, IGenericsRepo<ProductBrand> productBrand, IProductRepo productRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productBrand = productBrand;
            _productRepo = productRepo;
            _productTypes = productTypes;

        }

        [HttpPost]
        public string CreateProduct()
        {
            return "Good";
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReturnProductDTO>>> GetProduct()
        {
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ReturnProductDTO>>(await _productRepo.GetProducts()));
        }

        [HttpGet("brand")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrand()
        {
            return Ok(await _productBrand.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<Product>>> GetTypes()
        {
            return Ok(await _productTypes.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ReturnProductDTO>> GetProductById(Guid Id)
        {
            return Ok(_mapper.Map<Product, ReturnProductDTO>(await _productRepo.GetProductById(Id)));
        }

    }
}