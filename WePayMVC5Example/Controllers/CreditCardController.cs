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
    /// <summary>
    /// IMPORTANT: You need to have Tokenization permission from WePay to directly send credit card info
    /// https://www.wepay.com/developer/tutorial/tokenization
    /// </summary>
    public class CreditCardController : BaseController
    {
        public CreditCardController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetCreditCardAsync()
        {
            GetCreditCardRequest req = new GetCreditCardRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;
            req.credit_card_id = 000;

            CreditCard cc = new CreditCard();
            var response = await cc.GetCreditCardAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CreditCard"] = response;

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

        public async Task<string> CreateCreditCardAsync()
        {
            //create address object
            var adds = new AddressStructure()
            {
                address1 = "4102 E Woodrow Place",
                address2 = "",
                city = "Vancouver",
                state = "WA",
                zip = "98685",
                country = "US"
            };

            //convert it to JSON object per WEPAY documentation.
            var addressObject = adds.ToJSON();

            CreateCreditCardRequest req = new CreateCreditCardRequest();
            req.client_id = WePayConfiguration.clientId;
            //this is a test card number from WePay documentation: https://www.wepay.com/developer/reference/testing
            req.cc_number = "4003830171874018";
            req.cvv = 333;
            req.expiration_month = 06;
            req.expiration_year = 2016;
            req.user_name = "VISA Test CC";
            req.email = "testemail@gmail.com";
            req.address = addressObject;
            req.original_ip = "<IP_ADDRESS>";
            //get your device user-agent from here http://whatsmyuseragent.com/ & it will give show your IP address!
            req.original_device = "<USER-AGENT>";
            

            CreditCard cc = new CreditCard();
            var response = await cc.CreateCreditCardAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CreatedCreditCard"] = response;

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

        public async Task<string> AuthorizeCreditCardAsync()
        {
            AuthorizeCreditCardRequest req = new AuthorizeCreditCardRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;
            req.credit_card_id = 000;

            CreditCard cc = new CreditCard();
            var response = await cc.AuthorizeCreditCardAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["AuthorizedCreditCard"] = response;

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

        public async Task<string> FindCreditCardAsync()
        {
            FindCreditCardRequest req = new FindCreditCardRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;

            CreditCard cc = new CreditCard();
            var response = await cc.FindCreditCardAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["CreditCards"] = response;

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

        public async Task<string> DeleteCreditCardAsync()
        {
            DeleteCreditCardRequest req = new DeleteCreditCardRequest();
            req.client_id = WePayConfiguration.clientId;
            req.client_secret = WePayConfiguration.clientSecret;
            req.credit_card_id = 000;

            CreditCard cc = new CreditCard();
            var response = await cc.DeleteCreditCardAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["DeletedCreditCard"] = response;

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