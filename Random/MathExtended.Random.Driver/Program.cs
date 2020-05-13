using System;

namespace MathExtended.Random.Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            int factor = 4096 * 4096;
            var ranmar = new Ranmar(1802, 9373);
            for (int i = 0; i < 20006; i++)
            {
                Console.WriteLine($"{i+1}. {ranmar.Next(factor)}");
            }
            Console.ReadKey();
        }
    }
}
