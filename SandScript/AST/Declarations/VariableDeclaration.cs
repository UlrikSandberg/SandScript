using SandScript.AST.Expressions;
using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class VariableDeclaration : Declaration
    {
        public string VariableType { get; }
        public Expression Value { get; }
        public override SyntaxType Type { get; } = SyntaxType.VariableDeclaration;
        
        public VariableDeclaration(SourceSpan span, string name, string variableType, Expression value) : base(span, name)
        {
            VariableType = variableType;
            Value = value;
        }
    }
}