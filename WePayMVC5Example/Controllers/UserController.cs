using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WePayMVC5Example.IoC;
using WePayMVC5HttpClientSDK;
using WePayMVC5HttpClientSDK.WePayAPITypes;

namespace WePayMVC5Example.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IGlobalVariables gVars)
            : base(gVars)
        { }
        
        public async Task<string> GetUserAsync()
        {
            User u = new User();
            var response = await u.GetUserAsync();
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["UserInfo"] = response;

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

        public async Task<string> ModifyUserAsync()
        {
            ModifyUserRequest req = new ModifyUserRequest();
            //CANNOT USE LOCALHOST!
            req.callback_uri = "http://www.asp.net/";

            User u = new User();
            var response = await u.ModifyUserAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["ModifiedUserInfo"] = response;

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
        
        public async Task<string> RegisterUserAsync()
        {
            RegisterUserRequest req = new RegisterUserRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;
            req.email = "emailaddress@gmail.com";
            req.first_name = "Firstname";
            req.last_name = "LastName";
            req.scope = "manage_accounts,view_balance,collect_payments,refund_payments,view_user,preapprove_payments,send_money";
            req.original_ip = "<IP-ADDRESS>";
            //http://whatsmyuseragent.com/ to get you device user agent & your IP address
            req.original_device = "<USER-AGENT>";
            req.redirect_uri = "http://www.asp.net/";


            User u = new User();
            var response = await u.RegisterUserAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["RegisteredUser"] = response;

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