namespace SandScript.Language.Syntax.Expressions;

public enum LiteralType
{
    String,
    Int,
    Float,
    Boolean,
    Null
}

public class LiteralExpression : Expression
{
    public string Value { get; }
    public LiteralType LiteralType { get; }

    public LiteralExpression(string value, LiteralType literalType)
    {
        Value = value;
        LiteralType = literalType;
    }
}