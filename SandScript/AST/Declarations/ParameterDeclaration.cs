using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class ParameterDeclaration : Declaration
    {
        public string ParamterType { get; }
        public override SyntaxType Type { get; } = SyntaxType.ParameterDeclaration;
        
        public ParameterDeclaration(SourceSpan span, string name, string parameterType) : base(span, name)
        {
            ParamterType = parameterType;
        }
    }
}