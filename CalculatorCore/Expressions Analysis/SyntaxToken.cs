using CalculatorCore.Diagnostics;

namespace CalculatorCore.ExpressionsAnalysis
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value = null)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }

        public override TextSpan Span => new TextSpan(Position, Text.Length);
        public int Position { get; }
        public string Text { get; }
        public object Value { get; }
    }
}
