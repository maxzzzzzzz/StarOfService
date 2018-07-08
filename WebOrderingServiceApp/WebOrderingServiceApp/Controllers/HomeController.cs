using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using WebOrderingServiceApp.Models;

namespace WebOrderingServiceApp.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            //FirstMigration migration = new FirstMigration();
           // LaunchRole role = new LaunchRole();
            return View();
        }
        public ActionResult HomePage()
        {
            /*using (ServiceIndustryContext db = new ServiceIndustryContext())
            {
                var services = db.ServiceIndustryTypes.ToList();
                return View(services.ToList());
            }*/
             return View();
        }
        /*public ActionResult GetAllUsers()
        {
            using (ServiceIndustryContext db = new ServiceIndustryContext())
            {
                var users = db.Users.Include("Role");
                return View(users.ToList());
            }

        }*/
        /*public ActionResult GetAllServiceIndustries()
        {
            using (ServiceIndustryContext db = new ServiceIndustryContext())
            {
                var servicIndustries = db.ServiceIndustries.Include("ServiceIndustryType");
                return View(servicIndustries.ToList());
            }
        }*/
        /*public ActionResult GetAllExecutors()
        {
            using (ServiceIndustryContext db = new ServiceIndustryContext())
            {
                var executors = db.Executors.Include("User").Include("ServiceIndustry");
                return View(executors.ToList());
            }
        }*/
        /*[HttpGet]
        public ActionResult SingIn()
        {
            return View();
        }*/

        /*[HttpPost]
        public ActionResult SingIn(string username, string password)
        {
            if (username == "asd")
                return View("SingIn");
            else
                return View("About");
        }*/
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}