using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Models;

namespace UrlChanger.Concrete
{
    public class DatabaseRepo : IDisposable
    {
        private ApplicationContext appContext;
        private UrlRepository urlRepo;
        private UserRepository userRepo;

        private bool disposed = false;

        public DatabaseRepo(ApplicationContext context)
        {
            appContext = context;
        }


        public UrlRepository Urls
        {
            get
            {
                if (urlRepo == null)
                    urlRepo = new UrlRepository(appContext);
                return urlRepo;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (userRepo == null)
                    userRepo = new UserRepository(appContext);
                return userRepo;
            }
        }

        public void Save()
        {
            appContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                appContext.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
