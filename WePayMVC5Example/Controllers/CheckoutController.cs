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
    //NOTICE the 'checkout_id' parameter on the GetCheckoutAsync(), CancelCheckoutAsync(), RefundCheckoutAsync(), CaptureCheckoutAsync(), ModifyCheckoutAsync()
    //action methods, it is set to 00000 because it can only be set AFTER creating a checkout object
    //which returns a 'checkout_id' that expires 30 minutes after creation.
    //To test these calls, first create a checkout object, copy the returned 'checkout_id' and past in the above action methods.
    //You have 30 mins to test before you have to create a new checkout object.
    //see https://www.wepay.com/developer/reference/checkout to know how to read 'partial refunds' and 'partial chargebacks'
    public class CheckoutController : BaseController
    {
        public CheckoutController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetCheckoutAsync()
        {
            GetCheckoutRequest req = new GetCheckoutRequest();
            //expires 30 minutes after creation
            req.checkout_id = 000000;

            Checkout chk = new Checkout();
            var response = await chk.GetCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutInfo"] = response;

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

        //If you understand nothing else about WePays' API, know and understand this method.
        public async Task<string> CreateCheckoutAsync()
        {
            CreateCheckoutRequest req = new CreateCheckoutRequest();
            req.account_id = WePayConfiguration.accountId;
            req.short_description = "Test Checkout for dev account";
            req.amount = 23.25M;
            //this has SPECIFIC values check WePay documentation.
            req.type = "SERVICE";

            Checkout chk = new Checkout();
            var response = await chk.CreateCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutCreated"] = response;

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

        public async Task<string> FindCheckoutAsync()
        {
            FindCheckoutRequest req = new FindCheckoutRequest();
            req.account_id = WePayConfiguration.accountId;
            

            Checkout chk = new Checkout();
            var response = await chk.FindCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutCreated"] = response;

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

        public async Task<string> CancelCheckoutAsync()
        {
            CancelCheckoutRequest req = new CancelCheckoutRequest();
            req.checkout_id = 000000;
            req.cancel_reason = "End the dev test";


            Checkout chk = new Checkout();
            var response = await chk.CancelCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutCanceled"] = response;

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

        //return a full refund leave the 'amount' parameter (not used here) empty.
        //https://www.wepay.com/developer/reference/checkout#refund
        public async Task<string> RefundCheckoutAsync()
        {
            RefundCheckoutRequest req = new RefundCheckoutRequest();
            //Checkout must be in "captured" state. 
            req.checkout_id = 000000;
            req.refund_reason = "wrong color";

            Checkout chk = new Checkout();
            var response = await chk.RefundCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CheckoutRefunded"] = response;

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

        public async Task<string> CaptureCheckoutAsync()
        {
            CaptureCheckoutRequest req = new CaptureCheckoutRequest();
            req.checkout_id = 000000;

            Checkout chk = new Checkout();
            var response = await chk.CaptureCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CapturedCheckout"] = response;

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

        public async Task<string> ModifyCheckoutAsync()
        {
            ModifyCheckoutRequest req = new ModifyCheckoutRequest();
            req.checkout_id = 000000;

            Checkout chk = new Checkout();
            var response = await chk.ModifyCheckoutAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CapturedCheckout"] = response;

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