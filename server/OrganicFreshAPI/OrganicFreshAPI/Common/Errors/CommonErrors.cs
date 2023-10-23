using ErrorOr;

namespace OrganicFreshAPI.Common.Errors;

public static partial class CommonErrors
{
    public static Error DbSaveError = Error.Unexpected(
        code: "Common.DbSaveError",
        description: "Unexpected error while saving data"
    );

    public static Error ImageUploadError = Error.Unexpected(
        code: "Common.ImageUploadError",
        description: "Unexpected error while uploading the image"
    );

    public static Error ImageDeleteError = Error.Unexpected(
        code: "Common.ImageDeleteError",
        description: "Unexpected error while deleting the image"
    );
}