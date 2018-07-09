using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using Models;
using WebOrderingServiceApp.Models;

namespace WebOrderingServiceApp.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ServiceIndustryTypeController : Controller
    {
        ServiceIndustryTypeRepository serviceIndustryTypeRepository = new ServiceIndustryTypeRepository();
        // GET: ServiceIndustryType
        public ActionResult Index(string sortOrder)
        {
            //var serviceIndustryTypes = serviceIndustryTypeRepository.GetAll();
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            using (ApplicationContext db = new ApplicationContext())
            {
                var serviceIndustryTypes = from s in db.ServiceIndustryTypes
                            select s;
                switch (sortOrder)
                {
                    case "name_desc":
                        serviceIndustryTypes = serviceIndustryTypes.OrderByDescending(s => s.Name);
                        break;
                    default:
                        serviceIndustryTypes = serviceIndustryTypes.OrderBy(s => s.Name);
                        break;
                }
                return View(serviceIndustryTypes.ToList());
            }
        }
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Create(ServiceIndustryType serviceIndustryType, HttpPostedFileBase ImageFile)
        {

            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    serviceIndustryType.ServiceIndustryTypePhoto = @"~/Image/ServicesPhotos/simple.png";
                    serviceIndustryTypeRepository.Create(serviceIndustryType);
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = serviceIndustryType.Name + DateTime.Now.ToString("yymmssfff") + extension;
                    serviceIndustryType.ServiceIndustryTypePhoto = "~/Image/ServicesPhotos/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/ServicesPhotos/"), fileName);
                    ImageFile.SaveAs(fileName);
                    serviceIndustryTypeRepository.Create(serviceIndustryType);
                    return RedirectToAction("Index");
                }
                
            }
            return View(serviceIndustryType);
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
                var serviceIndustryType = serviceIndustryTypeRepository.FindById(id);
                if (serviceIndustryType == null)
                {
                    return HttpNotFound();
                }
                return View(serviceIndustryType);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult Edit(ServiceIndustryType serviceIndustryType, HttpPostedFileBase ImageFile)
        {
            var service = serviceIndustryTypeRepository.FindById(serviceIndustryType.ServiceIndustryTypeId);
            var photo = service.ServiceIndustryTypePhoto;
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    serviceIndustryType.ServiceIndustryTypePhoto = photo;
                    serviceIndustryTypeRepository.Update(serviceIndustryType);
                    return RedirectToAction("Index");
                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string extension = Path.GetExtension(ImageFile.FileName);
                    fileName = serviceIndustryType.Name + DateTime.Now.ToString("yymmssfff") + extension;
                    serviceIndustryType.ServiceIndustryTypePhoto = "~/Image/ServicesPhotos/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/ServicesPhotos/"), fileName);
                    ImageFile.SaveAs(fileName);
                    serviceIndustryTypeRepository.Update(serviceIndustryType);
                    return RedirectToAction("Index");
                }
            }
            return View(serviceIndustryType);
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
                var serviceIndustryType = serviceIndustryTypeRepository.FindById(id);
                if (serviceIndustryType == null)
                {
                    return HttpNotFound();
                }
                return View(serviceIndustryType);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var serviceIndustryType = serviceIndustryTypeRepository.FindById(id);
            serviceIndustryTypeRepository.Delete(serviceIndustryType.ServiceIndustryTypeId);
            return RedirectToAction("Index", "ServiceIndustryType");
        }
    }
}