namespace SandScript.Language.Syntax.Expressions;

public enum ArithmeticOperation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo
}

public class ArithmeticExpression : Expression
{
    public Expression Left { get; }
    public Expression Right { get; }
    public ArithmeticOperation OperatorType { get; }

    public ArithmeticExpression(Expression left, Expression right, ArithmeticOperation operatorType)
    {
        Left = left;
        Right = right;
        OperatorType = operatorType;
    }
}