using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class MethodDeclaration : Declaration
    {
        public override SyntaxType Type { get; } = SyntaxType.MethodDeclaration;
        
        public MethodDeclaration(SourceSpan span, string name) : base(span, name)
        {
        }
    }
}