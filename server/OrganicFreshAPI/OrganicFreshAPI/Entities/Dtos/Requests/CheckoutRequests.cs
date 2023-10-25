namespace OrganicFreshAPI.Entities.Dtos.Requests;

public record CreateCheckoutRequest(
    int productId,
    decimal quantity
);