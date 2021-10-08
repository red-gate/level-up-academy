using System;

namespace AccountCalculator.Domain
{
    public readonly struct UtcDateTime : IComparable<UtcDateTime>
    {
        private readonly long _ticks;

        public UtcDateTime(int year, int month, int day) : this(
            new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero).UtcTicks) { }

        public UtcDateTime(DateTimeOffset timeOfConversion) : this(timeOfConversion.UtcTicks) { }

        public UtcDateTime AddDays(int i) => new (_ticks + TimeSpan.FromDays(i).Ticks);

        private UtcDateTime(long ticks) => _ticks = ticks;

        public int CompareTo(UtcDateTime other) => _ticks.CompareTo(other._ticks);

        public static bool operator <(UtcDateTime left, UtcDateTime right) => left.CompareTo(right) < 0;

        public static bool operator >(UtcDateTime left, UtcDateTime right) => left.CompareTo(right) > 0;

        public static bool operator <=(UtcDateTime left, UtcDateTime right) => left.CompareTo(right) <= 0;

        public static bool operator >=(UtcDateTime left, UtcDateTime right) => left.CompareTo(right) >= 0;
    }
}