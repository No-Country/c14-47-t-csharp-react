using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;
using Stripe;
using Stripe.Checkout;

namespace OrganicFreshAPI.Controllers;

[Route("[controller]")]
public class CheckoutController : ApiController
{
    private readonly IConfiguration _configuration;
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly ISaleRepository _saleRepository;

    public CheckoutController(IConfiguration configuration, ICheckoutRepository checkoutRepository, ISaleRepository saleRepository)
    {
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
        _checkoutRepository = checkoutRepository;
        _saleRepository = saleRepository;
    }

    [Authorize(Policy = "StandardRights")]
    [HttpPost]
    public async Task<IActionResult> CreateCheckoutSession(List<CreateCheckoutRequest> request)
    {
        var checkoutResult = await _checkoutRepository.CreateCheckout(request);

        if (checkoutResult.IsError && checkoutResult.FirstError == ApiErrors.Product.InvalidCategory)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: checkoutResult.FirstError.Description
            );

        if (checkoutResult.IsError && checkoutResult.FirstError == ApiErrors.Product.NotEnoughStock)
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: checkoutResult.FirstError.Description
            );

        if (checkoutResult.IsError && checkoutResult.FirstError == ApiErrors.Authentication.UserNotFoundInRequest)
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: checkoutResult.FirstError.Description
            );

        if (checkoutResult.IsError && checkoutResult.FirstError == CommonErrors.DbSaveError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: checkoutResult.FirstError.Description
            );

        if (checkoutResult.IsError && checkoutResult.FirstError == CommonErrors.UnexpectedError)
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: checkoutResult.FirstError.Description
            );

        return Ok(checkoutResult.Value);
    }


    [HttpPost("webhook")]
    public async Task<IActionResult> Index()
    {
        string? endpointSecret = _configuration["StripeSettings:WebhookSecret"];

        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            Event stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointSecret);
            var response = stripeEvent.Data.Object;

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                Session session = (Session)response;

                var saleResult = await _saleRepository.ConfirmPay(session.Id);

                if (saleResult.IsError && saleResult.FirstError == ApiErrors.Sale.InvalidPaymentId)
                    return Problem(
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: saleResult.FirstError.Description
                    );

                return Ok();
            }
            if (stripeEvent.Type == Events.CheckoutSessionExpired)
            {
                Session session = (Session)response;
                var saleResult = await _saleRepository.DeleteSale(session.Id);
                if (saleResult.IsError && saleResult.FirstError == CommonErrors.DbSaveError)
                    return Problem(
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: saleResult.FirstError.Description
                    );
            }
            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
    }
}