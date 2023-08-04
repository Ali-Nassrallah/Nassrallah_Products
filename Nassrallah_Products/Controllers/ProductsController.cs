using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nassrallah_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{GetProductById}")]
        public async Task<Product> GetProductById(int id)
        {

            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            return product;
        }

        [HttpPost("{AddProduct}")]
        public async Task<Product> AddNewProduct(string Name, string Description, decimal Price, int Quantity)
        {
            var newProduct = await _context.Products.AddAsync(new Product(Name, Description, Price, Quantity));
            await _context.SaveChangesAsync();
            return newProduct.Entity;

        }
        [HttpPut("{UpdateProduct}")]
        public async Task<Product> UpdateProductAsync(int Id, string Name, string Description, decimal Price, int Quantity)
        {
            var forUpdate = await _context.Products.FirstOrDefaultAsync(i => i.Id == Id);
            throw new Exception("Not Found");
            forUpdate.Update(Name, Description, Price, Quantity);
            await _context.SaveChangesAsync();
            return forUpdate;
        }
        [HttpDelete("{DeleteProductById}")]
        public async Task DeleteProductById(int Id)
        {
            var forDelete = await _context.Products.FirstOrDefaultAsync(i => i.Id == Id);
            throw new Exception("Not found");
            _context.Remove(forDelete);
            await _context.SaveChangesAsync();
        }
    }
}
