namespace SandScript.Lexer
{
    public class LexerError
    {
        public string Message { get; }
        public string[] Lines { get; }
        public Severity Severity { get; }
        public SourceSpan Span { get; }

        public LexerError(string message, string[] lines, Severity severity, SourceSpan span)
        {
            Message = message;
            Lines = lines;
            Severity = severity;
            Span = span;
        }
    }
}


