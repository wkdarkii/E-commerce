using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Models
{
    public class Kullanicininsattig
    {

        public int Id { get; set; }

        // Ürün bilgileri
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklama { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunResim { get; set; }

        // Satıcı bilgileri
        public int SaticiId { get; set; }
        [ForeignKey("SaticiId")]
        public User Satici { get; set; }

        // Satın alan bilgileri
        public int SatinAlanId { get; set; }
        [ForeignKey("SatinAlanId")]
        public User SatinAlan { get; set; }
    }
}
