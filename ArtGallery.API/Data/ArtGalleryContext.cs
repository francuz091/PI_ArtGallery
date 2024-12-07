using ArtGallery.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.API.Data
{
    public class ArtGalleryContext : DbContext
    {
        public ArtGalleryContext(DbContextOptions<ArtGalleryContext> options)
            : base(options)
        {
        }

        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ArtWorkType> ArtWorkTypes { get; set; }
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Password).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<Artwork>()
                .Property(a => a.Title).HasMaxLength(50);
            modelBuilder.Entity<Artwork>()
                .Property(a => a.Description).HasMaxLength(150);
            modelBuilder.Entity<Artwork>()
                .Property(a => a.Price).HasColumnType("money");

            modelBuilder.Entity<RoleType>()
                .Property(rt => rt.Type).HasMaxLength(50);

            modelBuilder.Entity<ArtWorkType>()
                .Property(at => at.Type).HasMaxLength(50);

            modelBuilder.Entity<PaymentType>()
                .Property(pt => pt.Type).HasMaxLength(50);

            // Definiranje relacija
            modelBuilder.Entity<User>()
                .HasOne<RoleType>()
                .WithMany()
                .HasForeignKey(u => u.RoleTypeID);

            modelBuilder.Entity<Artwork>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artwork>()
                .HasOne<ArtWorkType>()
                .WithMany()
                .HasForeignKey(a => a.ArtWorkTypeID);

            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne<PaymentType>()
                .WithMany()
                .HasForeignKey(o => o.PaymentTypeID);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(oi => oi.OrderID);

            modelBuilder.Entity<OrderItem>()
                .HasOne<Artwork>()
                .WithMany()
                .HasForeignKey(oi => oi.ArtWorkID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
