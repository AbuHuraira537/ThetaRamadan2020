using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ThetaRamadan2020.Models
{
    public partial class thetaramdan2020Context : DbContext
    {
        public thetaramdan2020Context()
        {
        }

        public thetaramdan2020Context(DbContextOptions<thetaramdan2020Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<PageViewCount> PageViewCount { get; set; }
        public virtual DbSet<PurchasedItems> PurchasedItems { get; set; }
        public virtual DbSet<SaledItems> SaledItems { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=den1.mssql7.gear.host;Database=thetaramdan2020;User ID=thetaramdan2020; Password=Ba19!332?UeX;Trusted_Connection=False;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.TotalItems).HasColumnName("total_items");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.ToTable("items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasColumnName("color")
                    .HasMaxLength(20);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Images)
                    .IsRequired()
                    .HasColumnName("images");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PerItemPrice).HasColumnName("per_item_price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_items_categories");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_items_vendor");
            });

            modelBuilder.Entity<PageViewCount>(entity =>
            {
                entity.ToTable("page_view_count");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url");
            });

            modelBuilder.Entity<PurchasedItems>(entity =>
            {
                entity.ToTable("purchased_items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Item).HasColumnName("item");

                entity.Property(e => e.ItemCategory).HasColumnName("item_category");

                entity.Property(e => e.ItemCount).HasColumnName("item_count");

                entity.Property(e => e.PerItemPrice).HasColumnName("per_item_price");

                entity.Property(e => e.PurchasedDate)
                    .HasColumnName("purchased_date")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.PurchasedItems)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchased_items_items");

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.PurchasedItems)
                    .HasForeignKey(d => d.ItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_purchased_items_categories");
            });

            modelBuilder.Entity<SaledItems>(entity =>
            {
                entity.ToTable("saled_items");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ItemCount).HasColumnName("item_count");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.SaledDate)
                    .HasColumnName("saled_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TotalBill).HasColumnName("total_bill");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SaledItems)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_saled_items_categories");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SaledItems)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_saled_items_users");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.SaledItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_saled_items_items");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasColumnName("user_role")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.VendorCompany)
                    .IsRequired()
                    .HasColumnName("vendor_company")
                    .HasMaxLength(50);

                entity.Property(e => e.VendorEmail)
                    .HasColumnName("vendor_email")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
