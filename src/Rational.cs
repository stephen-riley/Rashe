using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Rashe;

public sealed record Rational
{
    public long Num { get; private set; }
    public long Denom { get; private set; }

    public bool Positive => Num / Denom / Math.Abs(Num / Denom) >= 0;
    public bool Negative => !Positive;

    public static Rational Zero() => From(0, 1);

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
}
