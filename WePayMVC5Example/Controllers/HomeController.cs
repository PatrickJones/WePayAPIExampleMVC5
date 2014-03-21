using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WePayMVC5Example.IoC;

namespace WePayMVC5Example.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public ActionResult Index()
        {
            return View();
        }
    }
}