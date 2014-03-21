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
    public class SubscriptionPlanController : BaseController
    {
        //NOTICE the 'subscription_plan_id' parameter on the GetSubscriptionPlanAsync(), ModifySubscriptionPlanAsync(), DeleteSubscriptionPlanAsync()
        //action methods, it is set to 00000 because it can only be set AFTER creating a subscription plan object
        //which returns a 'subscription_plan_id'.
        //To test these calls, first create a subscription plan object, copy the returned 'subscription_plan_id' and past in the above action methods.
        public SubscriptionPlanController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetSubscriptionPlanAsync()
        {
            GetSubscriptionPlanRequest req = new GetSubscriptionPlanRequest();
            req.subscription_plan_id = 00000;

            SubscriptionPlan sp = new SubscriptionPlan();
            var response = await sp.GetSubscriptionPlanAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionPlan"] = response;

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

        public async Task<string> FindSubscriptionPlanAsync()
        {
            FindSubscriptionPlanRequest req = new FindSubscriptionPlanRequest();
            req.account_id = WePayConfiguration.accountId;

            SubscriptionPlan sp = new SubscriptionPlan();
            var response = await sp.FindSubscriptionPlanAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionPlans"] = response;

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

        public async Task<string> CreateSubscriptionPlanAsync()
        {
            CreateSubscriptionPlanRequest req = new CreateSubscriptionPlanRequest();
            req.account_id = WePayConfiguration.accountId;
            req.name = "Test Subscription";
            req.short_description = "A short description.";
            req.amount = 3.33M;
            //The current period of time for each subscription. Must be a string: "weekly", "monthly", "yearly", or "quarterly".
            req.period = "monthly";

            SubscriptionPlan sp = new SubscriptionPlan();
            var response = await sp.CreateSubscriptionPlanAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CreatedSubscriptionPlan"] = response;

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

        //Modifies subscription plan. ATTENTION: some cases require 'reauthorization' and setting of 'update_subscriptions' parameter. https://www.wepay.com/developer/reference/subscription_plan#modify
        public async Task<string> ModifySubscriptionPlanAsync()
        {
            ModifySubscriptionPlanRequest req = new ModifySubscriptionPlanRequest();
            req.subscription_plan_id = 00000;
           
            SubscriptionPlan sp = new SubscriptionPlan();
            var response = await sp.ModifySubscriptionPlanAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["UpdatedSubscriptionPlan"] = response;

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

        //Deletes the subscription plan. Existing subscriptions to the plan will still be active, but there will be no new subscriptions to the plan. 
        public async Task<string> DeleteSubscriptionPlanAsync()
        {
            DeleteSubscriptionPlanRequest req = new DeleteSubscriptionPlanRequest();
            req.subscription_plan_id = 00000;
            req.reason = "no money.";

            SubscriptionPlan sp = new SubscriptionPlan();
            var response = await sp.DeleteSubscriptionPlanAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionPlanDeleted"] = response;

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