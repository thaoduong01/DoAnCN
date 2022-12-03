namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoBaoCaoTCEditRequest
    {
        public int IdBctc { get; set; }
        public IFormFile? ToVat { get; set; }
        public IFormFile? HopDongSdld { get; set; }
        public IFormFile? HopDongMuaBan { get; set; }
        public IFormFile? SaoKeTknh { get; set; }
        public IFormFile? BctaiChinh { get; set; }
        public string IdHsvay { get; set; }
    }
}
