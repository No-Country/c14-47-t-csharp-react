using ErrorOr;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Data;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;
using Stripe.Checkout;

namespace OrganicFreshAPI.DataService.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly IAuthenticationRepository _authRepository;
    private readonly IProductRepository _productRepository;
    private readonly MyDbContext _context;
    private readonly ISaleRepository _saleRepository;
    private readonly IConfiguration _configuration;

    public CheckoutRepository(IAuthenticationRepository authRepository, IProductRepository productRepository, ISaleRepository saleRepository, IConfiguration configuration, MyDbContext context)
    {
        _authRepository = authRepository;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _configuration = configuration;
        _context = context;
    }

    public async Task<ErrorOr<CreateCheckoutResponse>> CreateCheckout(List<CreateCheckoutRequest> listOfProducts)
    {
        // Get user
        var userResult = await _authRepository.UserDetails();
        var lineItems = new List<SessionLineItemOptions>();
        if (userResult.IsError && userResult.FirstError == ApiErrors.Authentication.UserNotFoundInRequest)
            return ApiErrors.Authentication.UserNotFoundInRequest;
        // Create sale
        var saleResult = await _saleRepository.CreateSale(userResult.Value.id);
        if (saleResult.IsError)
            return CommonErrors.DbSaveError;
        foreach (var productCheckout in listOfProducts)
        {
            var product = await _productRepository.GetProductById(productCheckout.productId);
            if (product.IsError)
                return ApiErrors.Product.InvalidProduct;
            // Create checkout details
            var createCheckoutResult = await _saleRepository.CreateCheckoutDetails(saleResult.Value.Id, productCheckout.productId, productCheckout.quantity);
            if (createCheckoutResult.IsError && createCheckoutResult.FirstError == CommonErrors.DbSaveError)
                return CommonErrors.DbSaveError;

            var updateProductStockResult = await _productRepository.UpdateProductStock(productCheckout.productId, -productCheckout.quantity);

            if (updateProductStockResult.IsError)
                return updateProductStockResult.FirstError;

            lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)(product.Value.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Value.Name,
                        Description = "OrganicFreshProduct"
                    }
                },
                Quantity = (long)productCheckout.quantity
            });
        }
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = _configuration["StripeUrls:SuccessUrl"]+ saleResult.Value.Id,
            CancelUrl = _configuration["StripeUrls:CancelUrl"]
        };

        var service = new SessionService();
        try
        {
            var session = service.Create(options);
            saleResult.Value.PaymentId = session.Id;
            await _context.SaveChangesAsync();
            return new CreateCheckoutResponse(session.Url);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return CommonErrors.UnexpectedError;
        }


    }
}
