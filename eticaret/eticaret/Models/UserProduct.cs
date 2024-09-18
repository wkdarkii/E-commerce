using eticaret.Models;
using System.ComponentModel.DataAnnotations;
using eticaret.Controllers;
namespace eticaret.Models
{
    public class UserProduct
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductController Product { get; set; }

        public DateTime ListingDate { get; set; } = DateTime.Now;
    }
}
