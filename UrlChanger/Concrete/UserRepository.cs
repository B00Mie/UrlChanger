using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Abstract;
using UrlChanger.Models;

namespace UrlChanger.Concrete
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationContext appContext;
        public UserRepository(ApplicationContext context)
        {
            appContext = context;
        }
        public void CreateRecord(User user)
        {
            appContext.Users.Add(user);
        }

        public void DeleteRecord(int id)
        {
            User user = appContext.Users.Find(id);
            if (user != null)
                appContext.Users.Remove(user);
        }

        public User GetRecord(int id)
        {
            return appContext.Users.Find(id);
        }

        public IEnumerable<User> GetRecords()
        {
            return appContext.Users;
        }

        public void UpdateRecord(User user)
        {
            appContext.Entry(user).State = EntityState.Modified;
        }
    }
}
