using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Cart>> GetUserCart(int userId)
        {
            var users = await (from ai in _context.CartItems
                               join us in _context.Users on ai.UserId equals us.UserId
                               where (ai.UserId == userId)
                               select new Cart
                               {
                                   User = us
                               }).FirstOrDefaultAsync();

            if (users == null)
            {
                return NotFound();
            }

            var products = await (from ci in _context.CartItems
                                  join pr in _context.Product on ci.ProductId equals pr.ProductId
                                  select new Product
                                  {
                                      Name = pr.Name,
                                      Price = pr.Price,
                                      InStock = pr.InStock,
                                  }).ToListAsync();

            users.Products = products;




            return Ok(users);
        }
    }
}
