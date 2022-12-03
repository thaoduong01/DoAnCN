using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using ReportsWebsite.M

namespace ReportsWebsite
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<DoanhNghiep> DoanhNghieps { get; set; }
        public virtual DbSet<HoSoTinDung> HoSoTinDungs { get; set; }
        public virtual DbSet<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; }
        public virtual DbSet<KhachHangCaNhan> KhachHangCaNhans { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<TheTinDung> TheTinDungs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .Property(e => e.IdDoanhNghiep)
                .IsUnicode(false);

            modelBuilder.Entity<DoanhNghiep>()
                .HasMany(e => e.HoSoVayDoanhNghieps)
                .WithRequired(e => e.DoanhNghiep)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoSoTinDung>()
                .Property(e => e.IdHSTinDung)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoTinDung>()
                .Property(e => e.ChuKy)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoTinDung>()
                .Property(e => e.IdNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoTinDung>()
                .Property(e => e.IdKhachHangCaNhan)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoTinDung>()
                .HasMany(e => e.TheTinDungs)
                .WithRequired(e => e.HoSoTinDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoSoVayDoanhNghiep>()
                .Property(e => e.IdHSVay)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoVayDoanhNghiep>()
                .Property(e => e.IdNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<HoSoVayDoanhNghiep>()
                .Property(e => e.IdDoanhNghiep)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHangCaNhan>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHangCaNhan>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHangCaNhan>()
                .Property(e => e.IdKhachHangCaNhan)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHangCaNhan>()
                .HasMany(e => e.HoSoTinDungs)
                .WithRequired(e => e.KhachHangCaNhan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.IdNhanVien)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.CMND_CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.IdChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoSoTinDungs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoSoVayDoanhNghieps)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheTinDung>()
                .Property(e => e.STK)
                .IsUnicode(false);

            modelBuilder.Entity<TheTinDung>()
                .Property(e => e.SoTrenThe)
                .IsUnicode(false);

            modelBuilder.Entity<TheTinDung>()
                .Property(e => e.IdHSTinDung)
                .IsUnicode(false);

            modelBuilder.Entity<TheTinDung>()
                .Property(e => e.MaPin)
                .IsUnicode(false);

            modelBuilder.Entity<TheTinDung>()
                .Property(e => e.SoSauThe)
                .IsUnicode(false);
        }
    }
}
