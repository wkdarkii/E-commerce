using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eticaret.Models
{
    public class sifirlama_kodlari
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public int sifirlama_rakami { get; set; }

        public virtual User User { get; set; }
    }
}
