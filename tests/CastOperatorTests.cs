using System.Diagnostics;
using Rashe;

namespace RasheTests;

[TestClass]
public class CastOperatorTests
{
    [TestMethod]
    public void SimpleDoubleCast()
    {
        var r = Rational.From(3, 4);
        double d = r;
        Assert.AreEqual(0.75d, d);
    }

    [TestMethod]
    public void IrrationalDoubleCast()
    {
        var r = Rational.From(2, 3);
        double d = r;
        Assert.AreEqual(0.6666666666666666d, d);
    }

    [TestMethod]
    [DataRow(0.25d, 1, 4)]
    [DataRow(0.333333333333333333d, 1, 3)]
    [DataRow(1d, 9, 9)]
    public void DoubleToRationalCast(double d, long xNum, long xDenom)
    {
        Rational r = d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }

    // Just for fun, let's try the Leibniz formula for π
    [TestMethod]
    public void PiCast()
    {
        var add = true;
        var r = Rational.Zero();

        const int COUNT = 1_000;

        for (var i = 1; i < COUNT; i += 2)
        {
            var term = Rational.From(add ? 4 : -4, i);
            r = r.Add(term);
            add = !add;

            // uncomment to get a running estimate
            Debug.WriteLine($"at {i}: {r} ({(double)r})");

            // due to long overflow of the denominator,
            //  let's convert to double then convert back
            //  to see if we can reduce the denominator
            //  every once in a while
            if ((i - 1) % 10 == 0)
            {
                var approx = (double)r;
                r = approx;
            }
        }

        // unless we convert Rational to use BigInteger,
        //  we have to be satisfied with only a few decimal
        //  places of accuracy, even after 1000 iterations.
        const double TEST_PRECISION = 0.001;

        double target = Math.Floor(Math.PI / TEST_PRECISION) * TEST_PRECISION;
        double d = Math.Round(r / TEST_PRECISION) * TEST_PRECISION;

        Assert.AreEqual(target, d);
    }
}