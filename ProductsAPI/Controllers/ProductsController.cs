using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.Data;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductsDbContext dbContext;

        public ProductsController(ProductsDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await dbContext.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product != null) 
            {
                return Ok(product);
            }
            return NotFound("Product not found");
        }

        [HttpPost]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> AddProduct(AddProductRequest addProductRequest)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = addProductRequest.Name,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, UpdateProductRequest updateProductRequest)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product != null)
            {
                product.Name = updateProductRequest.Name;
                product.Description = updateProductRequest.Description;
                product.Price = updateProductRequest.Price;
                product.LastModifiedDate = DateTime.Now;

                await dbContext.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound("Product not found");
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync();

                return Ok(product);
            }

            return NotFound("Product not found");
        }
    }
}
