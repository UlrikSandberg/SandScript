using SandScript.Lexer;

namespace SandScript.AST.Statements
{
    public abstract class Statement : SyntaxNode
    {
        public override SyntaxCategory Category { get; } = SyntaxCategory.Statement;
        
        protected Statement(SourceSpan span) : base(span)
        {
        }
    }
}