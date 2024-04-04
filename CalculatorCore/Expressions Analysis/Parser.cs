using CalculatorCore.Diagnostics;
using System.Collections.Generic;

namespace CalculatorCore.ExpressionsAnalysis
{
    internal sealed class Parser
    {
        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();
        private readonly SyntaxToken[] _tokens;

        private int _position;

        public Parser(string text)
        {
            var lexer = new Lexer(text);

            _tokens = LexTokens(lexer);
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public DiagnosticBag Diagnostics => _diagnostics;

        private SyntaxToken Current => Peek(0);

        public SyntaxTree Parse()
        {
            ExpressionSyntax expresion = ParseExpression();
            SyntaxToken endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new SyntaxTree(_diagnostics, expresion, endOfFileToken);
        }

        private SyntaxToken[] LexTokens(Lexer lexer) 
        {
            var tokens = new List<SyntaxToken>();

            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken)
                    tokens.Add(token);
            }
            while (token.Kind != SyntaxKind.EndOfFileToken);

            return tokens.ToArray();
        }

        private SyntaxToken Peek(int offset)
        {
            int index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
            ExpressionSyntax left;
            int unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();
            if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax operand = ParseExpression(unaryOperatorPrecedence);
                left = new UnaryExpressionSyntax(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                int precedence = Current.Kind.GetBinaryOperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParseExpression(precedence);
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.OpenParenthesisToken:
                {
                    SyntaxToken left = NextToken();
                    ExpressionSyntax expression = ParseExpression();
                    SyntaxToken right = MatchToken(SyntaxKind.CloseParenthesisToken);
                    return new ParenthesizedExpressionSyntax(left, expression, right);
                }
                case SyntaxKind.EKeyword:
                {
                    SyntaxToken keywordToken = NextToken();
                    double value = MathConstants.E;
                    return new LiteralExpressionSyntax(keywordToken, value);
                }
                case SyntaxKind.PIKeyword:
                {
                    SyntaxToken keywordToken = NextToken();
                    double value = MathConstants.PI;
                    return new LiteralExpressionSyntax(keywordToken, value);
                }
                default:
                {
                    SyntaxToken numberToken = MatchToken(SyntaxKind.NumberToken);
                    return new LiteralExpressionSyntax(numberToken);
                }
            }
        }

        private SyntaxToken NextToken()
        {
            SyntaxToken current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

            _diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, Current.Position, null, null);
        }
    }
}