using System;

namespace SandScript.Lexer
{
    public class SourceSpan
    {
        public SourceLocation Start { get; }
        public SourceLocation End { get; }

        public SourceSpan(SourceLocation start, SourceLocation end)
        {
            Start = start;
            End = end;
        }

        public int Length => End.Index - Start.Index;

        public override string ToString()
        {
            return $"{Start.Line}:[{Start.Column}], Length: {Length}";
        }
    }
}