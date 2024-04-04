namespace CalculatorCore.ExpressionsAnalysis
{
    internal struct Expression
    {
        public string ExpressionString { get; }

        public Expression(string expression)
        {
            ExpressionString = expression;
        }
    }
}
