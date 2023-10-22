using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingCartAPI.Models
{

    public class CartItem
    {

        [Key]
        [JsonIgnore]
        public int CartItemId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }
        public int Quantity { get; set; }
        

    }
}
