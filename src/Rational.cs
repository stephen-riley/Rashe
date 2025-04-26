using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Rashe;

public sealed record Rational
{
    public long Num { get; private set; }
    public long Denom { get; private set; }

    public bool Positive => Num / Denom / Math.Abs(Num / Denom) >= 0;
    public bool Negative => !Positive;

    public static Rational Zero => From(0, 1);

    public static Rational From(long num, long denom, bool simplify = true)
    {
        // Normalize negatives so that only the numerator can be negative.
        switch (num)
        {
            case < 0 when denom < 0:
                num = -num;
                denom = -denom;
                break;
            case > 0 when denom < 0:
                num = -num;
                denom = -denom;
                break;
            default: break;
        }

        var r = new Rational() { Num = num, Denom = denom };
        return simplify ? r.Simplify() : r;
    }

    public Rational Add(Rational r, bool simplify = true)
    {
        var lcm = Lcm(Denom, r.Denom);
        var a = Mul(lcm / Denom, simplify: false);
        var b = r.Mul(lcm / r.Denom, simplify: false);
        return From(a.Num + b.Num, a.Denom, simplify);
    }
    public Rational Sub(Rational r, bool simplify = true) => Add(r.Negate(simplify), simplify);

    public Rational Mul(Rational r, bool simplify = true)
        => From(Num * r.Num, Denom * r.Denom, simplify);

    public Rational Mul(long n, bool simplify = true) => From(Num * n, Denom * n, simplify);

    public Rational Div(Rational r)
        => ToNegativeOne().Mul(r);

    public Rational ToNegativeOne(bool simplify = true)
        => From(Denom, Num, simplify);

    public Rational Negate(bool simplify = true) => From(-Num, Denom, simplify);

    public Rational Simplify()
    {
        var gcd = Gcd(Num, Denom);
        return gcd > 1 ? From(Num / gcd, Denom / gcd) : this;
    }

    public static implicit operator double(Rational r) => (double)r.Num / r.Denom;

    public override string ToString() => $"R:({Num}/{Denom})";

    internal static long Gcd(long a, long b)
    {
        if (a == 0 || b == 0) return 1;

        a = Math.Abs(a);
        b = Math.Abs(b);

        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    internal static long Lcm(long a, long b)
    {
        (var num1, var num2) = a > b ? (a, b) : (b, a);

        for (long i = 1; i <= num2; i++)
        {
            if (num1 * i % num2 == 0)
            {
                return i * num1;
            }
        }
        return num2;
    }

    private const double DEFAULT_PRECISION = 0.0001;

    public static implicit operator Rational(double value) => DoubleToRational(value, DEFAULT_PRECISION);

    // from https://stackoverflow.com/a/32903747
    public static Rational DoubleToRational(double value, double accuracy = DEFAULT_PRECISION)
    {
        if (accuracy is >= 1.0 or <= 0.0)
        {
            throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");
        }

        int sign = Math.Sign(value);

        if (sign == -1)
        {
            value = Math.Abs(value);
        }

        // Accuracy is the maximum relative error; convert to absolute maxError
        double maxError = sign == 0 ? accuracy : value * accuracy;

        int n = (int)Math.Floor(value);
        value -= n;

        if (value < maxError)
        {
            return Rational.From(sign * n, 1, simplify: false);
        }

        if (1 - maxError < value)
        {
            return Rational.From(sign * (n + 1), 1, simplify: false);
        }

        // The lower fraction is 0/1
        int lower_n = 0;
        int lower_d = 1;

        // The upper fraction is 1/1
        int upper_n = 1;
        int upper_d = 1;

        while (true)
        {
            // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
            int middle_n = lower_n + upper_n;
            int middle_d = lower_d + upper_d;

            if (middle_d * (value + maxError) < middle_n)
            {
                // real + error < middle : middle is our new upper
                Seek(ref upper_n, ref upper_d, lower_n, lower_d, (un, ud) => (lower_d + ud) * (value + maxError) < (lower_n + un));
            }
            else if (middle_n < (value - maxError) * middle_d)
            {
                // middle < real - error : middle is our new lower
                Seek(ref lower_n, ref lower_d, upper_n, upper_d, (ln, ld) => (ln + upper_n) < (value - maxError) * (ld + upper_d));
            }
            else
            {
                // Middle is our best fraction
                return From((n * middle_d + middle_n) * sign, middle_d, simplify: false);
            }
        }
    }

    internal static void Seek(ref int a, ref int b, int ainc, int binc, Func<int, int, bool> f)
    {
        a += ainc;
        b += binc;

        if (f(a, b))
        {
            int weight = 1;

            do
            {
                weight *= 2;
                a += ainc * weight;
                b += binc * weight;
            }
            while (f(a, b));

            do
            {
                weight /= 2;

                int adec = ainc * weight;
                int bdec = binc * weight;

                if (!f(a - adec, b - bdec))
                {
                    a -= adec;
                    b -= bdec;
                }
            }
            while (weight > 1);
        }
    }
}
