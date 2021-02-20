using System;

namespace SandScript.Lexer
{
    public class SourceLocation
    {
        public int Index { get; }
        public int Line { get; }
        public int Column { get; }

        public SourceLocation(int index, int line, int column)
        {
            Index = index;
            Line = line;
            Column = column;
        }
    }
}