namespace SproutSocial.Application.Features.Queries.GoogleAuthenticator;

public class GAuthSetupQueryRequest : IRequest<GAuthSetupQueryResponse>
{
    public string Email { get; set; } = null!;
}