using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public abstract class Declaration : SyntaxNode
    {
        public string Name { get; }
        public override SyntaxCategory Category { get; } = SyntaxCategory.Declaration;

        protected Declaration(SourceSpan span, string name) : base(span)
        {
            Name = name;
        }
    }
}