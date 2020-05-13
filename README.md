# MathExtended.Random
Pure C# RANMAR Random Number Generator Algorithm Library.

## Legal information and credits

MathExtended.Random is project by Ales Hotko and was first released in May 2020. It's licensed under the MIT license.

Original &#xA9;:
```
RANMAR pseudo-random number generator
This random number generator originally appeared in "Toward a Universal
Random Number Generator" by George Marsaglia and Arif Zaman.
Florida State University Report: FSU-SCRI-87-50 (1987)
It was later modified by F. James and published in "A Review of Pseudo-
random Number Generators"
THIS IS THE BEST KNOWN RANDOM NUMBER GENERATOR AVAILABLE.
    (However, a newly discovered technique can yield
        a period of 10^600. But that is still in the development stage.)
It passes ALL of the tests for random number generators and has a period
of 2^144, is completely portable (gives bit identical results on all
machines with at least 24-bit mantissas in the floating point
representation).
The algorithm is a combination of a Fibonacci sequence (with lags of 97
and 33, and operation "subtraction plus one, modulo one") and an
"arithmetic sequence" (using subtraction).
========================================================================
C language version was written by Jim Butler, and was based on a
FORTRAN program posted by David LaSalle of Florida State University.
Adapted for Delphi by Anton Zhuchkov (fireton@mail.ru) in February, 2002.
Converted into a class by Primoz Gabrijelcic (gabr@17slon.com) in November, 2002.
(Url: https://github.com/gabr42/GpDelphiUnits/blob/master/src/GpRandomGen.pas)

Adapted for C# by Ales Hotko (https://github.com/ahotko) in May, 2020
(from Delphi (gabr42) and C (Url: https://www.pell.portland.or.us/~orc/Code/Misc/random.c))
```

## Usage

```csharp
int factor = 4096 * 4096;
var ranmar = new Ranmar(1802, 9373);

//To validate RANMAR generator, use the following procedure (described in original source):
// - set Seed1 to 1802
// - set Seed2 to 9373
// - Generate 20000 random numbers
// - next 6 generated numbers multiplied by 4096*4096 must be:
//   6533892, 14220222, 7275067, 6172232, 8354498, 10633180

for (int i = 0; i < 20006; i++)
{
    int generatedNumber = ranmar.Next(factor);
    if (i < 20000) continue;
    Console.WriteLine($"{i + 1}. {generatedNumber}");
}

//Output:
//20001. 6533892
//20002. 14220222
//20003. 7275067
//20004. 6172232
//20005. 8354498
//20006. 10633180
```

Also ```double``` can be generated:
```csharp
Console.WriteLine($"Double = {ranmar.NextDouble()}");

//Output:
//Double = 0,339674651622772
```
