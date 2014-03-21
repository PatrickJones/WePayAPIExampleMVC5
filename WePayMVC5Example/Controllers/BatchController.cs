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
    public class BatchController : BaseController
    {
        public BatchController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> CreateBatchCallAsync()
        {
            //create batch call objects...just two for test, max is 50
            //each 'BatchCall()' instance can only assign a single call 
            //MUST KNOW which parameters are required for each different call object

            //first call object
            //setting 'parameters' property because this specific call requires authorization. See comments on BatchCall class
            var batchA = new BatchCall()
            {
                call = WePayUrls.WePayActionUrls["GetAppRequest"],
                authorization = WePayConfiguration.accessToken,
                parameters = new Dictionary<string, object>()
                { 
                    { "client_id", WePayConfiguration.clientId }, 
                    { "client_secret", WePayConfiguration.clientSecret }
                }
            };
            
            //second call object
            var batchB = new BatchCall()
            {
                call = WePayUrls.WePayActionUrls["GetUserRequest"],
                authorization = WePayConfiguration.accessToken,
            };

            //create the actual Batch Call request
            CreateBatchRequest req = new CreateBatchRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;
            //set 'calls' property with list of BatchCall objects created above
            req.calls = new List<BatchCall>()
            {
                batchA, batchB
            };

            Batch bat = new Batch();
            var response = await bat.CreateBatchAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["BatchResponses"] = response;

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