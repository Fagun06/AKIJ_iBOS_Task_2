using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRUD.Models;

namespace CRUD.DBContext
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbERP;Connect Timeout=30;Encrypt=False;Trusted_Connection=true;TrustServerCertificate=False;ApplicationIntent=ReadWrite;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.IntOrderId);

                entity.ToTable("tblOrders");

                entity.Property(e => e.IntOrderId).HasColumnName("intOrderId");

                entity.Property(e => e.DtLastActiveDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dtLastActiveDateTime");

                entity.Property(e => e.DtOrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("dtOrderDate");

                entity.Property(e => e.IntProductId).HasColumnName("intProductId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.NumQuantity)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("numQuantity");

                entity.Property(e => e.StrCustomerName)
                    .HasMaxLength(50)
                    .HasColumnName("strCustomerName");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.IntProductId)
                    .HasName("PK_tbIProducts");

                entity.ToTable("tblProducts");

                entity.Property(e => e.IntProductId).HasColumnName("intProductId");

                entity.Property(e => e.NumStock)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("numStock");

                entity.Property(e => e.NumUnitPrice)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("numUnitPrice");

                entity.Property(e => e.StrProductName)
                    .HasMaxLength(50)
                    .HasColumnName("strProductName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
