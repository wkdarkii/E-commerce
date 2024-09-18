using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Models
{
    public class User
    {
        public int id { get; set; }


        public string Kullanici_adi { get; set; }


        public string Sifre {  get; set; }
   
        public string Email { get; set; }
        public string? Profil_Foto { get; set; }


        public string? PhoneNumber { get; set; }
        public string? Adres { get; set; }
        public bool Role { get; set; }

        public bool Cinsiyet { get; set; }

        public DateTime D_Tarihi { get; set; }
        public int Giris_Sayisi { get; set; }
        public DateTime? Giris_Tarihi { get; set; }
        public bool Giris_Yapabilirmi { get; set; }
        public virtual ICollection<SepeteEklenenUrunler> SepeteEklenenUrunler { get; set; }
        public virtual ICollection<Siparis> Siparisler { get; set; }
        public ICollection<Seller> Sellers { get; set; }

        public ICollection<usercards> UserCards { get; set; }
        public ICollection<ToplamPara> ToplamParalar { get; set; }

        public ICollection<UrunYorumlari> UrunYorumlari { get; set; }


    }
}
