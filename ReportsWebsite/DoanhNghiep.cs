namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoanhNghiep")]
    public partial class DoanhNghiep
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoanhNghiep()
        {
            HoSoVayDoanhNghieps = new HashSet<HoSoVayDoanhNghiep>();
        }

        [Required]
        [StringLength(100)]
        public string TenDoanhNghiep { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string SDT { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNguoiDaiDien { get; set; }

        [Required]
        [StringLength(50)]
        public string CMND_CCCD { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Key]
        [StringLength(16)]
        public string IdDoanhNghiep { get; set; }

        public int IdKhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; }
    }
}
