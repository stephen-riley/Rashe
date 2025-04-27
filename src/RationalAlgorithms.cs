namespace Rashe;

public partial record Rational
{
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