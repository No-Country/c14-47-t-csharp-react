namespace OrganicFreshAPI.Entities.Dtos.Requests;

public record CreateCategoryRequest(
    string name,
    IFormFile? image
);

public record UpdateCategoryRequest(
    string? name,
    IFormFile? image,
    bool? active
);
