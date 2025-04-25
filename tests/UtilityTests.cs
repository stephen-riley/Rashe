using Rashe;

namespace RasheTests;

[TestClass]
public class UtilityTests
{
    [TestMethod]
    [DataRow(3, 5, 15)]
    [DataRow(3, 3, 3)]
    public void Lcm(long a, long b, long expected) => Assert.AreEqual(expected, Rational.Lcm(a, b));
}