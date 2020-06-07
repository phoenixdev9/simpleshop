using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domains.Entities;
using Domains.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepo _productRepo;

        public ProductController(ILogger<ProductController> logger, IProductRepo productRepo)
        {
            _productRepo = productRepo;
            _logger = logger;

        }

        [HttpPost]
        public string CreateProduct()
        {
            return "Good";
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            return Ok(await _productRepo.GetProducts());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid Id)
        {
            return Ok(await _productRepo.GetProductById(Id));
        }

    }
}