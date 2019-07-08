using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CampanasPPG.Controllers
{
    public class WizardController : Controller
    {
        // GET: Wizard
        public ActionResult Wizard()
        {
            return View();
        }

        public JsonResult GetInformacionWizard()
        {
            Thread.Sleep(3000);

            return Json(true, JsonRequestBehavior.AllowGet);
        }


    }
}