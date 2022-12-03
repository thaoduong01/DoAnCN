namespace ReportsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheTinDung")]
    public partial class TheTinDung
    {
        [Key]
        [StringLength(14)]
        public string STK { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(16)]
        public string SoTrenThe { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayMoThe { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayNhanThe { get; set; }

        [Required]
        [StringLength(16)]
        public string IdHSTinDung { get; set; }

        [StringLength(6)]
        public string MaPin { get; set; }

        [StringLength(3)]
        public string SoSauThe { get; set; }

        public virtual HoSoTinDung HoSoTinDung { get; set; }
    }
}
