using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Concrete;
using UrlChanger.Models;

namespace UrlChanger.Middleware
{
    public class Reroute
    {
        private readonly RequestDelegate _next;
        public Reroute(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationContext _context)
        {
            DatabaseRepo repo = new DatabaseRepo(_context);
            string curUlr = context.Request.Path.Value.Replace("/","");


            var match = repo.Urls.GetRecords().Where(x => x.UrlModdedPath.Equals(curUlr));
            if (match.Count() > 0)
            {
                context.Response.Redirect(match.FirstOrDefault().UrlOriginal.AbsoluteUri);
                return;
            }

            await _next.Invoke(context);
        }

    }
}
