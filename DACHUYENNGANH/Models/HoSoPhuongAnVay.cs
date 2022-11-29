using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoPhuongAnVay
    {
        public int IdHspavay { get; set; }
        public string PhuongAnKd { get; set; } = null!;
        public string KeHoachTraNo { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhanHs { get; set; }
        public string IdHsvay { get; set; } = null!;

        public virtual HoSoVayDoanhNghiep IdHsvayNavigation { get; set; } = null!;
    }
}
