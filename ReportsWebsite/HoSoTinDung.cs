namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSoTinDung")]
    public partial class HoSoTinDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoSoTinDung()
        {
            TheTinDungs = new HashSet<TheTinDung>();
        }

        [Key]
        [StringLength(16)]
        public string IdHSTinDung { get; set; }

        public DateTime NgayNhanHS { get; set; }

        public double PhiMoThe { get; set; }

        [Required]
        [StringLength(255)]
        public string ChuKy { get; set; }

        [Required]
        [StringLength(10)]
        public string IdNhanVien { get; set; }

        [Required]
        [StringLength(12)]
        public string IdKhachHangCaNhan { get; set; }

        public virtual KhachHangCaNhan KhachHangCaNhan { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheTinDung> TheTinDungs { get; set; }
    }
}
