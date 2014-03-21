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
    public class PreapprovalController : BaseController
    {
        //NOTICE the 'preapproval_id' parameter on the GetPreapprovalAsync(), ModifyPreapprovalAsync(), CancelPreapprovalAsync()
        //action methods, it is set to 00000 because it can only be set AFTER creating a preapproval object
        //which returns a 'preapproval_id' that expires 30 minutes after creation(Like chechout_id).
        //To test these calls, first create a preapproval object, copy the returned 'preapproval_id' and past in the above action methods.
        //You have 30 mins to test before you have to create a new preapproval object.
        public PreapprovalController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetPreapprovalAsync()
        {
            GetPreapprovalRequest req = new GetPreapprovalRequest();
            //this will expire after 30 minutes
            req.preapproval_id = 000000;

            Preapproval pa = new Preapproval();
            var response = await pa.GetPreapprovalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["PreapprovalInfo"] = response;

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

        public async Task<string> CreatePreapprovalAsync()
        {
            CreatePreapprovalRequest req = new CreatePreapprovalRequest();
            req.short_description = "this is short description for a March preapproval test.";
            req.period = "monthly";
            req.frequency = 1;
            req.account_id = WePayConfiguration.accountId;
            req.amount = 13.33M;

            Preapproval pa = new Preapproval();
            var response = await pa.CreatePreapprovalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CreatedPreapproval"] = response;

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

        //This call lets you search the preapprovals associated with an account or an application.
        //If account_id is blank, then the response will be all preapprovals for the application.
        //Otherwise, it will be specifically for that account. You can search by 'state' and/or 'reference_id',
        //and the response will be an array of all the matching preapprovals. 
        public async Task<string> FindPreapprovalAsync()
        {
            FindPreapprovalRequest req = new FindPreapprovalRequest();
            req.account_id = WePayConfiguration.accountId;

            Preapproval pa = new Preapproval();
            var response = await pa.FindPreapprovalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Preapproval"] = response;

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

        public async Task<string> ModifyPreapprovalAsync()
        {
            ModifyPreapprovalRequest req = new ModifyPreapprovalRequest();
            req.preapproval_id = 000000;

            Preapproval pa = new Preapproval();
            var response = await pa.ModifyPreapprovalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["ModifiedPreapproval"] = response;

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

        public async Task<string> CancelPreapprovalAsync()
        {
            CancelPreapprovalRequest req = new CancelPreapprovalRequest();
            req.preapproval_id = 000000;

            Preapproval pa = new Preapproval();
            var response = await pa.CancelPreapprovalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CancelPreapproval"] = response;

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