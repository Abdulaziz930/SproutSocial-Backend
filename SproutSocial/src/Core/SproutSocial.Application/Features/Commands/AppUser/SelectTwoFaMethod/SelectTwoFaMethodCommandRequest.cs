namespace SproutSocial.Application.Features.Commands.AppUser.SelectTwoFaMethod;

public class SelectTwoFaMethodCommandRequest : IRequest<SelectTwoFaMethodCommandResponse>
{
    public string UserId { get; set; } = null!;
    public string TwoFaMethodId { get; set; } = null!;
}