using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlChanger.Abstract
{
    interface IRepository<T> where T:class
    {
        IEnumerable<T> GetRecords();
        T GetRecord(int id);
        void CreateRecord(T item);
        void UpdateRecord(T item);
        void DeleteRecord(int id);
    }
}
