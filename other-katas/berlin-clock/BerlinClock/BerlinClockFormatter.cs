using System;

namespace BerlinClock;

public class BerlinClockFormatter
{
    public string FormatSingleMinutes(TimeOnly time)
    {
        var lightCount = time.Minute % 5;

        return new string('Y', lightCount) + new string('O', 4 - lightCount);
    }

    public string FormatFiveMinutes(TimeOnly time)
    {
        var lightCount = time.Minute / 5;
        
        return new string('Y', lightCount).Replace("YYY", "YYR") + new string('O', 11 - lightCount);
    }

    public string FormatSingleHours(TimeOnly time)
    {
        var lightCount = time.Hour % 5;
        
        return new string('R', lightCount) + new string('O', 4 - lightCount);
    }
    
    public string FormatFiveHours(TimeOnly time)
    {
        var lightCount = time.Hour / 5;
        
        return new string('R', lightCount) + new string('O', 4 - lightCount);
    }

    public string FormatSeconds(TimeOnly time) => time.Second % 2 == 0 ? "Y" : "O";

    public string FormatTime(TimeOnly time) =>
        FormatSeconds(time) +
        FormatFiveHours(time) +
        FormatSingleHours(time) +
        FormatFiveMinutes(time) +
        FormatSingleMinutes(time);
}