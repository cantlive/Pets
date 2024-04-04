namespace CalculatorCore.ExpressionsAnalysis
{
    internal static class SyntaxFacts
    {
        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.MathFunctionToken:
                    return 5;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                default:
                    return 0;
            }
        }

        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.CaretToken:
                    return 3;

                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 2;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "exp":
                case "sqrt":
                case "ln":
                case "sign":
                case "abs":
                case "sin":
                case "cos":
                case "tg":
                case "ctg":
                case "arcsin":
                case "arccos":
                case "arctg":
                case "arcctg":
                case "sinh":
                case "cosh":
                case "tgh":
                case "ctgh":
                    return SyntaxKind.MathFunctionToken;
                case "e":
                    return SyntaxKind.EKeyword;
                case "pi":
                    return SyntaxKind.PIKeyword;
                default:
                    return SyntaxKind.BadToken;
            }
        }
    }
}
