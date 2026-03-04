namespace PayrollCalculator;

public class Person(string firstName, string lastName, int age)
{
    public string GetFirstName()
    {
        return firstName;
    }

    public string FullName()
    {
        return $"{lastName}, {firstName}";
    }
    public bool IsAdult()
    {
        if (age >= 18) return true;
        else return false;
    }
}
