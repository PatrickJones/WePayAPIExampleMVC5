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
    public class WithdrawalController : BaseController
    {
        //NOTICE the 'withdrawal_id' parameter on the GetWithdrawalAsync() and ModifyWithdrawalAsync()
        //action methods, it is set to 00000 because it can only be set AFTER creating a withdrawal object
        //which returns a 'withdrawal_id'.
        //To test these calls, first create a withdrawal object, copy the returned 'withdrawal_id' and past in the above action methods.
        public WithdrawalController(IGlobalVariables gVars)
            : base(gVars)
        { }

        public async Task<string> GetWithdrawalAsync()
        {
            GetWithdrawalRequest req = new GetWithdrawalRequest();
            req.withdrawal_id = 000000;

            Withdrawal wd = new Withdrawal();
            var response = await wd.GetWithdrawalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Withdrawal"] = response;

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

        public async Task<string> FindWithdrawalAsync()
        {
            FindWithdrawalRequest req = new FindWithdrawalRequest();
            req.account_id = WePayConfiguration.accountId;

            Withdrawal wd = new Withdrawal();
            var response = await wd.FindWithdrawalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["Withdrawal"] = response;

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

        public async Task<string> CreateWithdrawalAsync()
        {
            CreateWithdrawalRequest req = new CreateWithdrawalRequest();
            req.account_id = WePayConfiguration.accountId;
            req.note = "just taking out a little change";

            Withdrawal wd = new Withdrawal();
            var response = await wd.CreateWithdrawalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["WithdrawalCreated"] = response;

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

        public async Task<string> ModifyWithdrawalAsync()
        {
            ModifyWithdrawalRequest req = new ModifyWithdrawalRequest();
            req.withdrawal_id = 000000;

            Withdrawal wd = new Withdrawal();
            var response = await wd.ModifyWithdrawalAsync(req);

            if (response.ErrorResponse != null)
            {
                ViewData["Error"] = response.ErrorResponse;
            }

            ViewData["ModifiedWithdrawal"] = response;

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