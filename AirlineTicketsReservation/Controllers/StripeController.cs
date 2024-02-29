using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Stripe;
using System.Diagnostics;
using AirlineTicketsReservation.Models;
using AirlineTicketsReservation.Controllers;

namespace AirlineTicketsReservation.Controllers
{
    public class StripeController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public StripeController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            

        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CreateCheckoutSession(string shuma)
        {

            var currency = "usd"; // Currency code
            var successUrl = "https://localhost:7038/Home";
            var cancelUrl = "https://localhost:7038/Stripe/cancel";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(shuma) * 100,  // Amount in smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Total shuma e pageses:",
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);


            return Redirect(session.Url);
        }

        public IActionResult success()
        {

            return View();
        }



        public IActionResult cancel()
        {
            return View("List");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}