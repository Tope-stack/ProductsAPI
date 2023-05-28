using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProduct([FromRoute] int id) 
        {
            var product = _context.Products.Find(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var newProduct = new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Amount = product.Amount,
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return Ok(newProduct);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult EditProduct(int id, Product product) 
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Description = product.Description;
                existingProduct.Amount = product.Amount;

                _context.SaveChanges();

                return Ok(existingProduct);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChanges();
                return Ok("Sucessfully deleted product");
            }

            return NotFound();
        }

    }
}
