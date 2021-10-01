using System;

namespace AccountCalculator
{
    public class UtcDateTime
    {
        private DateTime _dateTime;

        public UtcDateTime(DateTimeOffset dateTimeOffset) => _dateTime = dateTimeOffset.UtcDateTime;

        public DateTime Get() => _dateTime;
    }
}