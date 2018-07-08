using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOrderingServiceApp.Models;
using Converter;

namespace WebOrderingServiceApp.Controllers
{
    [CustomAuthorize(Roles = "admin")]
    public class ConverterController : Controller
    {
        ConverterManager converterManager = new ConverterManager();
        // GET: Converter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JsonToXmlConverting()
        {
            converterManager.JsonToXmlServiceConverter();
            return View();
        }
        public ActionResult XmlToJsonConverting()
        {
            converterManager.XmlToJsonServiceConverter();
            return View();
        }
    }
}