using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DACHUYENNGANH.Models
{
    public partial class DAChuyenNganhContext : DbContext
    {
        public DAChuyenNganhContext()
        {
        }

        public DAChuyenNganhContext(DbContextOptions<DAChuyenNganhContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChucVu> ChucVus { get; set; } = null!;
        public virtual DbSet<CongTyThamDinh> CongTyThamDinhs { get; set; } = null!;
        public virtual DbSet<DoanhNghiep> DoanhNghieps { get; set; } = null!;
        public virtual DbSet<HoSoBaoCaoTc> HoSoBaoCaoTcs { get; set; } = null!;
        public virtual DbSet<HoSoPhapLy> HoSoPhapLies { get; set; } = null!;
        public virtual DbSet<HoSoPhuongAnVay> HoSoPhuongAnVays { get; set; } = null!;
        public virtual DbSet<HoSoTaiSanDb> HoSoTaiSanDbs { get; set; } = null!;
        public virtual DbSet<HoSoThamDinh> HoSoThamDinhs { get; set; } = null!;
        public virtual DbSet<HoSoTinDung> HoSoTinDungs { get; set; } = null!;
        public virtual DbSet<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<KhachHangCaNhan> KhachHangCaNhans { get; set; } = null!;
        public virtual DbSet<LoaiHoSoTsdb> LoaiHoSoTsdbs { get; set; } = null!;
        public virtual DbSet<NhanVien> NhanViens { get; set; } = null!;
        public virtual DbSet<TheTinDung> TheTinDungs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=ADMIN\\CAMTHUY;Database=DAChuyenNganh;User Id=sa;password=admin12345;Trusted_Connection=False;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.HasKey(e => e.IdChucVu)
                    .HasName("PK__ChucVu__81D916DFC81D7111");

                entity.ToTable("ChucVu");

                entity.Property(e => e.IdChucVu)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenChucVu).HasMaxLength(100);
            });

            modelBuilder.Entity<CongTyThamDinh>(entity =>
            {
                entity.HasKey(e => e.IdCongTy)
                    .HasName("PK__CongTyTh__7FB1391AAE4695D8");

                entity.ToTable("CongTyThamDinh");

                entity.Property(e => e.IdCongTy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenCty).HasMaxLength(100);
            });

            modelBuilder.Entity<DoanhNghiep>(entity =>
            {
                entity.HasKey(e => e.IdDoanhNghiep)
                    .HasName("PK__DoanhNgh__B7BC800DC3C01AE1");

                entity.ToTable("DoanhNghiep");

                entity.Property(e => e.IdDoanhNghiep)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CmndCccd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CMND_CCCD");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenDoanhNghiep).HasMaxLength(100);

                entity.Property(e => e.TenNguoiDaiDien).HasMaxLength(50);

                entity.HasOne(d => d.IdKhachHangNavigation)
                    .WithMany(p => p.DoanhNghieps)
                    .HasForeignKey(d => d.IdKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DoanhNghi__IdKha__47DBAE45");
            });

            modelBuilder.Entity<HoSoBaoCaoTc>(entity =>
            {
                entity.HasKey(e => e.IdBctc)
                    .HasName("PK__HoSoBaoC__38AB7D3A7AFFA8AE");

                entity.ToTable("HoSoBaoCaoTC");

                entity.Property(e => e.IdBctc).HasColumnName("IdBCTC");

                entity.Property(e => e.BctaiChinh)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BCTaiChinh");

                entity.Property(e => e.HopDongMuaBan)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HopDongSdld)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HopDongSDLD");

                entity.Property(e => e.IdHsvay)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSVay");

                entity.Property(e => e.NgayNhanHs)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayNhanHS");

                entity.Property(e => e.SaoKeTknh)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SaoKeTKNH");

                entity.Property(e => e.ToVat)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ToVAT");

                entity.HasOne(d => d.IdHsvayNavigation)
                    .WithMany(p => p.HoSoBaoCaoTcs)
                    .HasForeignKey(d => d.IdHsvay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoBaoCa__IdHSV__4D94879B");
            });

            modelBuilder.Entity<HoSoPhapLy>(entity =>
            {
                entity.HasKey(e => e.IdPhapLy)
                    .HasName("PK__HoSoPhap__6727369E6A12C177");

                entity.ToTable("HoSoPhapLy");

                entity.Property(e => e.BbhopHd)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("BBHopHD");

                entity.Property(e => e.CmndCccdKtt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CMND_CCCD_KTT");

                entity.Property(e => e.DieuLeCty)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DieuLeCTy");

                entity.Property(e => e.Gcndkthue)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GCNDKThue");

                entity.Property(e => e.Gdkkd)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("GDKKD");

                entity.Property(e => e.IdHsvay)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSVay");

                entity.Property(e => e.NgayNhanHs)
                    .HasColumnType("date")
                    .HasColumnName("NgayNhanHS");

                entity.Property(e => e.TenKttruong)
                    .HasMaxLength(30)
                    .HasColumnName("TenKTTruong");

                entity.HasOne(d => d.IdHsvayNavigation)
                    .WithMany(p => p.HoSoPhapLies)
                    .HasForeignKey(d => d.IdHsvay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoPhapL__IdHSV__4CA06362");
            });

            modelBuilder.Entity<HoSoPhuongAnVay>(entity =>
            {
                entity.HasKey(e => e.IdHspavay)
                    .HasName("PK__HoSoPhuo__B8DB0DCFC118255A");

                entity.ToTable("HoSoPhuongAnVay");

                entity.Property(e => e.IdHspavay).HasColumnName("IdHSPAVay");

                entity.Property(e => e.IdHsvay)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSVay");

                entity.Property(e => e.KeHoachTraNo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NgayNhanHs)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayNhanHS");

                entity.Property(e => e.PhuongAnKd)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("PhuongAnKD");

                entity.HasOne(d => d.IdHsvayNavigation)
                    .WithMany(p => p.HoSoPhuongAnVays)
                    .HasForeignKey(d => d.IdHsvay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoPhuon__IdHSV__5070F446");
            });

            modelBuilder.Entity<HoSoTaiSanDb>(entity =>
            {
                entity.HasKey(e => e.IdHsdb)
                    .HasName("PK__HoSoTaiS__5509385E025381A4");

                entity.ToTable("HoSoTaiSanDB");

                entity.Property(e => e.IdHsdb).HasColumnName("IdHSDB");

                entity.Property(e => e.ChungNhanBaoHiem)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DcnsoHuuDat)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DCNSoHuuDat");

                entity.Property(e => e.HdtaiSan)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("HDTaiSan");

                entity.Property(e => e.IdHsvay)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSVay");

                entity.Property(e => e.IdLoaiHs).HasColumnName("IdLoaiHS");

                entity.Property(e => e.NgayNhanHs)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayNhanHS");

                entity.Property(e => e.SoDangKiem)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SoNhaDat)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TbnopPhiNd)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TBNopPhiND");

                entity.HasOne(d => d.IdHsvayNavigation)
                    .WithMany(p => p.HoSoTaiSanDbs)
                    .HasForeignKey(d => d.IdHsvay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoTaiSa__IdHSV__4E88ABD4");

                entity.HasOne(d => d.IdLoaiHsNavigation)
                    .WithMany(p => p.HoSoTaiSanDbs)
                    .HasForeignKey(d => d.IdLoaiHs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoTaiSa__IdLoa__4BAC3F29");
            });

            modelBuilder.Entity<HoSoThamDinh>(entity =>
            {
                entity.HasKey(e => e.IdHsthamDinh)
                    .HasName("PK__HoSoTham__3950FE557B35017C");

                entity.ToTable("HoSoThamDinh");

                entity.Property(e => e.IdHsthamDinh).HasColumnName("IdHSThamDinh");

                entity.Property(e => e.BaoCaoThamDinh)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CmndCccd).HasColumnName("CMND_CCCD");

                entity.Property(e => e.IdCongTy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdHsdb).HasColumnName("IdHSDB");

                entity.Property(e => e.NgayNhanHoSo).HasColumnType("datetime");

                entity.Property(e => e.NgayThamDinh).HasColumnType("datetime");

                entity.Property(e => e.TenNguoiThamDinh).HasMaxLength(50);

                entity.HasOne(d => d.IdCongTyNavigation)
                    .WithMany(p => p.HoSoThamDinhs)
                    .HasForeignKey(d => d.IdCongTy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoThamD__IdCon__4AB81AF0");

                entity.HasOne(d => d.IdHsdbNavigation)
                    .WithMany(p => p.HoSoThamDinhs)
                    .HasForeignKey(d => d.IdHsdb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoThamD__IdHSD__49C3F6B7");
            });

            modelBuilder.Entity<HoSoTinDung>(entity =>
            {
                entity.HasKey(e => e.IdHstinDung)
                    .HasName("PK__HoSoTinD__F86CE6E4997A122D");

                entity.ToTable("HoSoTinDung");

                entity.Property(e => e.IdHstinDung)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSTinDung");

                entity.Property(e => e.ChuKy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdKhachHangCaNhan)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.IdNhanVien)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayNhanHs)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayNhanHS");

                entity.HasOne(d => d.IdKhachHangCaNhanNavigation)
                    .WithMany(p => p.HoSoTinDungs)
                    .HasForeignKey(d => d.IdKhachHangCaNhan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoTinDu__IdKha__45F365D3");

                entity.HasOne(d => d.IdNhanVienNavigation)
                    .WithMany(p => p.HoSoTinDungs)
                    .HasForeignKey(d => d.IdNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoTinDu__IdNha__4222D4EF");
            });

            modelBuilder.Entity<HoSoVayDoanhNghiep>(entity =>
            {
                entity.HasKey(e => e.IdHsvay)
                    .HasName("PK__HoSoVayD__460D0D485F54EBAE");

                entity.ToTable("HoSoVayDoanhNghiep");

                entity.Property(e => e.IdHsvay)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSVay");

                entity.Property(e => e.IdDoanhNghiep)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.IdNhanVien)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NgayBdvay)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayBDVay");

                entity.Property(e => e.NgayKt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayKT");

                entity.HasOne(d => d.IdDoanhNghiepNavigation)
                    .WithMany(p => p.HoSoVayDoanhNghieps)
                    .HasForeignKey(d => d.IdDoanhNghiep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoVayDo__IdDoa__48CFD27E");

                entity.HasOne(d => d.IdNhanVienNavigation)
                    .WithMany(p => p.HoSoVayDoanhNghieps)
                    .HasForeignKey(d => d.IdNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HoSoVayDo__IdNha__4316F928");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.IdKhachHang)
                    .HasName("PK__KhachHan__7CF5D8367C57D305");

                entity.ToTable("KhachHang");

                entity.Property(e => e.IdKhachHang).ValueGeneratedNever();

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KhachHangCaNhan>(entity =>
            {
                entity.HasKey(e => e.IdKhachHangCaNhan)
                    .HasName("PK__KhachHan__34A11F57F9C65E00");

                entity.ToTable("KhachHangCaNhan");

                entity.Property(e => e.IdKhachHangCaNhan)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.CmndCccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CMND_CCCD");

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(3);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.HasOne(d => d.IdKhachHangNavigation)
                    .WithMany(p => p.KhachHangCaNhans)
                    .HasForeignKey(d => d.IdKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KhachHang__IdKha__46E78A0C");
            });

            modelBuilder.Entity<LoaiHoSoTsdb>(entity =>
            {
                entity.HasKey(e => e.IdLoaiHs)
                    .HasName("PK__LoaiHoSo__B41B73F0CEBF094C");

                entity.ToTable("LoaiHoSoTSDB");

                entity.Property(e => e.IdLoaiHs)
                    .ValueGeneratedNever()
                    .HasColumnName("IdLoaiHS");

                entity.Property(e => e.TenLoai).HasMaxLength(100);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.IdNhanVien)
                    .HasName("PK__NhanVien__B8294845F16824BA");

                entity.ToTable("NhanVien");

                entity.Property(e => e.IdNhanVien)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CmndCccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("CMND_CCCD");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasMaxLength(3);

                entity.Property(e => e.IdChucVu)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TenNhanVien).HasMaxLength(50);

                entity.HasOne(d => d.IdChucVuNavigation)
                    .WithMany(p => p.NhanViens)
                    .HasForeignKey(d => d.IdChucVu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NhanVien__IdChuc__412EB0B6");
            });

            modelBuilder.Entity<TheTinDung>(entity =>
            {
                entity.HasKey(e => e.Stk)
                    .HasName("PK__TheTinDu__CA1EB69B5350CE2E");

                entity.ToTable("TheTinDung");

                entity.Property(e => e.Stk)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("STK");

                entity.Property(e => e.IdHstinDung)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IdHSTinDung");

                entity.Property(e => e.MaPin)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.NgayMoThe).HasColumnType("date");

                entity.Property(e => e.NgayNhanThe).HasColumnType("date");

                entity.Property(e => e.SoSauThe)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SoTrenThe)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TenTk)
                    .HasMaxLength(50)
                    .HasColumnName("TenTK");

                entity.HasOne(d => d.IdHstinDungNavigation)
                    .WithMany(p => p.TheTinDungs)
                    .HasForeignKey(d => d.IdHstinDung)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TheTinDun__IdHST__44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
