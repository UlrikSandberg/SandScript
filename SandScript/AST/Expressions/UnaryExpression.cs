using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class UnaryExpression : Expression
    {
        public Expression Argument { get; }
        public UnaryOperator UnaryOperator { get; }
        public override SyntaxType Type { get; } = SyntaxType.UnaryExpression;
        
        public UnaryExpression(SourceSpan span, Expression argument, UnaryOperator unaryOperator) : base(span)
        {
            Argument = argument;
            UnaryOperator = unaryOperator;
        }
    }
}