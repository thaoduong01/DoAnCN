namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoTinDungEditRequest
    {
        public string IdHstinDung { get; set; }
        public double PhiMoThe { get; set; }
        public IFormFile? ChuKy { get; set; }
        public string IdNhanVien { get; set; }
        public string IdKhachHangCaNhan { get; set; }
    }
}
