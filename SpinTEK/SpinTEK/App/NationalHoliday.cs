namespace App;

public class NationalHolidays
{
    public readonly List<DateTime> AllHolidaysForAChosenYear = new List<DateTime>();

    public void SetAllNationalHolidaysBasedOnYear(int year)
    {
        DateTime[] holidays = {
            new DateTime(year, 01, 01),
            new DateTime(year, 02, 24),
            new DateTime(year, 05, 01),
            new DateTime(year, 06, 23),
            new DateTime(year, 06, 24),
            new DateTime(year, 08, 20),
            new DateTime(year, 12, 24),
            new DateTime(year, 12, 25),
            new DateTime(year, 12, 26)
        };

        DateTime easter = CalculateEaster(year);
        holidays = holidays.Concat(new[] {
            easter,
            easter.AddDays(-2), // Good Friday
            easter.AddDays(49) // Pentecost
        }).ToArray();

        AllHolidaysForAChosenYear.AddRange(holidays);
    }

    private DateTime CalculateEaster(int year)
    {
        int g = year % 19;
        int c = year / 100;
        int h = (c - (c / 4) - ((8 * c + 13) / 25) + 19 * g + 15) % 30;
        int i = h - (h / 28) * (1 - (h / 28) * (29 / (h + 1)) * ((21 - g) / 11));
        int day = i - ((year + (year / 4) + i + 2 - c + (c / 4)) % 7) + 28;
        int month = 3;
        if (day > 31)
        {
            month++;
            day -= 31;
        }
        return new DateTime(year, month, day);
    }
}