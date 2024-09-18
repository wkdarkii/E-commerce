namespace eticaret.Models
{
    public class UserToplamParaViewModel
    {
        public int UserId { get; set; }
        public string KullaniciAdi { get; set; }
        public decimal ToplamMiktar { get; set; }
        public DateTime SonAlmaZamani { get; set; }
    }
}
