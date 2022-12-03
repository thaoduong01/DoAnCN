using AspNetCore.Reporting;
using DACHUYENNGANH.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DACHUYENNGANH.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDatabaseRepo _databaseRepo;
        
        public ReportController(IWebHostEnvironment webHostEnvironment, IDatabaseRepo databaseRepo)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._databaseRepo = databaseRepo;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Print()
        {
            string mintype = "";
            int extention = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report2.rdlc";
            Dictionary<string, string> parmeter = new Dictionary<string, string>();
            //parmeter.Add("rp1", "wellcome to Cuu Long Bank");
            var hosovay = await _databaseRepo.GetHoSoVayDoanhNghieps();
            LocalReport local = new LocalReport(path);
            local.AddDataSource("DSHoSoVayDoanhNghiep", hosovay);
            var result = local.Execute(RenderType.Pdf, extention, parmeter, mintype);
            if(result != null) {
                return File(result.MainStream, "application/pdf");
            }
            return View();

        }
    }
}
