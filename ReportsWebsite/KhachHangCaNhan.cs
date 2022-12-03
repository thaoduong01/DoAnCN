namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHangCaNhan")]
    public partial class KhachHangCaNhan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHangCaNhan()
        {
            HoSoTinDungs = new HashSet<HoSoTinDung>();
        }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        public int IdKhachHang { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(50)]
        public string SDT { get; set; }

        [Required]
        [StringLength(12)]
        public string CMND_CCCD { get; set; }

        [Required]
        [StringLength(50)]
        public string TenKH { get; set; }

        [Key]
        [StringLength(12)]
        public string IdKhachHangCaNhan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoSoTinDung> HoSoTinDungs { get; set; }
    }
}
