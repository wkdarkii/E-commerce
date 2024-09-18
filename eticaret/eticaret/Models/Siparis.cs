namespace eticaret.Models
{
    public class Siparis
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Siparişi veren kullanıcı
        public string? SiparisDurumu { get; set; }

        public int ProductId { get; set; } // Sipariş edilen ürün
        public bool? iptalet { get; set; }
        public DateTime SiparisTarihi { get; set; } // Sipariş tarihi


        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
     
    }

}
