using System;

namespace AccountCalculator.Domain
{
    public readonly struct UtcDateTime : IComparable<UtcDateTime>, IComparable
    {
        private readonly long _ticks;

        public static UtcDateTime Now => new (DateTimeOffset.UtcNow.Ticks);

        public UtcDateTime(int year, int month, int day)
            : this(new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero).Ticks)
        {
        }
        public UtcDateTime(int year, int month, int day, int hour, int minute, int second)
            : this(new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.Zero).Ticks)
        {
        }
        public UtcDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
            : this(new DateTimeOffset(year, month, day, hour, minute, second, millisecond, TimeSpan.Zero).Ticks)
        {
        }
        public UtcDateTime(DateTimeOffset dateTimeOffset) : this() => _ticks = dateTimeOffset.UtcTicks;
        private UtcDateTime(long ticks) : this() => _ticks = ticks;

        public override string ToString() => ToDateTimeOffset().ToString();
        public string ToString(string format) => ToDateTimeOffset().ToString(format);

        public DateTimeOffset ToDateTimeOffset() => this;

        public static implicit operator DateTimeOffset(UtcDateTime utcDateTime) =>
            new (utcDateTime._ticks, TimeSpan.Zero);

        public static implicit operator UtcDateTime(DateTimeOffset dateTimeOffset) => new (dateTimeOffset.Ticks);

        public static implicit operator DateTime(UtcDateTime utcDateTime) =>
            new (utcDateTime._ticks, DateTimeKind.Utc);

        public int CompareTo(UtcDateTime other) => _ticks.CompareTo(other._ticks);

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is UtcDateTime other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(UtcDateTime)}");
        }

        public static bool operator <(UtcDateTime left, UtcDateTime right) => left._ticks < right._ticks;

        public static bool operator >(UtcDateTime left, UtcDateTime right) => left._ticks > right._ticks;

        public static bool operator <=(UtcDateTime left, UtcDateTime right) => left._ticks <= right._ticks;

        public static bool operator >=(UtcDateTime left, UtcDateTime right) => left._ticks >= right._ticks;
    }
}
