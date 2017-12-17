using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather
{
    public static class GrandfatherClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather";

        public static void GrandfatherMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(GrandfatherClass)}.{nameof(GrandfatherMethod)}");
        }
    }
}
