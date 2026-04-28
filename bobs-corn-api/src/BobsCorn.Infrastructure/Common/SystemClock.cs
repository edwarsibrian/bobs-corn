using BobsCorn.Application.Clock;

namespace BobsCorn.Infrastructure.Common
{
    public sealed class SystemClock : IClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
