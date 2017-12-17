using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent2.Me
{
    public class MeClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent2.Me";

        public static void MeMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(MeClass)}.{nameof(MeMethod)}");
        }
    }
}
