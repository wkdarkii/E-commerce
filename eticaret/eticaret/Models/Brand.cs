using eticaret.Controllers;
using System.ComponentModel.DataAnnotations;

namespace eticaret.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Bir marka birçok ürüne sahip olabilir
        public ICollection<ProductController> Products { get; set; }
    }
}
