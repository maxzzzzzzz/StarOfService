using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class FirstMigration
    {
        public FirstMigration()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Executor executor1 = new Executor() { Price = 200, DateTime = DateTime.Now, UserId = "3d4f6039-773d-4c3a-a727-cca8ff3407e8" };
                Executor executor2= new Executor() { Price = 300, DateTime = DateTime.Now, UserId = "6171483a-a5be-415f-a7cc-02df0e49c7b8" };
                Executor executor3 = new Executor() { Price = 400, DateTime = DateTime.Now, UserId = "dc61eddc-313f-4570-8fce-629d31e92343" };
                Executor executor4 = new Executor() { Price = 500, DateTime = DateTime.Now, UserId = "e504fe16-ffcb-4e00-9626-ba576dc47316" };
                ServiceIndustryType typeAuto = new ServiceIndustryType()
                {
                    Name = "Auto",
                    ServiceIndustryTypePhoto = "auto.png"
                };
                ServiceIndustryType typeIt = new ServiceIndustryType()
                {
                    Name = "It",
                    ServiceIndustryTypePhoto = "it.png"
                };
                ServiceIndustry service = new ServiceIndustry() { Name = "Wehl change", Description = "Change KALESo", Executors =new List<Executor>() { executor1, executor2 }, ServiceIndustryType = typeAuto };
                ServiceIndustry service2 = new ServiceIndustry() { Name = "It change baterry", Description = "Change battery", Executors = new List<Executor>() { executor3, executor4}, ServiceIndustryType = typeIt };
                db.Executors.AddRange(new List<Executor>() { executor1, executor2, executor3, executor4 });
                db.ServiceIndustryTypes.AddRange(new List<ServiceIndustryType>() { typeAuto, typeIt });
                db.ServiceIndustries.AddRange(new List<ServiceIndustry>() { service, service2 });
                db.SaveChanges();
            }

        }
    }
}
