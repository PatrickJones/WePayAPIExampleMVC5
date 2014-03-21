using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WePayMVC5Example.IoC;

namespace WePayMVC5Example.Controllers
{
    /// <summary>
    /// Base Controller that all controllers with derive from.
    /// IGlobalVariables is for DI functionality using Ninject. It injects 'GlobalVariables.cs' located in IoC folder.
    /// </summary>
    public class BaseController : Controller
    {
        protected IGlobalVariables globals;

        public BaseController(IGlobalVariables globalVars)
        {
            globals = globalVars;
        }

        //will execute for every action method making GlobalVariables.cs proppertiues accessible.
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            globals.hostUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
        }
	}
}