using eticaret.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        // Bir siparişin birçok sipariş öğesi olabilir
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
