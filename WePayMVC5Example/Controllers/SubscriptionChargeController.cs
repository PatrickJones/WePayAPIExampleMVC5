using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WePayMVC5Example.IoC;
using WePayMVC5HttpClientSDK.WePayAPITypes;

namespace WePayMVC5Example.Controllers
{
    public class SubscriptionChargeController : BaseController
    {
        //Note that there is no '/subscription_charge/create' call.
        //That is because it is created automatically when a monthly 'subscription' payment is made.
        //So you have to lookup subscription charges based on 'subscription_id' (See FindSubcriptionChargeAsync() on Controller) 
        //and select from that returned array the 'subscription_charge_id' you want to view or refund.

        //Testing this Controllers' actions requires a few detailed steps...
        //First, you have to create a "Subscription Plan" and set its 'amount' proprty to '22.61M' & its 'period' property to 'weekly' and invoke the '/subscription_plan/create' call,
        //secondly, you create a "Subscription" for that Plan using the 'subscription/create' call (unfortunately you have to wait exactly 5 minutes, while it simulates a 'weekly period' payment (instead of waitind an actual week)),
        //third, invoke the '/subscription_charge/find' call, which is the 'FindSubcriptionChargeAsync()' method on this controller.
        //finally, you can get the 'subscription_charge_id' to test the 'GetSubcriptionChargeAsync()' & 'RefundSubscriptionChargeAsync()' action methods
        public SubscriptionChargeController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetSubcriptionChargeAsync()
        {
            GetSubscriptionChargeRequest req = new GetSubscriptionChargeRequest();
            req.subscription_charge_id = 0000;

            SubscriptionCharge sc = new SubscriptionCharge();
            var response = await sc.GetSubscriptionChargeAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionCharge"] = response;

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

        public async Task<string> FindSubcriptionChargeAsync()
        {
            FindSubscriptionChargeRequest req = new FindSubscriptionChargeRequest();
            req.subscription_id = 0000;

            SubscriptionCharge sc = new SubscriptionCharge();
            var response = await sc.FindSubscriptionChargeAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionCharges"] = response;

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

        public async Task<string> RefundSubscriptionChargeAsync()
        {
            RefundSubscriptionChargeRequest req = new RefundSubscriptionChargeRequest();
            req.subscription_charge_id = 0000;
            req.refund_reason = "mistake";

            SubscriptionCharge sc = new SubscriptionCharge();
            var response = await sc.RefundSubscriptionChargeAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["RefundedSubscriptionCharge"] = response;

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