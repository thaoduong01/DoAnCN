using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoTaiSanDBCreateRequest
    {
      
        public IFormFile? DcnsoHuuDat { get; set; }
        public IFormFile? HdtaiSan { get; set; }
        public IFormFile? SoNhaDat { get; set; }
        public IFormFile? TbnopPhiNd { get; set; } 
        public IFormFile? SoDangKiem { get; set; } 
        public IFormFile? ChungNhanBaoHiem { get; set; }
      
        public int IdLoaiHs { get; set; }
        public string IdHsvay { get; set; } 
    }
}
