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

                string res = urlSafe.Substring(new Random().Next(0, urlSafe.Length), new Random().Next(2, 6));
                return res;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}
