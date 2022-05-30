using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlChanger.Helpers
{
    public static class ShortenUrl
    {
        public static string Shorten()
        {
            try
            {
                string urlSafe = string.Empty;

                Enumerable.Range(48, 75).Where(i => i < 58 || i > 64 && i < 91 || i > 96)
                    .OrderBy(o => new Random().Next()).ToList()
                    .ForEach(i => urlSafe += Convert.ToChar(i));

                string res = urlSafe.Substring(new Random().Next(0, urlSafe.Length), new Random().Next(6, 6));
                return res;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        //Old version
        public static string FormatString()
        {
            Random random = new Random();
            string result = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length = 7;
            char[] randomStringArray = Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray();

            result += String.Concat(randomStringArray.TakeWhile(char.IsLetterOrDigit));

            return result;


        }
    }
}
