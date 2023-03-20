namespace App;

public class PayDay
{
    public readonly List<DateTime> AccountantPayDays = new List<DateTime>();
    public readonly List<DateTime> PayDays = new List<DateTime>();
    private readonly DateTime[] _initialPayDays = new DateTime[12];

    public void SetAllNormalPayDaysByAYear(int year)
    {
        for (int i = 0; i < _initialPayDays.Length; i++)
        {
            _initialPayDays[i] = new DateTime(year, i + 1, 10);
        }
    }

    public void findAllPayDayDatesBasedOnYear(int year)
    {
        var holidays = new NationalHolidays();
        holidays.SetAllNationalHolidaysBasedOnYear(year);
        foreach (var day in _initialPayDays)
        {
            var payDayForMonth = day;

            payDayForMonth = payDayForMonth.DayOfWeek switch
            {
                DayOfWeek.Saturday => payDayForMonth.AddDays(-1),
                DayOfWeek.Sunday => payDayForMonth.AddDays(-2),
                _ => payDayForMonth
            };

            while (holidays.AllHolidaysForAChosenYear.Contains(payDayForMonth))
            {
                payDayForMonth = payDayForMonth.AddDays(-1);
            }
            PayDays.Add(payDayForMonth);
            FindPayDayForAccountant(payDayForMonth, year);
    
        }
    }

    private void FindPayDayForAccountant(DateTime date, int year)
    {
        var holidays = new NationalHolidays();
        holidays.SetAllNationalHolidaysBasedOnYear(year);
        var payDay = date.AddDays(-3);
        
        while (payDay.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday || holidays.AllHolidaysForAChosenYear.Contains(payDay))
        {
            payDay = payDay.AddDays(-1); 
        }
        AccountantPayDays.Add(payDay);
    }
}