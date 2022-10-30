using Microsoft.AspNetCore.Http;

namespace SproutSocial.Infrastructure.Services.Common;

public class BaseUrlAccessor : IBaseUrlAccessor
{
    private readonly IHttpContextAccessor _httpContextAcessor;

    public BaseUrlAccessor(IHttpContextAccessor httpContextAcessor)
    {
        _httpContextAcessor = httpContextAcessor;
    }

    public string BaseUrl => $"{_httpContextAcessor?.HttpContext?.Request.Scheme}://" +
        $"{_httpContextAcessor?.HttpContext?.Request.Host}{_httpContextAcessor?.HttpContext?.Request.PathBase}";
}
