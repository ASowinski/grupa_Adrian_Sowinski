using Microsoft.EntityFrameworkCore;
using EbookShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EbookShop.DataAccess
{
    public class EbookShopContext : IdentityDbContext<AppUser>
    {
        
        
        public EbookShopContext(DbContextOptions<EbookShopContext> options) : base(options){}

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ebook> Ebooks { get; set; }
        public DbSet<AuthorEbooks> AuthorEbooks { get; set; }
        public DbSet<FilePath> Files { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EbookCategories> EbookCategories { get; set; }
        public DbSet<Order> Orders { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // define composite key for the junction table 
            modelBuilder.Entity<AuthorEbooks>()
                .HasKey(ae => new { ae.AuthorId, ae.EbookId });

            modelBuilder.Entity<EbookCategories>()
                .HasKey(ec => new { ec.CategoryId, ec.EbookId });

        }

    }
}
