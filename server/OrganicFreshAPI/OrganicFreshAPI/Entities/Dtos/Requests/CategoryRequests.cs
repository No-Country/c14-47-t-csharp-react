namespace OrganicFreshAPI.Entities.Dtos.Requests;

public record CreateCategoryRequest(
    string name,
    string? imageUrl
);

public record UpdateCategoryRequest(
    string? name,
    string? imageUrl
);
