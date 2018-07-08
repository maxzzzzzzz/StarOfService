using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Models;
using DAL.Interfaces;
using System.Data.Entity.Migrations;

namespace DAL
{
    public class UserRepository
    {
        public bool Create(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (user != null)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Can not create new User");
            }
            return false;
        }

        public bool Delete(string id)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    //var list = db.Executors.ToList();
                    foreach (Executor executor in db.Executors.ToList())
                    {
                        if (executor.UserId.ToString() == id)
                            db.Executors.Remove(executor);
                        db.SaveChanges();
                    }
                    var user = db.Users.FirstOrDefault(item => item.Id == id);
                    db.Users.Remove(user);
                    db.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant delete user"));
            }
            return false;
        }

        public bool Update(User user)
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (user != null)
                    {
                        db.Users.AddOrUpdate(user);
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant update user"));
            }
            return false;
        }
        public User FindUserById(string id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.Find(id);
                return user;
            }
        }
        public List<User> GetAllUsers()
        {
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var users = db.Users.ToList();
                    return users;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(new Exception("You cant get users"));
            }
            return null;
        }
    }
}