using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/cartItems")]
    [ApiController]
    public class CartItemController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CartItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CartItem>> AddCartItem([FromBody] CartItem cartItem)
        {
            var user = await _context.Users.FirstOrDefaultAsync(us => us.UserId == cartItem.UserId);
            if (user == null)
            {
                return BadRequest("userId not found");
            }

            var product =await  _context.Product.FirstOrDefaultAsync(pr => pr.ProductId == cartItem.ProductId);
            if (product == null)
            {
                return BadRequest("productId not found");
            }

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == cartItem.UserId && ci.ProductId == cartItem.ProductId);

            if (existingCartItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                existingCartItem.Quantity += cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
            }

            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.CartItemId }, cartItem);
        }

        [HttpDelete]
        public async Task<ActionResult<CartItem>> DeleteCartItem(CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == cartItem.UserId && ci.ProductId == cartItem.ProductId);

            if (existingCartItem == null)
            {
                return NotFound();
            }

            if (cartItem.Quantity >= existingCartItem.Quantity)
            {
                _context.CartItems.Remove(existingCartItem);
            }
            else
            {
                existingCartItem.Quantity -= cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
