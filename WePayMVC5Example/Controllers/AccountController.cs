using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class AccountController : BaseController
    {

        public AccountController(IGlobalVariables gVars) : base(gVars)
        { }

        public async Task<string> GetAccountAsync()
        {
            GetAccountRequest req = new GetAccountRequest();
            req.account_id = WePayConfiguration.accountId;

            Account acc = new Account();
            var response = await acc.GetAccountAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["AccountInfo"] = response;

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

        public async Task<string> FindAccountAsync()
        {
            //NOTICE: I have not set the reference_id property on the FindAccountRequest object (req),
            //this means the call will return an array of ALL of the user's accounts. If you set this
            //property you will get an array of accounts that match that reference_id
            FindAccountRequest req = new FindAccountRequest();

            Account acc = new Account();
            var response = await acc.FindAccountAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["AccountInfo"] = response;

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

        public async Task<string> CreateAccountAsync()
        {
            //NOTE: You cannot create an account with the word 'wepay' in it.
            CreateAccountRequest req = new CreateAccountRequest();
            req.name = "Developer Test";
            req.description = "A test account for developers";
            req.country = "US";

            Account acc = new Account();
            var response = await acc.CreateAccountAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Errro"] = response.ErrorResponse;
            }

            ViewData["CreatedAccount"] = response;

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

        public async Task<string> ModifyAccountAsync()
        {
            ModifyAccountRequest req = new ModifyAccountRequest();
            req.account_id = WePayConfiguration.accountId;
            req.description = "From Mind to Matter";

            Account acc = new Account();
            var response = await acc.ModifyAccountAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Errro"] = response.ErrorResponse;
            }

            ViewData["ModifiedAccount"] = response;

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

        public async Task<string> DeleteAccountAsync()
        {
            DeleteAccountRequest req = new DeleteAccountRequest();
            //set to 00000 on purpose so you don't delete your account!!!
            //when sure use 'WePayConfiguration.accountId'
            req.account_id = 00000;

            Account acc = new Account();
            var response = await acc.DeleteAccountAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Errro"] = response.ErrorResponse;
            }

            ViewData["DeletedAccount"] = response;

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

        public async Task<string> GetAccountUpdateUriAsync()
        {
            GetAccountUpdateUriRequest req = new GetAccountUpdateUriRequest();
            req.account_id = WePayConfiguration.accountId;
            req.redirect_uri = "http://www.asp.net/";

            Account acc = new Account();
            var response = await acc.GetAccountUpdateUriAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Errro"] = response.ErrorResponse;
            }

            ViewData["UpdateUri"] = response;

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

        public async Task<string> GetAccountReserveDetailsAsync()
        {
            GetAccountReserveDetailsRequest req = new GetAccountReserveDetailsRequest();
            req.account_id = WePayConfiguration.accountId;
           
            Account acc = new Account();
            var response = await acc.GetAccountReserveDetailsAsync(req);
            if (response.ErrorResponse != null)
            {
                ViewData["Errro"] = response.ErrorResponse;
            }

            ViewData["ReserveDetails"] = response;

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