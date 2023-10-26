using ErrorOr;
using OrganicFreshAPI.Common.Errors;
using OrganicFreshAPI.DataService.Repositories.Interfaces;
using OrganicFreshAPI.Entities.DbSet;
using OrganicFreshAPI.Entities.Dtos.Requests;
using OrganicFreshAPI.Entities.Dtos.Responses;
using Stripe.Checkout;

namespace OrganicFreshAPI.DataService.Repositories;

public class CheckoutRepository : ICheckoutRepository
{
    private readonly IAuthenticationRepository _authRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;

    public CheckoutRepository(IAuthenticationRepository authRepository, IProductRepository productRepository, ISaleRepository saleRepository)
    {
        _authRepository = authRepository;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
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
            Console.WriteLine(saleResult.Value.ToString());
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
                    UnitAmount = (long)product.Value.Price,
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
            SuccessUrl = "http://localhost:5020/success",
            CancelUrl = "http://localhost:5020/cancel"
        };

        var service = new SessionService();
        try
        {
            var session = service.Create(options);
            return new CreateCheckoutResponse(session.Url);
        }
        catch (Exception ex)
        {
            return CommonErrors.UnexpectedError;
        }


    }
}
