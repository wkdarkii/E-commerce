using eticaret.Models;
using System.ComponentModel.DataAnnotations;
using eticaret.Controllers;
namespace eticaret.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductController Product { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
