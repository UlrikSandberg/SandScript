using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class BinaryExpression : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }
        public BinaryOperator Op { get; }
        public override SyntaxType Type { get; } = SyntaxType.BinaryExpression;
        
        public BinaryExpression(SourceSpan span, Expression left, Expression right, BinaryOperator op) : base(span)
        {
            Left = left;
            Right = right;
            Op = op;
        }
    }
}