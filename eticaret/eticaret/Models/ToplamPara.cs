namespace eticaret.Models
{
    public class ToplamPara
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign Key
        public DateTime alma_zamani { get; set; }
        public decimal Miktar { get; set; }
        // Navigation Property
        public User User { get; set; }
        
               
    }
}
