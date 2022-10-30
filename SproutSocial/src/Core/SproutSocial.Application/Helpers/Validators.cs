using Microsoft.AspNetCore.Http;

namespace SproutSocial.Application.Helpers;

public static class Validators
{
    public static bool IsGuid(string id)
    {
        return Guid.TryParse(id, out var topicId);
    }
    public static bool IsImage(IFormFile formFile)
    {
        return formFile.ContentType.Contains("image/");
    }

    public static bool IsSizeAllowed(IFormFile formFile, int kb)
    {
        return formFile.Length < kb * 1000;
    }

}
