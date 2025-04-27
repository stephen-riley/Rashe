using Rashe;

var zero = Rational.Zero;
var twoThirds = Rational.From(2, 3);
var oneQuarter = Rational.From(1, 4);
var difference = twoThirds.Sub(oneQuarter);

Console.WriteLine($"{twoThirds} - {oneQuarter} = {difference}"); // prints `R:(2/3) - R:(1/4) = R:(5/12)`

double d = twoThirds;
Console.WriteLine(d); // prints `0.6666666666666666`

var oneHalf = Rational.From(1, 2);
Console.WriteLine(oneHalf == 0.5); // true
Console.WriteLine(oneHalf >= 0.5); // true
Console.WriteLine(oneHalf < 0.5);  // false
Console.WriteLine(0.5 >= oneHalf); // true