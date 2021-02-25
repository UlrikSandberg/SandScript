using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class PropertyDeclaration : Declaration
    {
        public override SyntaxType Type { get; } = SyntaxType.PropertyDeclaration;
        
        public PropertyDeclaration(SourceSpan span, string name) : base(span, name)
        {
        }
    }
}