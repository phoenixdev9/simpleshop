using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Domains.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {
        private readonly ILogger<ProductController> _logger;
        private readonly StoreContext _context;

        public ProductController (ILogger<ProductController> logger, StoreContext context) {
            _context = context;
            _logger = logger;

        }

        [HttpPost]
        public string CreateProduct () {
            _context.Products.Add (new Product { Name = "Hello" });
            _context.SaveChanges ();
            return "Good";
        }

        [HttpGet]
        public ICollection<Product> GetProduct () {
            return _context.Products.ToList ();

        }
    }
}