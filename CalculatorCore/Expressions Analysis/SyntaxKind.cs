namespace CalculatorCore.ExpressionsAnalysis
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        TrigonometricalFunctionToken,
        MathFunctionToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        CaretToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,

        // Keywords
        EKeyword,
        PIKeyword
    }
}
