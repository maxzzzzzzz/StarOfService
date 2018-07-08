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
    public class ServiceIndustryRepository : IRepository<ServiceIndustry>
    {
        public bool Create(ServiceIndustry serviceIndustry)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (serviceIndustry != null)
                    {
                        db.ServiceIndustries.Add(serviceIndustry);
                        db.SaveChanges();
                    }
                    return true;
                }
                //return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Can not create new ServiceIndustry");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (Executor executor in db.Executors.ToList())
                    {
                        if (executor.ServiceIndustryId == id)
                        {
                            db.Executors.Remove(executor);
                        }
                        db.SaveChanges();
                    }
                    ServiceIndustry serviceIndustry = db.ServiceIndustries.FirstOrDefault(item => item.ServiceIndustryId == id);
                    db.ServiceIndustries.Remove(serviceIndustry);
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant delete serviceIndustry"));
            }
            return false;
        }

        public ServiceIndustry FindById(int? id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var serviceIndustry = db.ServiceIndustries.Find(id);
                return serviceIndustry;
            }
        }

        public List<ServiceIndustry> GetAll()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var serviceIndustries = db.ServiceIndustries.Include("ServiceIndustryType").ToList();
                    return serviceIndustries;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant get serviceIndustries"));
            }
            return null;
        }

        public bool Update(ServiceIndustry serviceIndustry)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (serviceIndustry != null)
                    {
                        db.ServiceIndustries.AddOrUpdate(serviceIndustry);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant update serviceIndustry"));
            }
            return false;
        }
    }
}