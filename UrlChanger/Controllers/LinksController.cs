using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Concrete;
using UrlChanger.Models;

namespace UrlChanger.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private DatabaseRepo databaseRepo;
        public LinksController(ApplicationContext context)
        {
            databaseRepo = new DatabaseRepo(context);
        }
        // GET: LinksController
        public ActionResult Index()
        {
            User user = (User) HttpContext.Items["User"];
            IEnumerable<Url> data = databaseRepo.Urls.GetRecords().Where(x=>x.UserId == user.Id);
            return View(data);
        }

        //// GET: LinksController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: LinksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LinksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Url url)
        {
            Uri urlOriginal = new Uri(url.UrlOriginal.AbsoluteUri);
            string moddedUrl = FormatString(url.UrlOriginal.AbsoluteUri);
            User user = (User)HttpContext.Items["User"];

            url.UrlModdedPath = moddedUrl;
            url.UrlModded = $"{Request.Host.Value}/{moddedUrl}";

            url.UrlOriginalHost = urlOriginal.Host;
            url.UrlOriginalPath = urlOriginal.AbsolutePath;
            url.UserId = user.Id;
            try
            {
                databaseRepo.Urls.CreateRecord(url);
                databaseRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View(); //need to handle error
            }
        }

        // GET: LinksController/Edit/5
        public ActionResult Edit(int id)
        {
            Url url = databaseRepo.Urls.GetRecord(id);
            if (url == null)
                return null; //Needs changing
            return View(url);
        }

        // POST: LinksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Url url)
        {
            try
            {
                Url urlOriginal = databaseRepo.Urls.GetRecord(url.Id);
                if (url.UrlOriginal != urlOriginal.UrlOriginal)
                {
                    url.UrlModded = FormatString(url.UrlOriginal.AbsoluteUri);
                    databaseRepo.Urls.UpdateRecord(url);
                    databaseRepo.Save();
                }
                

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // POST: LinksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                databaseRepo.Urls.DeleteRecord(id);
                databaseRepo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string FormatString(string url)
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
