using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace CinemaManagement.Utils
{
    public class Helper
    {

        public static string ConvertDoubleToPercentageStr(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero).ToString("P", CultureInfo.InvariantCulture);
        }
        public static string MD5Hash(string str)
        {
            StringBuilder hash = new StringBuilder();
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] bytes = md5.ComputeHash(new UTF8Encoding().GetBytes(str));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("X2"));
            }
            return hash.ToString();
        }
        public static bool IsPhoneNumber(string number)
        {
            if (number is null) return false;
            return Regex.Match(number, @"(([03+[2-9]|05+[6|8|9]|07+[0|6|7|8|9]|08+[1-9]|09+[1-4|6-9]]){3})+[0-9]{7}\b").Success;
        }
        public static string GetHourMinutes(TimeSpan t)
        {
            return t.ToString(@"hh\:mm");
        }

        public static string GetImagePath(string imageName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\Images", $"{imageName}" /*SelectedItem.Image*/);
        }

        public static string GetEmailTemplatePath(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\EmailTemplate", $"{fileName}" /*SelectedItem.Image*/);
        }

        public static string FormatVNMoney(decimal money)
        {
            if (money == 0)
            {
                return "0 ₫";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#} ₫", money);
        }
        public static string FormatDecimal(decimal n)
        {
            if (n == 0)
            {
                return "0";
            }
            return String.Format(CultureInfo.InvariantCulture,
                                "{0:#,#}", n);
        }
    }
}
