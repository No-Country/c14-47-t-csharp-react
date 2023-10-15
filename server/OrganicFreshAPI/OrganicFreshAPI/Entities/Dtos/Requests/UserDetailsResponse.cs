namespace OrganicFreshAPI.Entities.Dtos.Requests;

public record UserDetailsResponse(
    string name,
    DateTime createdAt,
    DateTime modifiedAt,
    DateTime deletedAt,
    string id,
    string email
);
