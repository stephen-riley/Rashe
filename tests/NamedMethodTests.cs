using Rashe;

namespace RasheTests;

[TestClass]
public class NamedMethodTests
{
    [TestMethod]
    [DataRow(-2, 5, "R:(-2/5)")]
    [DataRow(2, -5, "R:(-2/5)")]
    [DataRow(-2, -5, "R:(2/5)")]
    public void ConstructorWithNegativesNormalization(long n, long d, string s)
    {
        var r = Rational.From(n, d);
        Assert.AreEqual(s, r.ToString());
    }

    [TestMethod]
    [DataRow(1, 3, 1, 3, 2, 3)]
    [DataRow(1, 3, 3, 5, 14, 15)]
    [DataRow(1, 3, -3, 5, -4, 15)]
    public void BasicAdd(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a.Add(b);
        var expected = Rational.From(nc, dc, simplify: false);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, 1, 3, 0, 3)]
    [DataRow(1, 3, 6, 9, -3, 9)]
    [DataRow(1, 3, -3, 5, 14, 15)]
    public void BasicSub(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a.Sub(b, simplify: false);
        var expected = Rational.From(nc, dc, simplify: false);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, 2, 5, 2, 15)]
    [DataRow(4, 1, 5, 6, 20, 6)]
    [DataRow(-1, 3, 2, 5, -2, 15)]
    [DataRow(-4, 1, 5, -6, 20, 6)]
    public void BasicMul(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a.Mul(b, simplify: false);
        var expected = Rational.From(nc, dc, simplify: false);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, 2, 5, 5, 6)]
    [DataRow(4, 1, 5, 6, 24, 5)]
    [DataRow(-1, 3, 2, 5, -5, 6)]
    [DataRow(-4, 1, 5, -6, 24, 5)]
    public void BasicDiv(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a.Div(b, simplify: false);
        var expected = Rational.From(nc, dc, simplify: false);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, -1, 3)]
    [DataRow(-1, -3, -1, 3)]
    public void Negation(long num, long denom, long xnum, long xdenom)
    {
        var a = Rational.From(num, denom, simplify: false).Negate();
        var x = Rational.From(xnum, xdenom, simplify: false);
        Assert.AreEqual(x, a);
    }

    [TestMethod]
    [DataRow(5, 15, "R:(1/3)")]
    [DataRow(9, 15, "R:(3/5)")]
    [DataRow(0, 3, "R:(0/3)")]
    public void Simplify(long n, long d, string x)
        => Assert.AreEqual(x, Rational.From(n, d, simplify: false).Simplify().ToString());

    [TestMethod]
    [DataRow(3, 5, "R:(5/3)")]
    public void ToNegativeOne(long n, long d, string x)
        => Assert.AreEqual(x, Rational.From(n, d, simplify: false).ToNegativeOne(simplify: false).ToString());

    [TestMethod]
    [DataRow(1, 2, 3)]
    [DataRow(1, -2, -1)]
    public void WholeNumberAddition(long a, long b, long x)
    {
        var ar = Rational.From(a, 1);
        var br = Rational.From(b, 1);
        var cr = ar.Add(br);
        var xr = Rational.From(x, 1);

        Assert.AreEqual(xr, cr);
    }

    [TestMethod]
    [DataRow(4, 1, -4, 3, 8, 3)]
    public void WholePlusFraction(long aNum, long aDenom, long bNum, long bDenom, long xNum, long xDenom)
    {
        var a = Rational.From(aNum, aDenom);
        var b = Rational.From(bNum, bDenom);
        var c = a.Add(b);

        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, c);
    }
}