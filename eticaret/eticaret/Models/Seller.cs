using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eticaret.Models
{
    public class Seller
    {
       
        public int Id { get; set; }       
        public int UserId { get; set; }
        public int? SatisaKoyulanUrun { get; set; }
        public string? UrunAdi { get; set; }
        public string? UrunAciklama { get; set; }
        public string? Onay_Asaması { get; set; }
        public string? UrunResim { get; set; }     
        public string? UrunKategori { get; set; } // ENUM type represented as string
        public int? Stok { get; set; }     
        public string? Sehir { get; set; } 
        public string? Ilce { get; set; }
        public int? UrunFiyat { get; set; }     
        public string? UrunDurumu { get; set; }     
        public string? Marka { get; set; }
        public User User { get; set; }  
    }


}
