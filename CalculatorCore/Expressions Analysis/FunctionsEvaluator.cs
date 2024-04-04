using System;

namespace CalculatorCore.ExpressionsAnalysis
{
    internal static class FunctionsEvaluator
    {
        public static double EvaluateFunction(string funcName, double value)
        {
            return funcName switch
            {
                "exp" => Math.Pow(Math.E, value),
                "sqrt" => Math.Sqrt(value),
                "ln" => Math.Log(value),
                "sign" => Math.Sign(value),
                "abs" => Math.Abs(value),
                "sin" => Math.Sin(value),
                "cos" => Math.Cos(value),
                "tg" => Math.Tan(value),
                "ctg" => 1.0 / Math.Tan(value),
                "arcsin" => Math.Asin(value),
                "arccos" => Math.Acos(value),
                "arctg" => Math.Atan(value),
                "arcctg" => Math.PI / 2 - Math.Atan(value),
                "sinh" => Math.Sinh(value),
                "cosh" => Math.Cosh(value),
                "tgh" => Math.Tanh(value),
                "ctgh" => (Math.Exp(value) + Math.Exp(-value)) / (Math.Exp(value) - Math.Exp(-value)),
                _ => throw new Exception($"Unexpected math function <{funcName}>"),
            };
        }
    }
}
