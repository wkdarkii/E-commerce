using Iyzipay.Model.V2.Subscription;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Models
{
    public class UrunYorumlari
    {

        public int id { get; set; }
        public int UserId { get; set; }
        public int ProductID { get; set; }
        public string Yorum { get; set; }
        public DateTime YorumTarihi { get; set; }

   
        // Foreign key relations
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }



    }
}
