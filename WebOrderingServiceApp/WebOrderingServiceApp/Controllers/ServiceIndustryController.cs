using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Interfaces;
using Models;
using WebOrderingServiceApp.Models;

namespace WebOrderingServiceApp.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ServiceIndustryController : Controller
    {
        IRepository<ServiceIndustry> repository;
        IRepository<ServiceIndustryType> repositoryServType;
        public ServiceIndustryController(IRepository<ServiceIndustry> repo, IRepository<ServiceIndustryType> repoServType)
        {
            repository = repo;
            repositoryServType = repoServType;
        }
        // GET: ServiceIndustry
        public ActionResult Index(int id,string sortOrder)
        {
            //var servcieIndustries = serviceIndustryRepository.GetAll().Where(item => item.ServiceIndustryTypeId==id);
            //var servcieIndustries = serviceIndustryRepository.GetAll();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var servcieIndustries = from s in db.ServiceIndustries.Include("ServiceIndustryType").Where(item => item.ServiceIndustryTypeId == id)
                    select s;
                //ViewBag.ServiceIndustryTypeName = db.ServiceIndustries.Include("ServiceIndustryType").FirstOrDefault(item => item.ServiceIndustryTypeId == id).ServiceIndustryType.Name;
                switch (sortOrder)
                {
                    case "name_desc":
                        servcieIndustries = servcieIndustries.OrderByDescending(s => s.Name);
                        break;
                    default:
                        servcieIndustries = servcieIndustries.OrderBy(s => s.Name);
                        break;
                }
                return View(servcieIndustries.ToList());
            }
        }
        private void SetTypeViewBag(int? ServiceIndustryTypeId = null)
        {

            if (ServiceIndustryTypeId == null)

                ViewBag.ServiceIndustryTypeId = new SelectList(repositoryServType.GetAll(), "ServiceIndustryTypeId", "Name");

            else

                ViewBag.ServiceIndustryTypeId = new SelectList(repositoryServType.GetAll(), "ServiceIndustryTypeId", "Name", ServiceIndustryTypeId);

        }

        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create()
        {
            SetTypeViewBag();
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create(ServiceIndustry serviceIndustry)
        {

            if (ModelState.IsValid)
            {
                repository.Create(serviceIndustry);

                return RedirectToAction("Index","ServiceIndustryType");
            }
            SetTypeViewBag(serviceIndustry.ServiceIndustryTypeId);

            return View(serviceIndustry);
            
        }

        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var serviceIndustry = repository.FindById(id);
                if (serviceIndustry == null)
                {
                    return HttpNotFound();
                }
                SetTypeViewBag();
                return View(serviceIndustry);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(ServiceIndustry serviceIndustry)
        {
            if (ModelState.IsValid)
            {

                repository.Update(serviceIndustry);
                return RedirectToAction("Index","ServiceIndustryType");
            }
            SetTypeViewBag(serviceIndustry.ServiceIndustryTypeId);
            return View(serviceIndustry);
        }

        [CustomAuthorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var serviceIndustry = repository.FindById(id);
                if (serviceIndustry == null)
                {
                    return HttpNotFound();
                }
                return View(serviceIndustry);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var serviceIndustry = repository.FindById(id);
            repository.Delete(serviceIndustry.ServiceIndustryId);
            return RedirectToAction("Index", "ServiceIndustryType");
        }
    }
}