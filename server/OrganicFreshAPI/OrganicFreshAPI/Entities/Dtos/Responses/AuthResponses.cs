namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record RegisterResponse(
    string jwt
);

public record LoginResponse(
    string jwt
);

public record UserDetailsResponse(
    string name,
    DateTime createdAt,
    DateTime modifiedAt,
    DateTime deletedAt,
    string id,
    string email
);