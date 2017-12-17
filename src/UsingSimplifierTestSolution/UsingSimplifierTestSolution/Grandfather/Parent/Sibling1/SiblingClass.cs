using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent.Sibling1
{
    public static class SiblingClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent.Sibling1";

        public static void SiblingClassMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(SiblingClass)}.{nameof(SiblingClassMethod)}");
        }
    }
}
