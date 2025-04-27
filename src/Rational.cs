namespace Rashe;

public sealed partial record Rational
{
    public long Num;
    public long Denom;

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

    public (Rational a, Rational b) CommonDenominators(Rational r)
    {
        var lcm = Lcm(Denom, r.Denom);
        var a = Mul(lcm / Denom, simplify: false);
        var b = r.Mul(lcm / r.Denom, simplify: false);
        return (a, b);
    }

    public Rational Add(Rational r, bool simplify = true)
    {
        var (a, b) = CommonDenominators(r);
        return From(a.Num + b.Num, a.Denom, simplify);
    }

    public Rational Sub(Rational r, bool simplify = true) => Add(r.Negate(simplify), simplify);

    public Rational Mul(Rational r, bool simplify = true)
        => From(Num * r.Num, Denom * r.Denom, simplify);

    public Rational Mul(long n, bool simplify = true) => From(Num * n, Denom * n, simplify);

    public Rational Div(Rational r, bool simplify = true)
        => Mul(r.ToNegativeOne(simplify), simplify);

    public Rational ToNegativeOne(bool simplify = true)
        => From(Denom, Num, simplify);

    public Rational Negate(bool simplify = true) => From(-Num, Denom, simplify);

    public Rational Simplify()
    {
        var gcd = Gcd(Num, Denom);
        return gcd > 1 ? From(Num / gcd, Denom / gcd) : this;
    }

    public bool Equals(Rational? r)
    {
        if (r is null) { return false; }

        var (a, b) = CommonDenominators(r);
        return a.Num == b.Num;
    }

    public bool GreaterThan(Rational r)
    {
        var (a, b) = CommonDenominators(r);
        return a.Num > b.Num;
    }

    public bool LessThan(Rational r)
    {
        var (a, b) = CommonDenominators(r);
        return a.Num < b.Num;
    }

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => $"R:({Num}/{Denom})";
}
