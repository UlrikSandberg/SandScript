namespace SandScript.Extensions
{
    public static class CharExtensions
    {
        public static bool IsEOF(this char ch)
        {
            return ch == '\0';
        }

        public static bool IsNewLine(this char ch)
        {
            return ch == '\n';
        }

        public static bool IsWhiteSpace(this char ch)
        {
            return char.IsWhiteSpace(ch) || ch.IsEOF();
        }

        public static bool IsDigit(this char ch)
        {
            return char.IsDigit(ch);
        }

        public static bool IsLetter(this char ch)
        {
            return char.IsLetter(ch);
        }

        public static bool IsLetterOrDigit(this char ch)
        {
            return char.IsLetterOrDigit(ch);
        }

        public static bool IsIdentifierOrkeyword(this char ch)
        {
            return ch.IsLetterOrDigit() || ch == '_';
        }
        
        public static bool IsQuotationMarks(this char ch)
        {
            return ch == '"' || ch == '\'';
        }

        public static bool IsPunctuation(this char ch)
        {
            return "<>{}[]()!%^&*+-=/.,?;:|".Contains($"{ch}");
        }
    }
}