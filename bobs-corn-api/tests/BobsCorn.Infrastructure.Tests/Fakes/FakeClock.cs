using BobsCorn.Application.Clock;

namespace BobsCorn.Infrastructure.Tests.Fakes
{
    public sealed class FakeClock : IClock
    {
        public DateTimeOffset UtcNow { get; private set; }

        public FakeClock(DateTimeOffset utcNow)
        {
            UtcNow = utcNow;
        }

        public void Advance(TimeSpan timeSpan)
        {
            UtcNow = UtcNow.Add(timeSpan);
        }
    }
}
