using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class IdentifierExpression : Expression
    {
        public override SyntaxType Type { get; } = SyntaxType.IdentifierExpression;
        
        public string Identifier { get; }
        
        public IdentifierExpression(SourceSpan span, string identifier) : base(span)
        {
            Identifier = identifier;
        }
    }
}