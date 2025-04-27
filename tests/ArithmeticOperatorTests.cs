namespace Rashe.Tests;

[TestClass]
public class ArithmeticOperatorTests
{
    [TestMethod]
    [DataRow(1, 3, 1, 3, 2, 3)]
    [DataRow(1, 3, 3, 5, 14, 15)]
    [DataRow(1, 3, -3, 5, -4, 15)]
    public void BasicAdd(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a + b;
        var expected = Rational.From(nc, dc);

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
        var c = a - b;
        var expected = Rational.From(nc, dc);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, 2, 5, 2, 15)]
    [DataRow(4, 1, 5, 6, 10, 3)]
    [DataRow(-1, 3, 2, 5, -2, 15)]
    [DataRow(-4, 1, 5, -6, 10, 3)]
    public void BasicMul(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, da, simplify: false);
        var b = Rational.From(nb, db, simplify: false);
        var c = a * b;
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
        var c = a / b;
        var expected = Rational.From(nc, dc, simplify: false);

        Assert.AreEqual(expected, c);
    }

    [TestMethod]
    [DataRow(1, 3, -1, 3)]
    public void Negation(long num, long denom, long xnum, long xdenom)
    {
        var a = Rational.From(num, denom, simplify: false);
        var neg = -a;
        var x = Rational.From(xnum, xdenom, simplify: false);
        Assert.AreEqual(x, neg);
    }

    [TestMethod]
    [DataRow(1, 2, 3)]
    [DataRow(1, -2, -1)]
    public void WholeNumberAddition(long a, long b, long x)
    {
        var ar = Rational.From(a, 1);
        var br = Rational.From(b, 1);
        var cr = ar + br;
        var xr = Rational.From(x, 1);

        Assert.AreEqual(xr, cr);
    }

    [TestMethod]
    [DataRow(4, 1, -4, 3, 8, 3)]
    public void WholePlusFraction(long aNum, long aDenom, long bNum, long bDenom, long xNum, long xDenom)
    {
        var a = Rational.From(aNum, aDenom);
        var b = Rational.From(bNum, bDenom);
        var c = a + b;

        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, c);
    }
}