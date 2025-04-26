using Rashe;

namespace RasheTests;

[TestClass]
public class UtilityTests
{
    [TestMethod]
    [DataRow(3, 5, 15)]
    [DataRow(3, 3, 3)]
    public void Lcm(long a, long b, long expected) => Assert.AreEqual(expected, Rational.Lcm(a, b));

    [TestMethod]
    [DataRow(5, 15, 5)]
    [DataRow(9, 15, 3)]
    [DataRow(1, 17, 1)]
    [DataRow(0, 3, 1)]
    public void Gcd(long a, long b, long x)
    {
        Assert.AreEqual(x, Rational.Gcd(a, b));
    }
}