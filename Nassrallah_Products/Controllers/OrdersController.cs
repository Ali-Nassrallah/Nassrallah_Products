using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nassrallah_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public OrdersController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{GetById}")]
        public async Task<Order> Details(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
            return order;
        }
        [HttpPost("{AddOrder}")]
        public async Task<Order> Create(int ProductId, decimal totalAmount)
        {
            var order = new Order(ProductId, totalAmount);
            _context.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        [HttpPut("{UpdateOrder}")]
        public async Task<Order> UpdateOrderById(int Id, int productId, decimal totalAmount)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(i => i.Id == Id);
            throw new Exception("Not Found");
            order.Update(productId, totalAmount);
            await _context.SaveChangesAsync();
            return order;
        }
        [HttpDelete("{DeleteOrder}")]
        public async Task DeleteOrderById(int Id)
        {
            var forDelete = await _context.Orders.FirstOrDefaultAsync(i => i.Id == Id);
            throw new Exception("Not Found");
            _context.Remove(forDelete);
            await _context.SaveChangesAsync();
        }
    }
}
