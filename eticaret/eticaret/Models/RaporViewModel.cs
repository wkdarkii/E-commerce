namespace eticaret.Models
{
    public class RaporViewModel
    {
        public string SiparisNo { get; set; }          // Sipariş numarası
        public string SiparisDurumu { get; set; }   // Sipariş durumu (onaylandı vs.)
        public string SiparisTarihi { get; set; }   // Sipariş tarihi
        public string Fiyat { get; set; }           // Sipariş fiyatı
    }

}
