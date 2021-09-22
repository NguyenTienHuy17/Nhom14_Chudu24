using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nhom14_ChuDu.Models
{
    public partial class ChuDu24 : DbContext
    {
        public ChuDu24()
            : base("name=ChuDu24")
        {
        }

        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMais { get; set; }
        public virtual DbSet<DatPhong> DatPhongs { get; set; }
        public virtual DbSet<KhachSan> KhachSans { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<LoaiK> LoaiKS { get; set; }
        public virtual DbSet<PhongK> PhongKS { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietKhuyenMai>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<KhachSan>()
                .HasMany(e => e.PhongKS)
                .WithRequired(e => e.KhachSan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachSan>()
                .HasMany(e => e.LoaiKS)
                .WithMany(e => e.KhachSans)
                .Map(m => m.ToTable("ChiTietLoaiKS").MapLeftKey("IDKS").MapRightKey("IDLoaiKS"));

            modelBuilder.Entity<KhuyenMai>()
                .HasMany(e => e.ChiTietKhuyenMais)
                .WithRequired(e => e.KhuyenMai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongK>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PhongK>()
                .HasMany(e => e.ChiTietKhuyenMais)
                .WithRequired(e => e.PhongK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongK>()
                .HasMany(e => e.DatPhongs)
                .WithRequired(e => e.PhongK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsFixedLength();

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SDT)
                .IsFixedLength();
        }

        public System.Data.Entity.DbSet<Nhom14_ChuDu.Models.ThanhPho> ThanhPhoes { get; set; }
    }
}
