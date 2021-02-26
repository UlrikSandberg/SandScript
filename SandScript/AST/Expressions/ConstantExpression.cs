using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class ConstantExpression : Expression
    {
        public string Value { get; }
        public ConstantType ConstantType { get; }
        public override SyntaxType Type { get; } = SyntaxType.ConstantExpression;
        public ConstantExpression(SourceSpan span, string value, ConstantType constantType) : base(span)
        {
            Value = value;
            ConstantType = constantType;
        }
    }

    public enum ConstantType
    {
        Invalid,
        Integer,
        Float,
        String,
        Boolean
    }
}