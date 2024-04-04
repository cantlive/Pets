using CalculatorCore.Diagnostics;
using CalculatorCore.ExpressionsAnalysis;
using System.Collections.Generic;

namespace CalculatorCore
{
    public class Calculator
    {
        private readonly List<Diagnostic> _diagnostics = new List<Diagnostic>();
        private double _result;

        public double Result => _result;
        public IReadOnlyList<Diagnostic> Diagnostics => _diagnostics;

        public void Calculate(string input)
        {
            SyntaxTree syntaxTree = SyntaxTree.Parse(input);
            if (syntaxTree.Diagnostics.Count > 0)
                _diagnostics.AddRange(syntaxTree.Diagnostics);

            _result = Evaluator.Evaluate(syntaxTree.Root);
        }
    }
}
