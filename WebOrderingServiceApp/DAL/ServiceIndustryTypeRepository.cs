using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Models;

namespace DAL
{
    public class ServiceIndustryTypeRepository : IRepository<ServiceIndustryType>
    {
        public bool Create(ServiceIndustryType serviceIndustryType)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (serviceIndustryType != null)
                    {
                        db.ServiceIndustryTypes.Add(serviceIndustryType);
                        db.SaveChanges();
                    }
                    return true;
                }
                //return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Can not create new serviceIndustryType");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach(ServiceIndustry service in db.ServiceIndustries.ToList())
                    {
                        if (service.ServiceIndustryTypeId == id)
                        {   foreach(var executor in service.Executors)
                            {
                                if (executor.ServiceIndustryId == id)
                                {
                                    db.Executors.Remove(executor);
                                }
                            }
                            db.ServiceIndustries.Remove(service);
                        }
                        db.SaveChanges();
                    }
                    ServiceIndustryType serviceIndustryType = db.ServiceIndustryTypes.FirstOrDefault(item => item.ServiceIndustryTypeId == id);
                    db.ServiceIndustryTypes.Remove(serviceIndustryType);
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

        public ServiceIndustryType FindById(int? id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var serviceIndustryType = db.ServiceIndustryTypes.Find(id);
                return serviceIndustryType;
            }
        }

        public List<ServiceIndustryType> GetAll()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var serviceIndustryTypes = db.ServiceIndustryTypes.ToList();
                    return serviceIndustryTypes;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant get serviceIndustryTypes"));
            }
            return null;
        }

        public bool Update(ServiceIndustryType serviceIndustryType)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (serviceIndustryType != null)
                    {
                        db.ServiceIndustryTypes.AddOrUpdate(serviceIndustryType);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant update serviceIndustryType"));
            }
            return false;
        }
    }
}
