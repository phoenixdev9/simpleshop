using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTO;
using api.Utils;
using AutoMapper;
using Domains.Entities;
using Domains.IRepo;
using Domains.Params;
using Domains.Repo;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    public class ProductController : BaseController {
        private readonly IGenericsRepo<ProductType> _productTypes;
        private readonly IProductRepo _productRepo;
        private readonly IGenericsRepo<ProductBrand> _productBrand;
        private readonly IMapper _mapper;

        public ProductController (IGenericsRepo<ProductType> productTypes, IGenericsRepo<ProductBrand> productBrand, IProductRepo productRepo, IMapper mapper) {
            _mapper = mapper;
            _productBrand = productBrand;
            _productRepo = productRepo;
            _productTypes = productTypes;

        }

        [HttpPost]
        public string CreateProduct () {
            return "Good";
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ReturnProductDTO>>> GetProduct ([FromQuery] GetProductParams getProductParams) {
            var listProducts = await _productRepo.GetProducts (getProductParams);

            var returnList = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ReturnProductDTO>> (listProducts);
            Response.AddPagination (listProducts.CurrentPage, listProducts.PageSize, listProducts.TotalCount, listProducts.TotalPages);
            return Ok (returnList);
        }

        [HttpGet ("brand")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrand () {
            return Ok (await _productBrand.ListAllAsync ());
        }

        [HttpGet ("types")]
        public async Task<ActionResult<List<Product>>> GetTypes () {
            return Ok (await _productTypes.ListAllAsync ());
        }

        [HttpGet ("{Id}")]
        public async Task<ActionResult<ReturnProductDTO>> GetProductById (Guid Id) {
            var product = _mapper.Map<Product, ReturnProductDTO> (await _productRepo.GetProductById (Id));
            if (product == null) {
                return NotFound ();
            }
            return Ok (product);
        }

    }
}