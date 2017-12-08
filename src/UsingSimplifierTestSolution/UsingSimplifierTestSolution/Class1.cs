using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution
{
    public class Class1
    {
        public static void JustAMethod(List<string> strings)
        {
            strings.ForEach(s => Console.WriteLine($"item: {s}"));
        }
    }
}
