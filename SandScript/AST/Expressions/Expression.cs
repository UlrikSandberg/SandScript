using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public abstract class Expression : SyntaxNode
    {
        public override SyntaxCategory Category { get; } = SyntaxCategory.Expression;

        public Expression(SourceSpan span) : base(span)
        {
        }
    }
}