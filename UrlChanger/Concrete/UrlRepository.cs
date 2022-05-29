using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Abstract;
using UrlChanger.Models;

namespace UrlChanger.Concrete
{
    public class UrlRepository : IRepository<Url>
    {
        private ApplicationContext appContext;
        public UrlRepository(ApplicationContext context)
        {
            appContext = context;
        }
        public void CreateRecord(Url url)
        {
            appContext.Urls.Add(url);
        }

        public void DeleteRecord(int id)
        {
            Url url = appContext.Urls.Find(id);
            if (url != null)
                appContext.Urls.Remove(url);
        }

        public Url GetRecord(int id)
        {
            return appContext.Urls.Find(id);
        }

        public IEnumerable<Url> GetRecords()
        {
            return appContext.Urls;
        }

        public void UpdateRecord(Url url)
        {
            Url data = appContext.Urls.Where(x => x.Id == url.Id).First();
            data.UrlModded = url.UrlModded;
            data.UrlOriginal = url.UrlOriginal;
            //appContext.Update(url);
            //appContext.Entry(url).State = EntityState.Modified;
        }
    }
}
