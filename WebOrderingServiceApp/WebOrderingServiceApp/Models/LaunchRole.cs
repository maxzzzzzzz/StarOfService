using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;

namespace WebOrderingServiceApp.Models
{
    public class LaunchRole
    {
        public LaunchRole()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var userManager = new ApplicationUserManager(new UserStore<User>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                // создаем две роли
                var role1 = new IdentityRole { Name = "admin" };
                var role2 = new IdentityRole { Name = "user" };

                // добавляем роли в бд
                roleManager.Create(role1);
                roleManager.Create(role2);
                var adminUser = db.Users.Where(name => name.Email == "111@mail.ru").First();
                var result = userManager.Update(adminUser);

                // если создание пользователя прошло успешно
                if (result.Succeeded)
                {
                    // добавляем для пользователя роль
                    userManager.AddToRole(adminUser.Id, role1.Name);
                    userManager.AddToRole(adminUser.Id, role2.Name);
                }
                db.SaveChanges();
            }
        }
    }
    
}