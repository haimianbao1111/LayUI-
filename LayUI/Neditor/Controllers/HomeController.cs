using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neditor.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Form(VideoMDL model)
        {


            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}