using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WePayMVC5Example.IoC;
using WePayMVC5HttpClientSDK;
using WePayMVC5HttpClientSDK.WePayAPIStructures;
using WePayMVC5HttpClientSDK.WePayAPITypes;

namespace WePayMVC5Example.Controllers
{
    public class AppController : BaseController
    {
        public AppController(IGlobalVariables gVars)
            : base(gVars)
        { }
        
        public async Task<string> GetAppInfoAsync()
        {
            //The WepayConfiguration class is in the SDK, not in the Sample app. 
            //Examine it to see the scope of permissions you want to access
            GetAppRequest req = new GetAppRequest
            {
                client_id = WePayConfiguration.clientId,
                client_secret = WePayConfiguration.clientSecret
            };

            var app = new App();
            var response = await app.GetAppAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["AppDetails"] = response;

            //simply returning reponse as JSON string
            //You can easily return ActionResult, ViewResult, PartialView, etc just by changing return type and creating a View,
            //because the 'response' is actually a typed object.
            if (response.ErrorResponse != null)
            {
                //have to format this way because 'ErrorResponse' class has [JsonIgnore] attribute
                return string.Format("An error occured. TYPE: {0}, MESSAGE: {1}", response.ErrorResponse.ExceptionName, response.ErrorResponse.Message);
            }
            return JsonConvert.SerializeObject(response);
        }

        public async Task<string> ModifyAppAsync()
        {
            //create a theme object if you like (optional)
            var theme = new ThemeStructure();
            theme.name = "A Simple Color";
            theme.button_color = "DAA520";
            theme.background_color = "DAA520";
            theme.primary_color = "DAA520";
            theme.secondary_color = "DAA520";

            //convert it to JSON object per WEPAY documentation.
            var themeObject = theme.ToJSON();

            ModifyAppRequest req = new ModifyAppRequest {
                client_id = WePayConfiguration.clientId,
                client_secret = WePayConfiguration.clientSecret,
                theme_object = themeObject,
                gaq_domains = new List<string>()
            };

            var appClient = new App();
            var response = await appClient.ModifyAppAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Modifications"] = response;

            //simply returning reponse as JSON string
            //You can easily return ActionResult, ViewResult, PartialView, etc just by changing return type and creating a View,
            //because the 'response' is actually a typed object.
            if (response.ErrorResponse != null)
            {
                //have to format this way because 'ErrorResponse' class has [JsonIgnore] attribute
                return string.Format("An error occured. TYPE: {0}, MESSAGE: {1}", response.ErrorResponse.ExceptionName, response.ErrorResponse.Message);
            }
            return JsonConvert.SerializeObject(response);
        }
	}
}