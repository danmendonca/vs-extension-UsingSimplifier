using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent2
{
    public class SiblingClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent2";

        public static void ParentMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(SiblingClass)}.{nameof(ParentMethod)}");
        }
    }
}
