using DAL.Interfaces;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using DAL;

namespace WebOrderingServiceApp.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<ServiceIndustry>>().To<ServiceIndustryRepository>();
            Bind<IRepository<Executor>>().To<ExecutorRepository>();
            Bind<IRepository<ServiceIndustryType>>().To<ServiceIndustryTypeRepository>();
        }
    }
}