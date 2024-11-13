using Core.Entities.OrderEntities;
using Demo.API.HandleResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using Services.PaymentServices;
using Stripe;

namespace Demo.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger _logger;
        private const string WhSecret = "whsec_6aa7bf5578f08c6613fdc619ddc933393212fa7db44b753ef02df0e142ac849c";

        public PaymentController(IPaymentServices paymentServices, ILogger logger)
        {
            _paymentServices = paymentServices;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentServices.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null)
                return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], WhSecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed ", paymentIntent.Id);
                    order = await _paymentServices.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Failed ", order.Id);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded ", paymentIntent.Id);
                    order = await _paymentServices.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Succeeded ", order.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}
