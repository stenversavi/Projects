using System.Globalization;
using System.Text;

namespace App;

public abstract class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("----ConsoleApp for SpinTEK internship----");
            Console.Write("Enter a valid year to calculate paydates: ");

            try
            {
                var year = Convert.ToInt32(Console.ReadLine());
                var normalPayDays = new PayDay();
                normalPayDays.SetAllNormalPayDaysByAYear(year);
                normalPayDays.findAllPayDayDatesBasedOnYear(year);
                var fileName = year + ".csv";

                var sb = new StringBuilder();

                sb.AppendLine("Pay date,Accountant Reminder");

                for (var i = 0; i < normalPayDays.PayDays.Count; i++)
                {
                    var payDate = normalPayDays.PayDays[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var payDayReminder = normalPayDays.AccountantPayDays[i].ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    sb.AppendLine($"{payDate},{payDayReminder}");
                }

                File.WriteAllText(fileName, sb.ToString());

                // Exit the loop and end the program
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
            }
        }
    }
}