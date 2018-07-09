using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Models;
using PagedList;
using WebOrderingServiceApp.Models;

namespace WebOrderingServiceApp.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ExecutorController : Controller
    {
        ExecutorRepository executorRepository = new ExecutorRepository();
        ServiceIndustryRepository serviceIndustryRepository = new ServiceIndustryRepository();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ExecutorController() { }
        public ExecutorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Executor
        public ActionResult Index(int id, string sortOrder, int? page)
        {
            //var executors = executorRepository.GetAll().Where(item => item.ServiceIndustryId == id);
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            using (ApplicationContext db = new ApplicationContext())
            {
                var executors = from s in db.Executors.Include("ServiceIndustry").Include("User").Where(item => item.ServiceIndustryId == id)
                            select s;
                switch (sortOrder)
                {
                    case "Price":
                        executors = executors.OrderBy(s => s.Price);
                        break;
                    case "price_desc":
                        executors = executors.OrderByDescending(s => s.Price);
                        break;
                    case "Date":
                        executors = executors.OrderBy(s => s.DateTime);
                        break;
                    case "date_desc":
                        executors = executors.OrderByDescending(s => s.DateTime);
                        break;
                    default:
                        executors = executors.OrderByDescending(s => s.ServiceIndustry.Name);
                        break;
                }
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                return View(executors.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult GetAllExecutors(string sortOrder, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            using (ApplicationContext db = new ApplicationContext())
            {
                var executors = from s in db.Executors.Include("ServiceIndustry").Include("User")
                                select s;
                switch (sortOrder)
                {
                    case "name_desc":
                        executors = executors.OrderBy(s => s.ServiceIndustry.Name);
                        break;
                    case "Price":
                        executors = executors.OrderBy(s => s.Price);
                        break;
                    case "price_desc":
                        executors = executors.OrderByDescending(s => s.Price);
                        break;
                    case "Date":
                        executors = executors.OrderBy(s => s.DateTime);
                        break;
                    case "date_desc":
                        executors = executors.OrderByDescending(s => s.DateTime);
                        break;
                    default:
                        executors = executors.OrderByDescending(s => s.ServiceIndustry.Name);
                        break;
                }
                int pageSize = 7;
                int pageNumber = (page ?? 1);
                return View(executors.ToPagedList(pageNumber, pageSize));
            }
        }
        [Authorize]
        public ActionResult Create()
        {
            SetServiceViewBag();
            return View();
        }
        private void SetServiceViewBag(int? ServiceIndustryId = null)
        {

            if (ServiceIndustryId == null)

                ViewBag.ServiceIndustryId = new SelectList(serviceIndustryRepository.GetAll(), "ServiceIndustryId", "Name");

            else

                ViewBag.ServiceIndustryId = new SelectList(serviceIndustryRepository.GetAll(), "ServiceIndustryId", "Name", ServiceIndustryId);

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(Executor executor)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                executor.UserId = user.Id;
                executor.DateTime = DateTime.Now;
                executorRepository.Create(executor);

                return RedirectToAction("Index","ServiceIndustryType");
            }
            SetServiceViewBag(executor.ServiceIndustryId);
            //SetServiceViewBag(executor.ServiceIndustryId);
            return View(executor);
       
        }
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var executor = executorRepository.FindById(id);
                if (executor == null)
                {
                    return HttpNotFound();
                }
                SetServiceViewBag();
                return View(executor);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Executor executor)
        {
            if (ModelState.IsValid)
            {
                executor.DateTime = DateTime.Now;
                executorRepository.Update(executor);
                return RedirectToAction("Index","User");
            }
            SetServiceViewBag(executor.ServiceIndustryId);
            return View(executor);
        }

        // GET: /Movies/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var executor = executorRepository.FindById(id);
                if (executor == null)
                {
                    return HttpNotFound();
                }
                return View(executor);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var executor = executorRepository.FindById(id);
            executorRepository.Delete(executor.ExecutorId);
            return RedirectToAction("Index", "User");
        }
    }
}