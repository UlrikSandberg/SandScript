namespace SandScript.Lexer
{
    public enum TokenCategory
    {
        Punctuation, // "->", "=>", ":", ";", ",", "."
        Operator, // "=", "!=",
        Comment,
        Whitespace,
        Grouping, // "{", "}", "[", "]", "(", ")"
        Identifier,
        Keyword,
        Constant,
        Invalid,
        Unknown
    }
}