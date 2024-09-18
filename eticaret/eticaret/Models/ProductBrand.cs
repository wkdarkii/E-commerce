using eticaret.Controllers;
using System.ComponentModel.DataAnnotations;

namespace eticaret.Models
{
    public class ProductBrand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Bir marka birçok ürüne sahip olabilir
        public ICollection<ProductController> Products { get; set; }
    }
}
