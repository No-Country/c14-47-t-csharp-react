using CloudinaryDotNet.Actions;

namespace OrganicFreshAPI.Helpers;

public interface IImageService
{
    Task<ImageUploadResult> AddImageAsync(IFormFile file);
    Task<DeletionResult> DeleteImageAsync(string publicId);
}