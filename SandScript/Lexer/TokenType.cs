namespace SandScript.Lexer
{
    public enum TokenType
    {
        EOF,  // "End of File",
        WhiteSpace, // " "
        Error, // "Error Token
        Float, // A float literal value constant
        Integer, // A integer literal value constant
        String, // A string literal constant value
        LineComment, // A single outcommented line
        BlockComment, // A block comment of one or multiple lines
        Keyword, // A token which indicates one of the valid keywords
        Identifier, // A token which represents an identifier
        Semicolon,
        Colon,
        LeftCurlyBracket,
        RightCurlyBracket,
        LeftSquareBracket,
        RightSquareBracket,
        LeftParenthesis,
        RightParenthesis,
        GreaterThan,
        GreaterThanOrEqual,
        BitShiftRight,
        LessThan,
        LessThanOrEqual,
        BitShiftLeft,
        PlusEqual,
        PlusPlus,
        Plus,
        MinusEqual,
        RightArrow,
        LeftArrow,
        MinusMinus,
        Minus,
        Equal,
        FatArrow,
        Assignment,
        NotEqual,
        Not,
        MulEqual,
        MulMul,
        Mul,
        DivideEqual,
        Divide,
        Dot,
        Comma,
        BoolAnd,
        BitwiseAndEqual,
        BitwiseAnd,
        BoolOr,
        BitwiseOrEqual,
        BitwiseOr,
        ModEqual,
        Mod,
        BitwiseXorEqual,
        BitwiseXor,
        Power,
        DoubleQuestion,
        Question,
        NewLine
    }
}

