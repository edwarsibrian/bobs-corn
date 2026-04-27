using MediatR;

namespace BobsCorn.Application.Corn.BuyCorn
{
    public sealed record BuyCornCommand(string ClientId) : IRequest<BuyCornResult>;
}
