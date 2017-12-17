using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent2.Sibling1
{
    public static class SiblingClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent2.Sibling1";

        public static void SiblingMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(SiblingClass)}.{nameof(SiblingMethod)}");
        }
    }
}
