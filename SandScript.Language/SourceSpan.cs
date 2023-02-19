namespace SandScript.Language;

public class SourceSpan
{
    public SourceLocation Start { get; }
    public SourceLocation End { get; }
    public SourceCode SourceCode { get; }

    public int Size => End.Index - Start.Index;

    public SourceSpan(SourceCode sourceCode, SourceLocation start, SourceLocation end)
    {
        Start = start;
        End = end;
        SourceCode = sourceCode;
    }
}