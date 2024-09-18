using eticaret.Models; // Doğru ad alanını kullanın

namespace eticaret.Models
{
    public class categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        // Navigation property
        public ICollection<Product> products { get; set; }
    }
}
