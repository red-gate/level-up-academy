using System;
using BerlinClock;
using FluentAssertions;
using NUnit.Framework;

namespace BerlinClockTests;

public class BerlinClockFormatterTests
{
    private readonly BerlinClockFormatter _berlinClockFormatter = new ();

    [TestCase("00:00:00", "OOOO")]
    [TestCase("23:59:59", "YYYY")]
    [TestCase("12:32:00", "YYOO")]
    [TestCase("12:34:00", "YYYY")]
    [TestCase("12:35:00", "OOOO")]

    public void CheckFormatSingleMinutesIsCorrect(string time, string expected) => _berlinClockFormatter.FormatSingleMinutes(TimeOnly.Parse(time)).Should().Be(expected);

    [TestCase("00:00:00", "OOOOOOOOOOO")]
    [TestCase("23:59:59", "YYRYYRYYRYY")]
    [TestCase("12:04:00", "OOOOOOOOOOO")]
    [TestCase("12:23:00", "YYRYOOOOOOO")]
    [TestCase("12:35:00", "YYRYYRYOOOO")]
    public void CheckFormatFiveMinutesIsCorrect(string time, string expected) => _berlinClockFormatter.FormatMinutesBlock(TimeOnly.Parse(time)).Should().Be(expected);

    [TestCase("00:00:00", "OOOO")]
    [TestCase("23:59:59", "RRRO")]
    [TestCase("02:04:00", "RROO")]
    [TestCase("08:23:00", "RRRO")]
    [TestCase("14:35:00", "RRRR")]
    public void CheckFormatSingleHoursIsCorrect(string time, string expected) => _berlinClockFormatter.FormatSingleHours(TimeOnly.Parse(time)).Should().Be(expected);

    [TestCase("00:00:00", "OOOO")]
    [TestCase("23:59:59", "RRRR")]
    [TestCase("02:04:00", "OOOO")]
    [TestCase("08:23:00", "ROOO")]
    [TestCase("16:35:00", "RRRO")]
    public void CheckFormatFiveHoursIsCorrect(string time, string expected) => _berlinClockFormatter.FormatHoursBlock(TimeOnly.Parse(time)).Should().Be(expected);

    [TestCase("00:00:00", "Y")]
    [TestCase("23:59:59", "O")]
    public void CheckFormatSecondsIsCorrect(string time, string expected) => _berlinClockFormatter.FormatSeconds(TimeOnly.Parse(time)).Should().Be(expected);

    [TestCase("00:00:00", "YOOOOOOOOOOOOOOOOOOOOOOO")]
    [TestCase("23:59:59", "ORRRRRRROYYRYYRYYRYYYYYY")]
    [TestCase("16:50:06", "YRRROROOOYYRYYRYYRYOOOOO")]
    [TestCase("11:37:01", "ORROOROOOYYRYYRYOOOOYYOO")]
    public void CheckFormatTimeIsCorrect(string time, string expected) => _berlinClockFormatter.FormatTime(TimeOnly.Parse(time)).Should().Be(expected);
}
