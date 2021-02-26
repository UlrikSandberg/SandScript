namespace SandScript.AST.Expressions
{
    public enum BinaryOperator
    {
        // Assignment binary operators
        Assign,
        AddAssign,
        SubAssign,
        MulAssign,
        DivAssign,
        ModAssign,
        AndAssign,
        XorAssign,
        OrAssign,
        
        // Logical
        LogicalOr,
        LogicalAnd,
        
        // Equality
        Equal,
        NotEqual,
        
        // Relational
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        
        // Bitwise
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        
        // Shift
        LeftShift,
        RightShift,
        
        // Additive
        Add,
        Sub,
        
        // Multiplicative
        Mul,
        Div,
        Mod,
        
        // Error
        Error
    }
}