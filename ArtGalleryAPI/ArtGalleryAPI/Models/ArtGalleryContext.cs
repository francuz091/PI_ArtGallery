using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Models;

public partial class ArtGalleryContext : DbContext
{
    public ArtGalleryContext()
    {
    }

    public ArtGalleryContext(DbContextOptions<ArtGalleryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArtWork> ArtWorks { get; set; }

    public virtual DbSet<ArtWorkType> ArtWorkTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<RoleType> RoleTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=ArtGallery;User Id=sa;Password=SQL;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtWork>(entity =>
        {
            entity.HasKey(e => e.IdartWork).HasName("PK__ArtWork__9E90614DE456F519");

            entity.ToTable("ArtWork");

            entity.Property(e => e.IdartWork).HasColumnName("IDArtWork");
            entity.Property(e => e.ArtWorkTypeId).HasColumnName("ArtWorkTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.PublicationDate).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ArtWorkType).WithMany(p => p.ArtWorks)
                .HasForeignKey(d => d.ArtWorkTypeId)
                .HasConstraintName("FK__ArtWork__ArtWork__2C3393D0");

            entity.HasOne(d => d.User).WithMany(p => p.ArtWorks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ArtWork__UserID__2B3F6F97");
        });

        modelBuilder.Entity<ArtWorkType>(entity =>
        {
            entity.HasKey(e => e.IdartWorkType).HasName("PK__ArtWorkT__8B67024B4D15B2B6");

            entity.ToTable("ArtWorkType");

            entity.Property(e => e.IdartWorkType).HasColumnName("IDArtWorkType");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Idorder).HasName("PK__Order__5CBBCADB957043D3");

            entity.ToTable("Order");

            entity.Property(e => e.Idorder).HasColumnName("IDOrder");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK__Order__PaymentTy__31EC6D26");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Order__UserID__30F848ED");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdorderItem).HasName("PK__OrderIte__C1C25E90D0E771C5");

            entity.ToTable("OrderItem");

            entity.Property(e => e.IdorderItem).HasColumnName("IDOrderItem");
            entity.Property(e => e.ArtWorkId).HasColumnName("ArtWorkID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.HasOne(d => d.ArtWork).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ArtWorkId)
                .HasConstraintName("FK__OrderItem__ArtWo__35BCFE0A");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderItem__Order__34C8D9D1");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.IdpaymentType).HasName("PK__PaymentT__E385F39EEE897D62");

            entity.ToTable("PaymentType");

            entity.Property(e => e.IdpaymentType).HasColumnName("IDPaymentType");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.IdroleType).HasName("PK__RoleType__5D77D1A83AE7F594");

            entity.ToTable("RoleType");

            entity.Property(e => e.IdroleType).HasColumnName("IDRoleType");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__Users__EAE6D9DF2098A828");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleTypeId).HasColumnName("RoleTypeID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.RoleType).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleTypeId)
                .HasConstraintName("FK__Users__RoleTypeI__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
