namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoSoTinDungs = new HashSet<HoSoTinDung>();
            HoSoVayDoanhNghieps = new HashSet<HoSoVayDoanhNghiep>();
        }

        [Key]
        [StringLength(10)]
        public string IdNhanVien { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNhanVien { get; set; }

        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND_CCCD { get; set; }

        [Required]
        [StringLength(50)]
        public string SDT { get; set; }

        [Required]
        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(10)]
        public string IdChucVu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSoTinDung> HoSoTinDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; }
    }
}
