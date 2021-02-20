using SandScript.Lexer;

namespace SandScript.AST
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxCategory Category { get; }
        public abstract SyntaxType Type { get; }
        public SourceSpan Span { get; }

        protected SyntaxNode(SourceSpan span)
        {
            Span = span;
        }
    }
}