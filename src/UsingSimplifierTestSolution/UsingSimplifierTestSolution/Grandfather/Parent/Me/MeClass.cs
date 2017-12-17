using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingSimplifierTestSolution.Grandfather.Parent.Me
{
    public static class MeClass
    {
        public static string Ns { get; } = "UsingSimplifierTestSolution.Grandfather.Parent.Me";

        public static void MeClassMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(MeClass)}.{nameof(MeClassMethod)}");
        }
    }
}

namespace UsingSimplifierTestSolution.Grandfather.Parent.WhatIfIamAnImpersonator
{
    public static class MeClass
    {
        public static string Ns { get; } = 
            "UsingSimplifierTestSolution.Grandfather.Parent.WhatIfIAmAnImpersonator";

        public static void MeClassMethod()
        {
            Console.WriteLine($"{Ns}.{nameof(MeClass)}.{nameof(MeClassMethod)}");
        }
    }
}
