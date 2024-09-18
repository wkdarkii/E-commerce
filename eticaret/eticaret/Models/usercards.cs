namespace eticaret.Models
{
    public class usercards
    {
        public int id { get; set; }
        public int UserId { get; set; }  // users tablosuyla ilişkilendirilecek
        public long cartnumber { get; set; }
        public string cartholder { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }

        // İlişki tanımı
        public User User { get; set; }
    }
}
