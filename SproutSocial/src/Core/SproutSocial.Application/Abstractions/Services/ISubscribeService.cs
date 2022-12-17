namespace SproutSocial.Application.Abstractions.Services;

public interface ISubscribeService
{
    Task<bool> SubscribeAsync(string email);
    Task<bool> UnsubscribeAsync(string email);
}
