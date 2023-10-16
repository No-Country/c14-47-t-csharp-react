namespace OrganicFreshAPI.Entities.Dtos.Requests;

public record CreateCategoryRequest(
    string name
);

public record UpdateCategoryRequest(
    string Id,
    string name
);

public record DeleteCategoryRequest(
    string Id
);