using CalculatorCore;
using CalculatorCore.Diagnostics;
using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();

            while (true)
            {
                Console.Write(">> ");
                string input = Console.ReadLine();
                calculator.Calculate(input);

                if (calculator.Diagnostics.Count > 0)
                {
                    foreach (Diagnostic diagnostic in calculator.Diagnostics)
                        Console.WriteLine(diagnostic);
                }

                Console.WriteLine(calculator.Result);
            }
        }
    }
}
