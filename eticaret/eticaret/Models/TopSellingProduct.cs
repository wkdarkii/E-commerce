using System.ComponentModel.DataAnnotations;
using eticaret.Controllers;
namespace eticaret.Models
{
    public class TopSellingProduct
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductController Product { get; set; }

        public int SalesCount { get; set; }

        public DateTime RecordDate { get; set; } = DateTime.Now;
    }
}
