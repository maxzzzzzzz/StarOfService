using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using DAL.Interfaces;
using Models;

namespace DAL
{
    public class ExecutorRepository : IRepository<Executor>
    {
        public bool Create(Executor executor)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (executor != null)
                    {
                        db.Executors.Add(executor);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public bool Delete(int id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var executor = db.Executors.Find(id);
                    if (executor != null)
                        db.Executors.Remove(executor);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant delete executor"));
            }
            return false;
        }

        public bool Update(Executor executor)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (executor != null)
                    {
                        db.Executors.AddOrUpdate(executor);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant update executor"));
            }
            return false;
        }
        public Executor FindById(int? id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var executor = db.Executors.Include("ServiceIndustry").Include("User").ToList().Find(item=>item.ExecutorId==id);
                return executor;
            }
        }
        public List<Executor> GetAll()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var executors = db.Executors.Include("ServiceIndustry").Include("User").ToList();
                    return executors;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant get executors"));
            }
            return null;
        }
    }
}