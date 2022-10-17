namespace SproutSocial.Application.Helpers;

public static class Validators
{
    public static bool IsGuid(string id)
    {
        return Guid.TryParse(id, out var topicId);
    }
}
