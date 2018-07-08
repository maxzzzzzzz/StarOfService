using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL.Interfaces
{

    public interface IRepository<T>
         where T : class
    {

        bool Create(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T FindById(int? id);
        List<T> GetAll();

    }
}