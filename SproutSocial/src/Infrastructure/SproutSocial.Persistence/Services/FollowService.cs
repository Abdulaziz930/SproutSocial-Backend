using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SproutSocial.Application.Abstractions.Services;
using SproutSocial.Application.Exceptions.Authentication;
using SproutSocial.Application.Exceptions.Users;
using SproutSocial.Domain.Entities.Identity;
using System.Net;
using SproutSocial.Application.Helpers.Extesions;

namespace SproutSocial.Persistence.Services;

public class FollowService : IFollowService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FollowService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> FollowRequestAsync(string followingName, string followedName)
    {
        followingName.ThrowIfNullOrWhiteSpace("Follower username cannot be null or whitespace");
        followedName.ThrowIfNullOrWhiteSpace("Followed username cannot be null or whitespace");

        var isLoggedInUser = _httpContextAccessor.HttpContext.User.Identity.Name == followingName;
        if (!isLoggedInUser) throw new AuthorizationException("User not logged in", HttpStatusCode.Unauthorized);

        var followingUser = await _userManager.FindByNameAsync(followingName);
        if (followingUser is null)
            throw new UserNotFoundException("UserName", followingName);

        var followedUser = await _userManager.FindByNameAsync(followedName);
        if (followedUser is null)
            throw new UserNotFoundException("UserName", followedName);

        bool isFollowing = await IsFollowingAsync(followingName, followedName);
        if (isFollowing)
            throw new UserAlreadyFollowingException(followingName, followedName);

        if (followingName == followedName) throw new UserFollowException();

        UserFollow userFollow = new()
        {
            FollowedUser = followedUser,
            FollowingUser = followingUser,
            IsConfirmed = false
        };

        bool result = await _unitOfWork.FollowingWriteRepository.AddAsync(userFollow);
        await _unitOfWork.SaveAsync();

        return result;
    }

    public async Task<bool> AcceptOrDeclineFollowRequestAsync(bool acceptOrDeclineFollowRequest, string followedName
        , string followingName)
    {
        followedName.ThrowIfNullOrWhiteSpace("Followed username cannot be null or whitespace");

        var isLoggedInUser = _httpContextAccessor.HttpContext.User.Identity.Name == followedName;
        if (!isLoggedInUser) throw new AuthorizationException("User not logged in", HttpStatusCode.Unauthorized);

        var userFollow = await _unitOfWork.FollowingReadRepository.GetSingleAsync(f => f.FollowedUser.UserName == followedName &&
             f.FollowingUser.UserName == followingName && !f.IsConfirmed, tracking: true, "FollowingUser", "FollowedUser");
        if (userFollow is null)
            throw new UserNotFoundException("UserName", followingName);

        if (acceptOrDeclineFollowRequest)
        {
            userFollow.IsConfirmed = true;
            _unitOfWork.FollowingWriteRepository.Update(userFollow);
        }
        else
        {
            _unitOfWork.FollowingWriteRepository.Remove(userFollow);
        }

        await _unitOfWork.SaveAsync();

        return acceptOrDeclineFollowRequest;
    }

    public async Task<bool> UnFollowAsync(string followingName, string followedName)
    {
        followingName.ThrowIfNullOrWhiteSpace("Follower username cannot be null or whitespace");
        followedName.ThrowIfNullOrWhiteSpace("Followed username cannot be null or whitespace");

        var isLoggedInUser = _httpContextAccessor.HttpContext.User.Identity.Name == followingName;
        if (!isLoggedInUser) throw new AuthorizationException("User not logged in", HttpStatusCode.Unauthorized);

        var userFollow = await _unitOfWork.FollowingReadRepository.GetSingleAsync(f => f.FollowingUser.UserName == followingName &&
             f.FollowedUser.UserName == followedName, tracking: true, "FollowingUser", "FollowedUser");
        if (userFollow is null)
            throw new FollowerNotFoundException(followingName, followedName);

        var result = _unitOfWork.FollowingWriteRepository.Remove(userFollow);
        await _unitOfWork.SaveAsync();

        return result;
    }

    private async Task<bool> IsFollowingAsync(string followingName, string followedName)
    {
        return await _unitOfWork.FollowingReadRepository.IsExistsAsync(f => f.FollowingUser.UserName == followingName &&
          f.FollowedUser.UserName == followedName, "FollowingUser", "FollowedUser");
    }
}
