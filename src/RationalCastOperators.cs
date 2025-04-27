namespace Rashe;

public sealed partial record Rational
{
    // decimal, long, and int are handled through built-in implicit casts to double

    public static implicit operator Rational(double value) => DoubleToRational(value, DEFAULT_PRECISION);

    public static implicit operator Rational(float value) => DoubleToRational(value, DEFAULT_PRECISION);

    public static implicit operator double(Rational r) => (double)r.Num / r.Denom;

    public static implicit operator float(Rational r) => (float)(double)r;

    public static explicit operator decimal(Rational r) => (decimal)(double)r;

    public static explicit operator long(Rational r) => (long)(double)r;

    public static explicit operator int(Rational r) => (int)(double)r;
}