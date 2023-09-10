using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShoppingCartAPI.Models
{
    public class Product
    {

        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public bool InStock { get; set; }


    }
}
