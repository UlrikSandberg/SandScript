namespace SandScript.Language;

public class SyntaxError
{
    public string Message { get; }
    public string[] Lines { get; }
    public Severity Severity { get; }
    public SourceSpan SourceSpan { get; }

    public SyntaxError(string message, string[] lines, Severity severity, SourceSpan sourceSpan)
    {
        Message = message;
        Lines = lines;
        Severity = severity;
        SourceSpan = sourceSpan;
    }
}