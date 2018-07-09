using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebOrderingServiceApp.Models;

namespace WebOrderingServiceApp.Controllers
{
    [Authorize]
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UserRepository userRepository = new UserRepository();
        public UserController() { }
        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ActionResult> Index()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            using (ApplicationContext db = new ApplicationContext())
            {
                ViewData["Executors"] = db.Executors.Include("ServiceIndustry").Where(item => item.UserId == user.Id).ToList();
                return View(user);
            }        
        }
        /*[CustomAuthorize(Roles = "user")]
        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Index(string signInUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user;
                if (signInUser.Contains("@"))
                {
                    user = db.Users.FirstOrDefault(item => item.Email == signInUser);
                }
                else
                {
                    user = db.Users.FirstOrDefault(item => item.Id == signInUser);
                }
                ViewData["Executors"] = db.Executors.Include("ServiceIndustry").Where(item => item.UserId == user.Id).ToList();
                return View(user);
            }
        }*/
        /*public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }*/
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = userRepository.FindUserById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user, HttpPostedFileBase ImageFile)
        {
            
            var getGurrentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var photo = getGurrentUser.Image;
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    user.Image = photo;
                    user.PasswordHash = getGurrentUser.PasswordHash;
                    user.SecurityStamp = getGurrentUser.SecurityStamp;
                    userRepository.Update(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = user.UserName+ DateTime.Now.ToString("yymmssfff") + extension;
                    user.Image = "~/Image/ProfilePhotos/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/ProfilePhotos/"), fileName);
                    ImageFile.SaveAs(fileName);
                    user.PasswordHash = getGurrentUser.PasswordHash;
                    user.SecurityStamp = getGurrentUser.SecurityStamp;
                    userRepository.Update(user);
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        [CustomAuthorize(Roles = "admin")]
        public ActionResult GetAllUsers(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = from s in db.Users
                               select s;
                switch(sortOrder)
                {
                    case "name_desc":
                        users = users.OrderByDescending(s => s.FirstName);
                    break;
                    default:
                        users = users.OrderBy(s => s.FirstName);
                    break;
                }
                return View(users.ToList());
            }
        }
        // GET: /Movies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = userRepository.FindUserById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        // POST: /Movies/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Manage");
            }
        }*/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = userRepository.FindUserById(id);
            userRepository.Delete(user.Id);
            return RedirectToAction("Index", "User");
        }

        /*public ActionResult Index()
        {
            return View();
        }*/
    }
}