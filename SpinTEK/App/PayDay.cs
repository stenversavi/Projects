namespace App;

public class PayDay
{
    public List<DateTime> AccountantPayDays = new List<DateTime>();
    public List<DateTime> PayDays = new List<DateTime>();
    private DateTime[] _initialPayDays = new DateTime[12];

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
        foreach (DateTime day in _initialPayDays)
        {
            DateTime payDayForMonth = day;

            if (payDayForMonth.DayOfWeek == DayOfWeek.Saturday)
            {
                payDayForMonth = payDayForMonth.AddDays(-1);
            } 
            else if (payDayForMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                payDayForMonth = payDayForMonth.AddDays(-2);
            }

            while (holidays.AllHolidaysForAChosenYear.Contains(payDayForMonth))
            {
                payDayForMonth = payDayForMonth.AddDays(-1);
            }
            PayDays.Add(payDayForMonth);
            FindPayDayForAccountant(payDayForMonth, year);
    
        }
    }

    public void FindPayDayForAccountant(DateTime date, int year)
    {
        var holidays = new NationalHolidays();
        holidays.SetAllNationalHolidaysBasedOnYear(year);
        DateTime payDay = date.AddDays(-3);
        
        while (payDay.DayOfWeek == DayOfWeek.Saturday || payDay.DayOfWeek == DayOfWeek.Sunday || holidays.AllHolidaysForAChosenYear.Contains(payDay))
        {
            payDay = payDay.AddDays(-1); 
        }
        AccountantPayDays.Add(payDay);
    }
}
