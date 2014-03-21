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
    public class SubscriptionController : BaseController
    {
        //NOTICE the 'subscription_id' parameter on the GetSubscriptionAsync(), ModifySubscriptionAsync(), CancelSubscriptionAsync()
        //action methods, it is set to 00000 because it can only be set AFTER creating a subscription object
        //which returns a 'subscription_id'. MUST CREATE 'SUBCRIPTION PLAN' BEFORE CREATING A 'SUBSCRIPTION'.
        //To test these calls, first create a subscription object, copy the returned 'subscription_id' and past in the above action methods.
        public SubscriptionController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetSubscriptionAsync()
        {
            GetSubscriptionRequest req = new GetSubscriptionRequest();
            req.subscription_id = 00000;

            Subscription sub = new Subscription();
            var response = await sub.GetSubscriptionAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Subscription"] = response;

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

        //NOTICE the 'subscription_plan_id' will NOT be the same as the 'subscription_plan'
        //This associates a subscription with a particular plan.
        //So to create a 'subscription', you MUST create a 'subscription plan' first.
        public async Task<string> FindSubscriptionAsync()
        {
            FindSubscriptionRequest req = new FindSubscriptionRequest();
            req.subscription_plan_id = 000000;

            Subscription sub = new Subscription();
            var response = await sub.FindSubscriptionAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Subscriptions"] = response;

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

        //MUST CREATE 'SUBCRIPTION PLAN' BEFORE CREATING A 'SUBSCRIPTION'.
        public async Task<string> CreateSubscriptionAsync()
        {
            CreateSubscriptionRequest req = new CreateSubscriptionRequest();
            req.subscription_plan_id = 000000;

            Subscription sub = new Subscription();
            var response = await sub.CreateSubscriptionAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["SubscriptionCreated"] = response;

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

        public async Task<string> ModifySubscriptionAsync()
        {
            ModifySubscriptionRequest req = new ModifySubscriptionRequest();
            req.subscription_plan_id = 000000;
            req.subscription_id = 000000;

            Subscription sub = new Subscription();
            var response = await sub.ModifySubscriptionAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["UpdatedSubscription"] = response;

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

        public async Task<string> CancelSubscriptionAsync()
        {
            CancelSubscriptionRequest req = new CancelSubscriptionRequest();
            req.subscription_id = 000000;

            Subscription sub = new Subscription();
            var response = await sub.CancelSubscriptionAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CanceledSubscription"] = response;

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