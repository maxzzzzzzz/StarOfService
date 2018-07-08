using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;

namespace DAL
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("WebApp") { }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public DbSet<Executor> Executors { get; set; }
        public DbSet<ServiceIndustry> ServiceIndustries { get; set; }
        public DbSet<ServiceIndustryType> ServiceIndustryTypes { get; set; }

        public DbSet<MyRole> IdentityRoles { get; set; }
    }
}