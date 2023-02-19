namespace SandScript.Language;


public class SourceCode
{
    public string RawCode { get; private set; }

    public int Size => RawCode.Length;

    public SourceCode(string rawCode)
    {
        RawCode = rawCode;
    }

    public void Append(string rawCode)
    {
        RawCode += rawCode;
    }

    public static SourceCode Empty => new (string.Empty);
}