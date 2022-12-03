namespace DACHUYENNGANH.Models.HoSo
{
    public class HoSoPhuongAnVayEditRequest
    {
        public int IdHspavay { get; set; }
        public IFormFile? PhuongAnKd { get; set; }
        public IFormFile? KeHoachTraNo { get; set; }

        public string IdHsvay { get; set; }
    }
}
