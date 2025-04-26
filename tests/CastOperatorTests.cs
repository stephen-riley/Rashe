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

    // [TestMethod]
    public void PiCast()
    {
        var add = true;
        var r = Rational.Zero();

        const int COUNT = 100;

        for (var i = 1; i < COUNT; i += 2)
        {
            var term = Rational.From(4, i);
            term = add ? term : term.Negate();
            r = r.Add(term);
            Debug.WriteLine($"at {i}: {r}");
            add = !add;
        }

        double d = r;
        Assert.AreEqual(Math.PI, d);
    }
}