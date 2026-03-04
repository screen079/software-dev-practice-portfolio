namespace PayrollCalculator;

class Program
{
    const double TAX_RATE = 0.2;

    static double CalculatePay(double hours, double rate)
    {
        try
        {
            if (hours < 0) throw new ArgumentOutOfRangeException(nameof(hours), "negative value");
            if (hours < 0) throw new ArgumentOutOfRangeException(nameof(rate), "negative value");
            double gross = hours * rate;
            double tax = gross * TAX_RATE;
            double net = gross - tax;
            return net;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Hours and Rate must be positive.");
            return null;
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter employee name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Hours worked: ");
        string str_hours = Console.ReadLine();
        Console.WriteLine("Hourly Rate: ");
        string str_rate = Console.ReadLine();
        double hours, rate;
        double.TryParse(str_hours, out hours);
        double.TryParse(str_rate, out rate);
        double net_pay = CalculatePay(hours, rate);
        Console.WriteLine($"{name} earned ${net_pay:F2} after tax.");
    }
}
