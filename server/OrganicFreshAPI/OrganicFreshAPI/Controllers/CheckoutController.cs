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
    private readonly MyDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ICheckoutRepository _checkoutRepository;

    public CheckoutController(MyDbContext context, IConfiguration configuration, ICheckoutRepository checkoutRepository)
    {
        _context = context;
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
        _checkoutRepository = checkoutRepository;
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
}