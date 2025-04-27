namespace Rashe;

public partial record Rational
{
    public static bool operator >(Rational a, Rational b) => a.GreaterThan(b);
    public static bool operator >=(Rational a, Rational b) => a.GreaterThan(b) || a.Equals(b);
    public static bool operator <(Rational a, Rational b) => a.LessThan(b);
    public static bool operator <=(Rational a, Rational b) => a.LessThan(b) || a.Equals(b);
}