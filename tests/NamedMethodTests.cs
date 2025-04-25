using Rashe;

namespace RasheTests;

[TestClass]
public class NamedMethodTests
{
    [TestMethod]
    public void Constructor()
    {
        var r = Rational.From(-2, 5);
        Assert.AreEqual("R:(-2/5)", r.ToString());
    }

    [TestMethod]
    [DataRow(1, 3, 3, 5, 14, 5)]
    public void BasicAdd(long na, long da, long nb, long db, long nc, long dc)
    {
        var a = Rational.From(na, nb);
        var b = Rational.From(nb, db);
        var c = a.Add(b);
        var expected = Rational.From(nc, dc);

        Assert.AreEqual(expected, c);
    }
}