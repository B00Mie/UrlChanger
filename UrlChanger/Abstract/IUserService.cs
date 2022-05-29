using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlChanger.Models;

namespace UrlChanger.Abstract
{
    public interface IUserService
    {
        string Authenticate(User user);
        User GetById(int id);
    }
}
