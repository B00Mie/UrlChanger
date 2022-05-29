using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using UrlChanger.Models;

namespace UrlChanger.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext appContext;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            appContext = context;
        }

        public IActionResult Index()
        {
            var mapping = appContext.Model.FindEntityType(typeof(Url));
            string schema = mapping.GetSchema();
            string table = mapping.GetTableName();

            bool hasChanges = appContext.ChangeTracker.HasChanges();

            int updates = appContext.SaveChanges();
            return View();
        }

        public IActionResult FormatLink(string url)
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}
