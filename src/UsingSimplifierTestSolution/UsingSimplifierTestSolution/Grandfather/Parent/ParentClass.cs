using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent
{
    class ParentClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent";

        public static void ParentMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(ParentClass)}.{nameof(ParentMethod)}");
        }
    }
}
