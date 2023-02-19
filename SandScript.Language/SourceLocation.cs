namespace SandScript.Language;

public class SourceLocation
{
    public SourceCode SourceCode { get; }
    public int Index { get; }
    public int Line { get; }
    public int Column { get; }

    public SourceLocation(SourceCode sourceCode, int index, int line, int column)
    {
        SourceCode = sourceCode;
        Index = index;
        Line = line;
        Column = column;
    }
}