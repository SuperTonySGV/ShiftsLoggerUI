using Spectre.Console;
using System.Globalization;

namespace ShiftsLoggerUI;

internal class Helpers
{

    public static DateTime GetDateAndTime(string message)
    {
        Console.Clear();
        var rule = new Rule($"[blue]{message}[/]");
        AnsiConsole.Write(rule);
        var currentYear = DateTime.Now.Year;
        var year = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a year:")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more years)[/]")
                .AddChoices(
                    new[] { currentYear } // Current year as the first choice
                    .Concat(Enumerable.Range(currentYear - 5, 5)) // 5 years before
                    .Concat(Enumerable.Range(currentYear + 1, 5)) // 5 years after (excluding current)
                )
        );

        var currentMonth = DateTime.Now.Month; // Get current month (1-12)
        var month = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a month:")
                .AddChoices(new[] { currentMonth }.Concat(Enumerable.Range(1, currentMonth - 1)).Concat(Enumerable.Range(currentMonth + 1, 12 - currentMonth))) // Current month first
        );

        var currentDay = DateTime.Now.Day; // Get the current day of the month
        var day = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title($"Select a day (for {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}):")
                .AddChoices(
                    new[] { currentDay }  // Prepend the current day
                    .Concat(Enumerable.Range(1, currentDay - 1)) // Add days before the current day
                    .Concat(Enumerable.Range(currentDay + 1, DateTime.DaysInMonth(year, month) - currentDay)) // Add days after
                )
        );

        var time = GetTime();

        return new DateTime(year, month, day) + time;
    }

    public static TimeSpan GetTime()
    {
        var currentHour = DateTime.Now.Hour;
        var currentMinute = DateTime.Now.Minute;
        var currentSecond = DateTime.Now.Second;

        var hour = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select the hour:")
                .AddChoices(
                    new[] { currentHour } // Prepend the current hour
                    .Concat(Enumerable.Range(0, currentHour)) // Add hours before current hour
                    .Concat(Enumerable.Range(currentHour + 1, 23 - currentHour)) // Add hours after current hour
                )
        );

        var minute = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select the minute:")
                .AddChoices(
                    new[] { currentMinute } // Prepend the current minute
                    .Concat(Enumerable.Range(0, currentMinute)) // Add minutes before current minute
                    .Concat(Enumerable.Range(currentMinute + 1, 59 - currentMinute)) // Add minutes after current minute
                )
        );

        var second = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select the second:")
                .AddChoices(
                    new[] { currentSecond } // Prepend the current second
                    .Concat(Enumerable.Range(0, currentSecond)) // Add seconds before current second
                    .Concat(Enumerable.Range(currentSecond + 1, 59 - currentSecond)) // Add seconds after current second
                )
        );

        return new TimeSpan(hour, minute, second);
    }

}
