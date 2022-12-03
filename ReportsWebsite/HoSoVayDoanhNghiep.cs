namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoSoVayDoanhNghiep")]
    public partial class HoSoVayDoanhNghiep
    {
        [Key]
        [StringLength(16)]
        public string IdHSVay { get; set; }

        public DateTime NgayBDVay { get; set; }

        public double SoTienVay { get; set; }

        public DateTime NgayKT { get; set; }

        public double LaiSuat { get; set; }

        [Required]
        [StringLength(10)]
        public string IdNhanVien { get; set; }

        [Required]
        [StringLength(16)]
        public string IdDoanhNghiep { get; set; }

        public virtual DoanhNghiep DoanhNghiep { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
