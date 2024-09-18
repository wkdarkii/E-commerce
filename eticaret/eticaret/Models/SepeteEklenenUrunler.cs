namespace eticaret.Models
{
    public class SepeteEklenenUrunler
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public DateTime? satın_alma_zamani { get; set; }

        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }


}
