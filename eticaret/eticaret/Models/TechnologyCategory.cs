using System.ComponentModel.DataAnnotations;
using eticaret.Controllers;
namespace eticaret.Models
{
    public class TechnologyCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        // Bir teknoloji kategorisi birçok ürüne sahip olabilir
        public ICollection<ProductController> Products { get; set; }
    }
}
