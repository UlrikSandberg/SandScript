namespace SandScript.Lexer
{
    public enum TokenType
    {
        If, // "if"
        Else, // "else"
        True, // "true"
        False, // "false"
        Comma, // ","
        Comment, // "//"
        OpenParenthesis, // "("
        CloseParenthesis, // ")"
        OpenSquareBracket, // "["
        CloseSquareBracket, // "]"
        OpenCurlyBrace, // "{"
        CloseCurlyBrace, // "}"
        Equal, // Assignment "="
        Plus, // "+"
        PlusEqual, // "+="
        Minus, // "-"
        MinusEqual, // "-="
        Multiply, // "*"
        MultiplyEqual, // "*="
        Divide, // "/"
        DivideEqual, // "/="
        Int, // whole number 10 16-bit
        Float, // decimal number 10.23 32-bit
        Double, // decimal number 
        Identifier, // 
        Return,
        Function,
        Interface,
        Eq, // Equality operator ==
        Neq, // Not equal operator !=
        While, // "while"
        For, // "for"
        Foreach, // "foreach"
        Void, // "void"
        EOL, // "End of Line"
        EOF,  // "End of File",
        WhiteSpace // " "
    }
}

