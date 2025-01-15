using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Models
{
    public class AddProductRequest
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 500)]
        public int StockQuantity { get; set; }
    }
}
