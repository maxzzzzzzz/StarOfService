using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOrderingServiceApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNet.Identity;
using Models;
using DAL;
using System.Data.Entity.Infrastructure;

namespace WebOrderingServiceApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {

        public ActionResult RootPage()
        {
            return View();
        }
    }
}
