using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class FieldDeclaration : Declaration
    {
        public override SyntaxType Type { get; } = SyntaxType.FieldDeclaration;
        
        public FieldDeclaration(SourceSpan span, string name) : base(span, name)
        {
        }
    }
}