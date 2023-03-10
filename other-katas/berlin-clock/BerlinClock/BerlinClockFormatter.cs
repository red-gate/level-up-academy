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

    private static readonly IReadOnlyDictionary<string, int> _singleMinutePatternsReverseLookup =
        _singleMinutePatterns
            .Select((pattern, index) => (pattern, index))
            .ToDictionary(x => x.pattern, x => x.index);

    private static readonly IReadOnlyDictionary<string, int> _minuteBlockPatternsReverseLookup =
        _minuteBlockPatterns
            .Select((pattern, index) => (pattern, index))
            .ToDictionary(x => x.pattern, x => x.index);

    private static readonly IReadOnlyDictionary<string, int> _singleHourPatternsReverseLookup =
        _singleHourPatterns
            .Select((pattern, index) => (pattern, index))
            .ToDictionary(x => x.pattern, x => x.index);

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

    public bool TryParseSingleMinute(string input, out int value) =>
        _singleMinutePatternsReverseLookup.TryGetValue(input, out value);

    public bool TryParseMinutesBlock(string input, out int value) =>
        _minuteBlockPatternsReverseLookup.TryGetValue(input, out value);

    public bool TryParseSingleHour(string input, out int value) =>
        _singleHourPatternsReverseLookup.TryGetValue(input, out value);
}
