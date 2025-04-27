namespace Rashe;

public partial record Rational
{
    public static Rational operator +(Rational r) => r;
    public static Rational operator -(Rational r) => r.Negate();

    public static Rational operator +(Rational a, Rational b) => a.Add(b);
    public static Rational operator -(Rational a, Rational b) => a.Sub(b);
    public static Rational operator *(Rational a, Rational b) => a.Mul(b);
    public static Rational operator /(Rational a, Rational b) => a.Div(b);
}