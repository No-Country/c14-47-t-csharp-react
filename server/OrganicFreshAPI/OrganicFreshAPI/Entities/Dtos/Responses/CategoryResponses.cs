using OrganicFreshAPI.Entities.DbSet;

namespace OrganicFreshAPI.Entities.Dtos.Responses;

public record GetCategoriesResponse(List<Category> categories);
public record CreateCategoryResponse(Category category);
public record UpdateCategoryResponse(Category updatedCategory);
