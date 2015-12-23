using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plivo_MVC_Samples.Controllers
{
    /// <summary>
    /// The AccountController demonstrates ways of interacting with the Plivo user Account
    /// </summary>
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

    }
}
