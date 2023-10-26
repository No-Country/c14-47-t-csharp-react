using ErrorOr;

namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record RegisterResponse(
    string jwt,
    bool isAdmin
);

public record LoginResponse(
    string jwt,
    bool isAdmin
);

public record UserDetailsResponse(
    string name,
    DateTime createdAt,
    DateTime modifiedAt,
    DateTime deletedAt,
    string id,
    string email,
    List<SaleAndCheckoutDetails> sales
);