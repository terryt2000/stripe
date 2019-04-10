using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe.Models;

namespace Stripe.Controllers
{
    public class PledgeController : Controller
    {
        // GET: Pledge
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Charge(ChargeModel model)
        {
            var result = await MakeStripeCharge(model);
            if (result.Succeeded)
            {
                ViewBag.Message = $"Your pledge of {model.PledgeAmount:C} succeed.  Thank you!";
            }
            else
            {
                ViewBag.ErrorMessage = $"There was a problem processing your pledge";
            }
            return View("Index");
        }

        private async Task<StripeResult> MakeStripeCharge(ChargeModel model)
        {
            var chargeSvc = new ChargeService("Your Secret API Key");
            var options = new ChargeCreateOptions();
            options.Amount = Convert.ToInt64(model.PledgeAmount * 100m);
            options.SourceId = model.StripeToken;
            options.Currency = "usd";
            options.Description = "Pledge";
            var charge = await chargeSvc.CreateAsync(options);
            return new StripeResult
            {
                Succeeded = charge.Status == "succeeded"
            };
        }
    }

    internal class StripeResult
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}