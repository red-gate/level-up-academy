using System;
using System.Collections.Generic;
using System.Linq;

namespace BerlinClock;

public class BerlinClockFormatter
{
    private static readonly IReadOnlyList<string> _singleMinutePatterns = new []
    {
        "OOOO",
        "YOOO",
        "YYOO",
        "YYYO",
        "YYYY"
    };

    private static readonly IReadOnlyList<string> _minuteBlockPatterns = new []
    {
        "OOOOOOOOOOO",
        "YOOOOOOOOOO",
        "YYOOOOOOOOO",
        "YYROOOOOOOO",
        "YYRYOOOOOOO",
        "YYRYYOOOOOO",
        "YYRYYROOOOO",
        "YYRYYRYOOOO",
        "YYRYYRYYOOO",
        "YYRYYRYYROO",
        "YYRYYRYYRYO",
        "YYRYYRYYRYY",
    };

    private static readonly IReadOnlyList<string> _singleHourPatterns = new []
    {
        "OOOO",
        "ROOO",
        "RROO",
        "RRRO",
        "RRRR"
    };

    private static readonly IReadOnlyList<string> _hourBlockPatterns = new []
    {
        "OOOO",
        "ROOO",
        "RROO",
        "RRRO",
        "RRRR"
    };

    private static readonly IReadOnlyList<string> _secondPatterns = new []
    {
        "Y",
        "O"
    };

    public string FormatSingleMinutes(TimeOnly time) =>
        _singleMinutePatterns[time.Minute % _singleMinutePatterns.Count];

    public string FormatMinutesBlock(TimeOnly time) =>
        _minuteBlockPatterns[time.Minute / _singleMinutePatterns.Count];

    public string FormatSingleHours(TimeOnly time) =>
        _singleHourPatterns[time.Hour % _singleHourPatterns.Count];

    public string FormatHoursBlock(TimeOnly time) =>
        _hourBlockPatterns[time.Hour / _singleHourPatterns.Count];

    public string FormatSeconds(TimeOnly time) =>
        _secondPatterns[time.Second % 2];

    public string FormatTime(TimeOnly time) =>
        FormatSeconds(time) +
        FormatHoursBlock(time) +
        FormatSingleHours(time) +
        FormatMinutesBlock(time) +
        FormatSingleMinutes(time);
}
