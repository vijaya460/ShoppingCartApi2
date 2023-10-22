using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingCartAPI.Models
{
    public class User
    {

        [Key]
        [JsonIgnore]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

    }

    
}
