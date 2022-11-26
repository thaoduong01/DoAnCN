namespace DACHUYENNGANH.TienIch
{
    public class GetIDTD
    {
        public static string GetDateString()
        {
            string day = DateTime.Now.Day.ToString("00");
            string month = DateTime.Now.Month.ToString("00");
            string year = DateTime.Now.Year.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();
            // lấy ra với dạng ddMMyy
            string result = day + month + year.Substring(year.Length - 2) + hour + minute + second;
            return result;
        }

        public static string GetIDTinDun()
        {
            return "HDTD" + GetDateString();
        }
    }
}
