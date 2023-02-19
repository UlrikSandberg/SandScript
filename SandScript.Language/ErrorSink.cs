namespace SandScript.Language;

public class ErrorSink
{
    private readonly List<SyntaxError> _errors;

    public IEnumerable<SyntaxError> Errors => _errors.AsReadOnly();

    public bool HasError => Errors.Any();

    public ErrorSink()
    {
        _errors = new List<SyntaxError>();
    }

    public void Clear() => _errors.Clear();

    public void AddEntry(string message, SourceCode sourceCode, Severity severity, SourceSpan span)
    {
        _errors.Add(new SyntaxError(
            message, 
            sourceCode.RawCode.Substring(span.Start.Index, span.End.Index - span.Start.Index).Split('\n'),
            severity,
            span));
    }
}