using System;

namespace MathExtended.Random
{
    #region RANMAR (c)
    /*
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
    */
    #endregion

    /// <summary>
    /// RANMAR pseudo-random number generator
    /// </summary>
    public class Ranmar
    {
        private readonly int MaxSeed1 = 31328;
        private readonly int MaxSeed2 = 30081;

        private readonly int MaxRandom = 1073741822;

        private readonly double[] u = new double[97];
        private double c;
        private double cd;
        private double cm;
        private int i97;
        private int j97;

        public Ranmar(int seed1, int seed2)
        {
            Initialize(seed1, seed2);
        }

        public Ranmar()
        {
            var random = new System.Random();
            Initialize(random.Next(MaxSeed1), random.Next(MaxSeed2));
        }

        public int Next()
        {
            return Next(0, MaxRandom);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            double range = maxValue - minValue;
            return (int)(Generate() * range + minValue);
        }

        public double NextDouble()
        {
            return Generate();
        }

        private double Generate()
        {
            double result = u[i97] - u[j97];
            if (result < 0.0) result += 1.0;
            u[i97] = result;
            i97--;
            if (i97 < 0) i97 = 96;
            j97--;
            if (j97 < 0) j97 = 96;
            c -= cd;
            if (c < 0.0) c += cm;
            result -= c;
            if (result < 0.0) result += 1.0;
            return result;
        }

        #region RANMAR Init routine
        /* This is the initialization routine for the random number generator RANMAR()
          NOTE: The seed variables can have values between:    0 <= seed1 <= 31328
                                                               0 <= seed2 <= 30081
          The random number sequences created by these two seeds are of sufficient
          length to complete an entire calculation with.For example, if several
         different groups are working on different parts of the same calculation,
         each group could be assigned its own seed1 seed.This would leave each group

         with 30000 choices for the second seed.That is to say, this random
         number generator can create 900 million different subsequences -- with
         each subsequence having a length of approximately 10^30.

         Use seed1 = 1802 & seed2 = 9373 to test the random number generator. The
         subroutine RANMAR should be used to generate 20000 random numbers.

         Then display the next six random numbers generated multiplied by 4096*4096

         If the random number generator is working properly, the random numbers
         should be:
                    6533892.0  14220222.0  7275067.0
                    6172232.0  8354498.0   10633180.0
        */
        private void Initialize(int seed1, int seed2)
        {
            if (seed1 < 0 || seed1 > MaxSeed1)
                throw new ArgumentOutOfRangeException("seed1", $"Seed 1 must be between 0 and {MaxSeed1}.");
            if (seed2 < 0 || seed2 > MaxSeed2)
                throw new ArgumentOutOfRangeException("seed2", $"Seed 2 must be between 0 and {MaxSeed2}.");

            int i = (seed1 / 177) % 177 + 2;
            int j = seed1 % 177 + 2;
            int k = (seed2 / 169) % 178 + 1;
            int l = seed2 % 169;

            for (int ii = 0; ii < 97; ii++)
            {
                double s = 0.0;
                double t = 0.5;

                for (int jj = 1; jj <= 24; jj++)
                {
                    int m = (((i * j) % 179) * k) % 179;
                    i = j;
                    j = k;
                    k = m;
                    l = (53 * l + 1) % 169;
                    if ((m * l) % 64 >= 32)
                    {
                        s += t;
                    }
                    t *= 0.5;
                }
                u[ii] = s;
            }
            c = 362436.0 / 16777216.0;
            cd = 7654321.0 / 16777216.0;
            cm = 16777213.0 / 16777216.0;

            i97 = 96;
            j97 = 32;
        }
        #endregion
    }
}
