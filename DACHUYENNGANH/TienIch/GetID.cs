using System.Text.RegularExpressions;
using System.Text;

namespace DACHUYENNGANH.TienIch
{
    public class GetID
    {
        // B1. Bỏ dấu
        public static string BoDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // B2. lấy ký tự đầu tiên
        public static string GetFirstChar(string text)
        {
            string result = "";

            // ngắt chuỗi với những khoảng trắng, xuống dòng, khoảng tab
            string[] s = BoDau(text).Split();
            foreach (var i in s)
            {
                if (!string.IsNullOrEmpty(i))
                    result += i.ToUpper().Substring(0, 1);
            }
            return result;
        }

        public static string GetDateString(DateTime date)
        {
            string day = date.Day.ToString("00");
            string month = date.Month.ToString("00");
            string year = date.Year.ToString();
            // lấy ra với dạng ddMMyy
            string result = day + month + year.Substring(year.Length - 2); ;
            return result;
        }

        public static string GetIDByFullNameAnDob(string fullName, DateTime dateTime)
        {
            return GetFirstChar(fullName) + GetDateString(dateTime);
        }
    }
}
