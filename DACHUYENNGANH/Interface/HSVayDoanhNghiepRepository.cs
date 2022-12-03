using DACHUYENNGANH.Models;
using Microsoft.AspNetCore.Identity;

using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using Dapper;
using System.Data;

namespace DACHUYENNGANH.Interface
{
    public class HSVayDoanhNghiepRepository :IDatabaseRepo
    {
        private readonly string _connectionString;
        public HSVayDoanhNghiepRepository(IOptions<AppDbConnection> options)
        {
            _connectionString = options.Value.GetConnectionString;
        }
        public async Task<IEnumerable<HoSoVayDoanhNghiep>> GetHoSoVayDoanhNghieps() {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<HoSoVayDoanhNghiep>(@"select HoSoVayDoanhNghiep.IdHSVay, HoSoVayDoanhNghiep.NgayBDVay, HoSoVayDoanhNghiep.NgayKT, HoSoVayDoanhNghiep.SoTienVay, HoSoVayDoanhNghiep.LaiSuat, NhanVien.TenNhanVien, NhanVien.IdNhanVien from HoSoVayDoanhNghiep inner join NhanVien on HoSoVayDoanhNghiep.IdNhanVien = NhanVien.IdNhanVien", commandType: CommandType.Text);
            }
        }
    }
}
