using WebDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebDb.Helpers
{
    public class ContextDb : DbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Delivery> Deliveries => Set<Delivery>();
        public DbSet<Manufacture> Manufactures => Set<Manufacture>();
        public DbSet<PriceChange> PriceChanges => Set<PriceChange>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();
        public DbSet<Store> Stores => Set<Store>();

        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id = Guid.NewGuid(), Name = "Мелкая бытовая техника" });
            //modelBuilder.Entity<Product>().HasData(new Product { Id = Guid.NewGuid(), Name = "холодильник" });
            modelBuilder.Entity<Manufacture>().HasData(new Manufacture { Id = Guid.NewGuid(), Name = "Bosh" });
            //modelBuilder.Entity<Customer>().HasData(new Customer { Id = Guid.NewGuid(), Name = "Филатова Ольга Петровна" });
            modelBuilder.Entity<Store>().HasData(new Store { Id = Guid.NewGuid(), Name = "Филиал на Киевской" });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(m => m.Manufacture).WithMany(p => p.Products).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Category).WithMany(p => p.Products).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany<PriceChange>().WithOne(p => p.Product);
                entity.HasMany<PurchaseItem>().WithOne(p => p.Product);
                entity.HasMany<Delivery>().WithOne(p => p.Product);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasOne(c => c.Customer).WithMany(p => p.Purchases).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.Store).WithMany(p => p.Purchases).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<PurchaseItem>().WithOne(p => p.Purchase);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasMany<Delivery>().WithOne(s => s.Store);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}