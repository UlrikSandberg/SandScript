namespace SandScript.Language.Lexer;

public static class StringExtension
{
    public static char CharAt(this string source, int index)
    {
        if (source == null || index < 0 || index >= source.Length)
        {
            return char.MinValue;
        }

        return source[index];
    }
}