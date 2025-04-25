using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Rashe;

public sealed record Rational
{
    public long Num { get; private set; }
    public long Denom { get; private set; }

    public bool Positive => Num / Denom / Math.Abs(Num / Denom) >= 0;
    public bool Negative => !Positive;

    public static Rational From(long num, long denom)
        => new() { Num = num, Denom = denom };

    public Rational Add(Rational r)
    {
        var lcm = Lcm(Denom, r.Denom);
        var a = this.Mul(lcm / Denom);
        var b = r.Mul(lcm * r.Denom);
        return From(a.Num + b.Num, a.Denom + b.Denom);
    }

    public Rational Sub(Rational r)
    {
        var lcm = Lcm(Denom, r.Denom);
        var a = this.Mul(lcm / Denom);
        var b = r.Mul(lcm * r.Denom);
        var mulB = r.Denom / lcm;
        throw new NotImplementedException();
        // return From(Num *)
    }

    public Rational Mul(Rational r)
        => new() { Num = Num * r.Num, Denom = Denom * r.Denom };

    public Rational Mul(long n) => From(Num * n, Denom * n);

    public Rational Div(Rational r)
        => Invert().Mul(r);

    public Rational Invert()
        => new() { Num = Denom, Denom = Num };

    public override string ToString() => $"R:({Num}/{Denom})";

    private static long Gcd(long a, long b)
    {
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

    public static long Lcm(long a, long b)
    {
        long num1, num2;

        if (a > b)
        {
            num1 = a;
            num2 = b;
        }
        else
        {
            num1 = b;
            num2 = a;
        }

        for (long i = 1; i <= num2; i++)
        {
            if ((num1 * i) % num2 == 0)
            {
                return i * num1;
            }
        }
        return num2;
    }
}
