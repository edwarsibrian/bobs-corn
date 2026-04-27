namespace BobsCorn.Application.Clock
{
    public interface IClock
    {
        DateTimeOffset UtcNow { get; }
    }
}
