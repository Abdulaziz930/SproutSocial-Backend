using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Subscribes;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Persistence.Services;

public class SubscribeService : ISubscribeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public SubscribeService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<bool> SubscribeAsync(string email)
    {
        ArgumentNullException.ThrowIfNull(email);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (user.IsAuthenticated)
        {
            var dbUser = await _userManager.FindByNameAsync(user.Name);
            if (dbUser.Email != email)
                throw new WrongSubscribeEmailException();

            bool isExist = await _unitOfWork.SubscribeReadRepository.IsExistsAsync(s => s.Email == email);
            if (isExist)
                throw new SubscribedEmailAlreadyExist();

            bool result = await _unitOfWork.SubscribeWriteRepository.AddAsync(new() { Email = email});
            await _unitOfWork.SaveAsync();

            return result;
        }

        throw new AuthorizationException("Subscribe");
    }

    public async Task<bool> UnsubscribeAsync(string email)
    {
        ArgumentNullException.ThrowIfNull(email);

        var user = _httpContextAccessor.HttpContext.User.Identity;
        if (user.IsAuthenticated)
        {
            var dbUser = await _userManager.FindByNameAsync(user.Name);
            if (dbUser.Email != email)
                throw new WrongSubscribeEmailException();

            var subsribe = await _unitOfWork.SubscribeReadRepository.GetSingleAsync(s => s.Email == email);
            if (subsribe is null)
                throw new SubscribedEmailNotFound();

            var result = _unitOfWork.SubscribeWriteRepository.Remove(subsribe);
            await _unitOfWork.SaveAsync();

            return result;
        }

        throw new AuthorizationException("Unsubscribe");
    }
}
