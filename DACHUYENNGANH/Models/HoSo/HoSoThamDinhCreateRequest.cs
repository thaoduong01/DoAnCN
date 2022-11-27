﻿using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoThamDinhCreateRequest
    {
        public DateTime NgayThamDinh { get; set; }
        public double SoTienThamDinh { get; set; }
        public IFormFile? BaoCaoThamDinh { get; set; }
        public string TenNguoiThamDinh { get; set; }
        public int CmndCccd { get; set; }
        public string IdCongTy { get; set; }
        public int IdHsdb { get; set; }
    }
}
