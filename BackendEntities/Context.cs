using BackendEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendEntities
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
            //Database.EnsureDeletedAsync();
            //Database.EnsureCreatedAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.Property(x => x.Name).HasColumnType("varchar");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Product");

                entity.HasIndex(x => x.ManufactureId, "IX_ManufactureId");
                entity.HasIndex(x => x.CategoryId, "IX_CategoryId");

                entity.HasOne(m => m.Manufacture).WithMany(p => p.Products).HasForeignKey(x => x.ManufactureId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(c => c.Category).WithMany(p => p.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PriceChange>(entity =>
            {
                entity.ToTable("PriceChanges");

                entity.Property(x => x.NewPrice).HasColumnType("double precision");
                //entity.Property(x => x.DataPriceChange).HasColumnType("datetime");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.PriceChange");

                entity.HasIndex(x => x.ProductId, "IX_ProductId");

                entity.HasOne(p => p.Product).WithMany(pc => pc.PriceChanges).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PurchaseItem>(entity =>
            {
                entity.ToTable("PurchaseItems");

                entity.Property(x => x.ProductPrice).HasColumnType("double precision");
                entity.Property(x => x.ProductCount).HasColumnType("integer");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.PurchaseItem");

                entity.HasIndex(x => x.ProductId, "IX_ProductId");
                entity.HasIndex(x => x.PurchaseId, "IX_PurchaseId");

                entity.HasOne(p => p.Product).WithMany(pi => pi.PurchaseItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Purchase).WithOne(pi => pi.PurchaseItems).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Deliverys");

                //entity.Property(x => x.DeliveryDate).HasColumnType("datetime");
                entity.Property(x => x.ProductCount).HasColumnType("integer");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Delivery");

                entity.HasIndex(x => x.ProductId, "IX_ProductId");
                entity.HasIndex(x => x.StoreId, "IX_StoreId");

                entity.HasOne(p => p.Product).WithMany(d => d.Deliveries).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Store).WithMany(d => d.Deliveries).HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchases");

                //entity.Property(x => x.PurshaseDate).HasColumnType("datetime");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Purchase");

                entity.HasIndex(x => x.CustomerId, "IX_CustomerId");
                entity.HasIndex(x => x.StoreId, "IX_StoreId");

                entity.HasOne(c => c.Customer).WithMany(p => p.Purchases).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.Store).WithMany(p => p.Purchases).HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Stores");

                entity.HasData(new Store { Id = Guid.NewGuid(), Name = "Филиал на Киевской" });

                entity.Property(x => x.Name).HasColumnType("varchar");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Store");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.Property(x => x.Name).HasColumnType("varchar");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Customer");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasData(new Category { Id = Guid.NewGuid(), Name = "Мелкая бытовая техника" });

                entity.Property(x => x.Name).HasColumnType("varchar");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Category");
            });

            modelBuilder.Entity<Manufacture>(entity =>
            {
                entity.ToTable("Manufacture");

                entity.HasData(new Manufacture { Id = Guid.NewGuid(), Name = "Bosh" });

                entity.Property(x => x.Name).HasColumnType("varchar");

                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id).HasName("PK_dbo.Manufacture");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}