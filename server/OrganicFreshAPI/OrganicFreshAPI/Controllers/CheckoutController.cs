using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.DataService.Data;
using Stripe;
using Stripe.Checkout;

namespace OrganicFreshAPI.Controllers;

[Route("[controller]")]
public class CheckoutController : ApiController
{
    private readonly MyDbContext _context;
    private readonly IConfiguration _configuration;

    public CheckoutController(MyDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
    }

    [HttpPost]
    public async Task<IActionResult> CreateCheckoutSession(int productId, decimal quantity)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)product.Price,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Name,
                        Description = "fruits"
                    }
                },
                Quantity = (long)quantity
            },
        },
            Mode = "payment",
            SuccessUrl = "http://localhost:5020/success",
            CancelUrl = "http://localhost:5020/cancel"
        };

        var service = new SessionService();
        var session = service.Create(options);
        return Ok(new { sessionId = session.Id, sessionUrl = session.Url });
    }
}