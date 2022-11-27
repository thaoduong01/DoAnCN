namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoTinDungCreateRequest
    {
        public double PhiMoThe { get; set; }
        public IFormFile? ChuKy { get; set; } 
        public string IdNhanVien { get; set; }
        public string IdKhachHangCaNhan { get; set; }
    }
}
