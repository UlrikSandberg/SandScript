using System.Text;

namespace SandScript.Language.Lexer;

public partial class SandScriptLexer
{
    public ErrorSink ErrorSink { get; }

    private static string punctuation = "{}()[].;,";
    private static string operators = "+-=%&|?:!><*/^";
    private static List<string> keywords = new()
    {
        // Conditional
        "if", "else", 
        
        // Iterations
        "while", "for",
        
        // try catch
        "try", "catch", "raise",
        
        // class declarations
        "new", "class",
        
        // variable declaration
        "var",
        
        // Method declarations
        "void", "return",
        
        // Contextual keywords
        "is",
        
        // Access modifiers
        "public", "private", "protected",
        
        // modifiers
        "static",
        
        // imports
        "import",
    };

    // The purpose of this Lexer is to tokenize a given source file/text.
    private SourceCode sourceCode;
    private int line;
    private int column;
    private int index;
    
    private StringBuilder tokenBuilder;
    private SourceLocation previousTokenEndLocation;

    private char Current => sourceCode.RawCode.CharAt(index);
    private char Last => sourceCode.RawCode.CharAt(index - 1);
    private char Next => sourceCode.RawCode.CharAt(index + 1);
    
    public SandScriptLexer() : this(string.Empty) { }
    public SandScriptLexer(string sourceCode) : this(new SourceCode(sourceCode), new ErrorSink()) { }
    
    public SandScriptLexer(SourceCode sourceCode, ErrorSink errorSink)
    {
        ErrorSink = errorSink;
        LoadSourceCode(sourceCode);
    }

    private void Advance()
    {
        index++;
        column++;
    }

    public void Feed(string sourceCode)
    {
        if (IsEOF())
        {
            LoadSourceCode(new SourceCode(sourceCode));
        }
        else
        {
            this.sourceCode.Append(sourceCode);
        }
    }

    private void Consume()
    {
        tokenBuilder.Append(Current);
        Advance();
    }

    private void AddError(string message, Severity severity)
    {
        var sourceSpan = new SourceSpan(sourceCode, previousTokenEndLocation,
            new SourceLocation(sourceCode, index, line, column));
        ErrorSink.AddEntry(message, sourceCode, severity, sourceSpan);
    }
    
    private void LoadSourceCode(SourceCode sourceCode)
    {
        this.sourceCode = sourceCode;
        tokenBuilder = new StringBuilder();
        line = 1;
        column = 0;
        index = 0;
        previousTokenEndLocation = new SourceLocation(sourceCode, index, line, column);
    }
    
    public Token CreateToken(TokenType type)
    {
        var tokenValue = tokenBuilder.ToString();
        var sourceStart = previousTokenEndLocation;
        var sourceEnd = new SourceLocation(sourceCode, index, line, column);

        // Move previousTokenEndLocation to this token.
        previousTokenEndLocation = sourceEnd;
        tokenBuilder.Clear();

        return new Token(tokenValue, type, sourceCode, sourceStart, sourceEnd);
    }
    
    /**
     * Complete a lexical analysis of the current loaded sourceCode
     */
    public IEnumerable<Token> LexSourceCode()
    {
        // Create first initial BOF token
        Token lastToken = CreateToken(TokenType.BOF);
        yield return lastToken;
        
        // Lex tokens until encountering EOF
        while (!IsEOF())
        {
            lastToken = LexToken();
            if (lastToken.TokenType == TokenType.NewLine) 
            {
                // If token is newline e.g '\n' advance the the line number and reset column index
                line++;
                column = 0;
            }
            yield return lastToken;
        }
        
        // EOF encountered - return last token as EOF
        yield return CreateToken(TokenType.EOF);
    }

    public Token LexSingleToken(Func<Token, bool> predicate)
    {
        if (IsEOF())
        {
            return CreateToken(TokenType.EOF);
        }
        
        var token = LexToken();

        if (!predicate(token))
        {
            return LexSingleToken(predicate);
        }

        return token;
    }
    
    /**
     * Lexing the next token is a process of detecting the upcomming char(s) identifying
     * what token type is next and then starting a scanning procedure that will consume
     * the 'X' amount of chars that make of the respective token.
     */
    public Token LexToken()
    {
        if (IsComment())
            return ScanComment();

        if (IsNewLine())
            return ScanNewLine(true)!;

        if (IsWhiteSpace())
            return ScanWhiteSpace();

        if (IsStringLiteral())
            return ScanStringLiteral();

        if (IsIdentifier())
            return ScanIdentifier();

        if (IsPunctuation())
            return ScanPunctuation();
        
        if (IsOperator())
            return ScanOperator();

        if (IsHexadecimal())
            return ScanHex();

        if (IsOctal())
            return ScanOctal();

        if (IsDigit())
            return ScanNumber(false);
        
        
        return ScanUnexpectedToken();
    }
    
    private bool IsEOF() => Current == '\0';
    private bool IsComment() => Current == '/' && (Next == '/' || Next == '*');
    private bool IsNewLine() => Current == '\n';
    private bool IsWhiteSpace() => (char.IsWhiteSpace(Current) || IsEOF()) && !IsNewLine();
    private bool IsStringLiteral() => Current == '"';
    private bool IsIdentifier() => char.IsLetter(Current) || Current == '_';
    private bool IsLetterOrDigit() => char.IsLetterOrDigit(Current);
    private bool IsPunctuation() => punctuation.Contains(Current);
    private bool IsOperator() => operators.Contains(Current);
    private bool IsOctal() => Current == '0' && Next == 'c';
    private bool IsHexadecimal() => Current == '0' && Next == 'x';
    private bool IsDigit() => !IsEOF() && char.IsDigit(Current);
    private bool IsHexDigit(char ch) => !IsEOF() && ((ch >= '0' && ch <= '9')
                                                     || (ch >= 'a' && ch <= 'f')
                                                     || (ch >= 'A' && ch <= 'F'));

    private bool IsOctalDigit() => Current is >= '0' and <= '7';
}
