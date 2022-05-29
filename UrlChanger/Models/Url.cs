using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlChanger.Models
{
    public class Url
    {
        public int Id { get; set; }
        public Uri UrlOriginal { get; set; }
        public string UrlOriginalHost { get; set; }
        public string UrlOriginalPath { get; set; }


        public string UrlModded { get; set; }
        public string UrlModdedPath { get; set; }
        public int UserId { get; set; }

    }
}
