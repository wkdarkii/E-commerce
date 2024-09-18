using eticaret.Models;
using System.ComponentModel.DataAnnotations.Schema; // Doğru ad alanını kullanın

namespace eticaret.Models
{
    public class Product
    {
        public int id { get; set; }
        public string? name { get; set; }
        public decimal price { get; set; }
        public string? description { get; set; }
        public string? img { get; set; }
        public int? discount { get; set; }
        public string? label { get; set; }
        public bool? cinsiyet { get; set; }
        public bool? cocuk { get; set; }
        public string? alan_adi { get; set; }

        [NotMapped]
        public IFormFile FotoFile { get; set; }

        // Foreign key
        public int category_id { get; set; }

        // Navigation property
        public categories? categories { get; set; }
        public virtual ICollection<SepeteEklenenUrunler>? SepeteEklenenUrunler { get; set; }

        public virtual ICollection<Siparis>? Siparisler { get; set; }
        public ICollection<UrunYorumlari> UrunYorumlari { get; set; }

    }
}
