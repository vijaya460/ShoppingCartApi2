
namespace ShoppingCartAPI.Models
{

    public class Cart
    {
        public Cart()
        {
            User = new User();

            Products = new List<Product>();
        }
        
        public  User User { get; set; }

        public  List<Product> Products { get; set; }
    }
}
