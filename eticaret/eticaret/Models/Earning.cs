using eticaret.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eticaret.Controllers;

namespace eticaret.Models
{
    public class Earning
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public ProductController Product { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public DateTime EarnedAt { get; set; } = DateTime.Now;
    }
}
