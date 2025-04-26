# Rashe

A dotnet rational number library.

```csharp
var zero = Rational.Zero;
var twoThirds = Rational.From(2,3);
var oneQuarter = Rational.From(1,4);
var difference = twoThirds.Sub(oneQuarter);
Console.WriteLine($"{twoThirds} - {oneQuarter} = {difference}"); // prints `R:(5/12)`

double d = twoThirds;
Console.WriteLine(d); // prints `0.6666666666666666`
```