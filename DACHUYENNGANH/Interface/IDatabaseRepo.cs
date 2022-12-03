
using DACHUYENNGANH.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DACHUYENNGANH.Interface
{
    public interface IDatabaseRepo
    {
        Task<IEnumerable<HoSoVayDoanhNghiep>> GetHoSoVayDoanhNghieps();
        
    }
}
