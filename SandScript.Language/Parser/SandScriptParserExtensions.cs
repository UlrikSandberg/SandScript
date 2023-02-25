namespace SandScript.Language.Parser;

public static class SandScriptParserExtensions
{
    public static string HexStringToNumberString(this string hexString)
    {
        var value = hexString.Substring(2);
        return Convert.ToInt64(value, 16).ToString();
    }

    public static string OctalStringToNumberString(this string octalString)
    {
        var value = octalString.Substring(2);
        return Convert.ToInt64(value, 8).ToString();
    }
}