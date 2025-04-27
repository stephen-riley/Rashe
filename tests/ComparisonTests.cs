namespace Rashe.Tests;

[TestClass]
public class ComparisonTests
{
    [TestMethod]
    [DataRow(1, 2, 1, 3, true, true, false, false, false)]
    [DataRow(1, 3, 1, 2, false, false, false, true, true)]
    [DataRow(2, 4, 1, 2, true, false, true, true, false)]
    public void Comparisons(long aNum, long aDenom, long bNum, long bDenom, bool gte, bool gt, bool eq, bool lte, bool lt)
    {
        var a = Rational.From(aNum, aDenom, simplify: false);
        var b = Rational.From(bNum, bDenom, simplify: false);

        Assert.AreEqual(gte, a >= b);
        Assert.AreEqual(gt, a > b);
        Assert.AreEqual(eq, a == b);
        Assert.AreEqual(lte, a <= b);
        Assert.AreEqual(lt, a < b);
    }
}