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
    [DataRow(1, 3, 0.3333333333333333d)]
    [DataRow(2, 3, 0.6666666666666666d)]
    [DataRow(1, 4, 0.25d)]
    public void IrrationalDoubleCast(long aNum, long aDenom, double x)
    {
        var r = Rational.From(aNum, aDenom);
        double d = r;
        Assert.AreEqual(x, d);
    }

    [TestMethod]
    [DataRow(1, 3, 0.3333333333333333f)]
    [DataRow(2, 3, 0.6666666666666666f)]
    [DataRow(1, 4, 0.25f)]
    public void IrrationalFloatCast(long aNum, long aDenom, float x)
    {
        var r = Rational.From(aNum, aDenom);
        float d = r;
        Assert.AreEqual(x, d);
    }

    [TestMethod]
    [DataRow(1, 3, "0.333333333333333")]
    [DataRow(2, 3, "0.666666666666667")]
    [DataRow(1, 4, "0.25")]
    public void IrrationalDecimalCast(long aNum, long aDenom, string x)
    {
        var r = Rational.From(aNum, aDenom);
        decimal d = (decimal)r;
        var xd = decimal.Parse(x);
        Assert.AreEqual(xd, d);
    }

    [TestMethod]
    [DataRow(1, 3, 0L)]
    [DataRow(4, 3, 1L)]
    [DataRow(-4, 3, -1L)]
    public void IrrationalLongCast(long aNum, long aDenom, long x)
    {
        var r = Rational.From(aNum, aDenom);
        long d = (long)r;
        Assert.AreEqual(x, d);
    }

    [TestMethod]
    [DataRow(1, 3, 0)]
    [DataRow(4, 3, 1)]
    [DataRow(-4, 3, -1)]
    public void IrrationalIntCast(long aNum, long aDenom, int x)
    {
        var r = Rational.From(aNum, aDenom);
        long d = (long)r;
        Assert.AreEqual(x, d);
    }

    [TestMethod]
    [DataRow(0.25d, 1, 4)]
    [DataRow(0.333333333333333333d, 1, 3)]
    [DataRow(1d, 9, 9)]
    public void DoubleToRationlCast(double d, long xNum, long xDenom)
    {
        Rational r = d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }

    [TestMethod]
    [DataRow(0.25f, 1, 4)]
    [DataRow(0.33333333333333f, 1, 3)]
    [DataRow(1, 9, 9)]
    public void FloatToRationalCast(float d, long xNum, long xDenom)
    {
        Rational r = d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }

    [TestMethod]
    [DataRow("0.25", 1, 4)]
    [DataRow("0.333333333333333333", 1, 3)]
    [DataRow("1", 9, 9)]
    public void DecimalToRationalCast(string s, long xNum, long xDenom)
    {
        var d = decimal.Parse(s);
        Rational r = (double)d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }

    [TestMethod]
    [DataRow(0.25d, 0, 1)]
    [DataRow(3.5, 3, 1)]
    [DataRow(1d, 9, 9)]
    public void LongToRationalCast(double d, long xNum, long xDenom)
    {
        Rational r = (long)d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }

    [TestMethod]
    [DataRow(0.25d, 0, 1)]
    [DataRow(3.5, 3, 1)]
    [DataRow(1d, 9, 9)]
    public void IntToRationalCast(double d, long xNum, long xDenom)
    {
        Rational r = (int)d;
        var x = Rational.From(xNum, xDenom);
        Assert.AreEqual(x, r);
    }
}