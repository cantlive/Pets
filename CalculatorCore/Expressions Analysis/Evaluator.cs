using System;

namespace CalculatorCore.ExpressionsAnalysis
{
    public static class Evaluator
    {
        public static double Evaluate(ExpressionSyntax root)
        {
            return EvaluateExpression(root);
        }

        private static double EvaluateExpression(ExpressionSyntax node)
        {
            if (node is LiteralExpressionSyntax n)
            {
                if (n.Value == null)
                    return 0;

                return (double)n.Value;
            }

            if (node is UnaryExpressionSyntax unaryExpressionSyntax)
            {
                double operand = EvaluateExpression(unaryExpressionSyntax.Operand);
                string funcName = unaryExpressionSyntax.OperatorToken.Text;

                switch (unaryExpressionSyntax.OperatorToken.Kind)
                {
                    case SyntaxKind.MathFunctionToken:
                        return FunctionsEvaluator.EvaluateFunction(funcName, operand);
                    case SyntaxKind.PlusToken:
                        return operand;
                    case SyntaxKind.MinusToken:
                        return -operand;
                    default:
                        throw new Exception($"Unexpected unary operator {unaryExpressionSyntax.OperatorToken.Kind}");
                }
            }

            if (node is BinaryExpressionSyntax binaryExpressionSyntax)
            {
                double left = EvaluateExpression(binaryExpressionSyntax.Left);
                double right = EvaluateExpression(binaryExpressionSyntax.Right);

                switch (binaryExpressionSyntax.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return left + right;
                    case SyntaxKind.MinusToken:
                        return left - right;
                    case SyntaxKind.StarToken:
                        return left * right;
                    case SyntaxKind.SlashToken:
                        return left / right;
                    case SyntaxKind.CaretToken:
                        return Math.Pow(left, right);
                    default:
                        throw new Exception($"Unexpected binary operator {binaryExpressionSyntax.OperatorToken.Kind}");
                }
            }

            if (node is ParenthesizedExpressionSyntax p)
                return EvaluateExpression(p.Expression);

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
