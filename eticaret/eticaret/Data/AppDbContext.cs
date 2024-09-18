using eticaret.Models;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace eticaret.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Sadece User tablosunu kullanmak için bu DbSet'i bırakıyoruz
        public DbSet<User> Users { get; set; }
        public DbSet<giris_yapildi> Giris_Yapildi { get; set; }
        public DbSet<categories> Categories { get; set; } // Büyük harfle başlayabilir
        public DbSet<Product> Products { get; set; } // Büyük harfle başlayabilir Satin_Alinanlar
        public DbSet<Satin_Alinanlar> Satin_Alinanlar { get; set; }
        public DbSet<SepeteEklenenUrunler> SepeteEklenenUrun { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<sifirlama_kodlari> SifirlamaKodlari { get; set; }
        public DbSet<usercards> UserCards { get; set; }
        public DbSet<ToplamPara> ToplamParalar { get; set; }
        public DbSet<UrunYorumlari> UrunYorumlari { get; set; }
        public DbSet<Kullanicininsattig> KullanicininSattiklari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrunYorumlari>()
          .HasOne(uy => uy.User)
          .WithMany(u => u.UrunYorumlari)
          .HasForeignKey(uy => uy.UserId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UrunYorumlari>()
                .HasOne(uy => uy.Product)
                .WithMany(p => p.UrunYorumlari)
                .HasForeignKey(uy => uy.ProductID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ToplamPara>().ToTable("toplamparalar");

            modelBuilder.Entity<usercards>()
           .HasOne(uc => uc.User)
           .WithMany(u => u.UserCards)
           .HasForeignKey(uc => uc.UserId);

            // Seller ile User arasındaki ilişkiyi tanımlıyoruz.
            modelBuilder.Entity<Seller>()
                .HasOne(s => s.User)             // Bir Seller'ın bir User'ı vardır.
                .WithMany(u => u.Sellers)        // Bir User'ın birden fazla Seller'ı olabilir.
                .HasForeignKey(s => s.UserId)    // Seller tablosundaki UserId, foreign key olarak tanımlanır.
                .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde bağlı Seller kayıtları da silinsin.

            modelBuilder.Entity<Seller>()
                .Property(s => s.UrunKategori)
                .HasConversion<string>();        // ENUM türünü string olarak saklayacak.

            // Diğer model konfigürasyonları burada yapılabilir.



            modelBuilder.Entity<Siparis>(entity =>
            {

              


                entity.ToTable("siparisler");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.SiparisTarihi).HasColumnName("siparis_tarihi");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Siparisler)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Siparisler)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<SepeteEklenenUrunler>(entity =>
            {

               



                entity.ToTable("sepete_eklenen_urunler");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.user_id).HasColumnName("user_id");
                entity.Property(e => e.product_id).HasColumnName("product_id");
                entity.Property(e => e.satın_alma_zamani).HasColumnName("satın_alma_zamani");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SepeteEklenenUrunler) // User modelinde koleksiyon ismi
                    .HasForeignKey(d => d.user_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SepeteEklenenUrunler_User");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SepeteEklenenUrunler) // Product modelinde koleksiyon ismi
                    .HasForeignKey(d => d.product_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SepeteEklenenUrunler_Product");
            });


            // Category ve Product arasındaki ilişki
            modelBuilder.Entity<Product>()
                .HasOne(p => p.categories)
                .WithMany(c => c.products)
                .HasForeignKey(p => p.category_id);

            // CategoryID'nin Primary Key olduğunu belirtin
            modelBuilder.Entity<categories>()
                .HasKey(c => c.CategoryID);

            base.OnModelCreating(modelBuilder);



        }
     

        // Gereksiz olan tüm bağlantıları yorum satırına aldım

        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Brand> Brands { get; set; }

        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        //public DbSet<TopSellingProduct> TopSellingProducts { get; set; }
        //public DbSet<Earning> Earnings { get; set; }
        //public DbSet<Discount> Discounts { get; set; }
        //public DbSet<TechnologyCategory> TechnologyCategories { get; set; }
        //public DbSet<ProductBrand> ProductBrands { get; set; }
        //public DbSet<UserProduct> UserProducts { get; set; }
        //public DbSet<Main_Data> main_Datas { get; set; }



    }
}
