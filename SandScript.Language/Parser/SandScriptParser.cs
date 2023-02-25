using SandScript.Language.Lexer;
using SandScript.Language.Syntax;

namespace SandScript.Language.Parser;

public partial class SandScriptParser
{
    private readonly SandScriptLexer _lexer;

    private Token last;
    private Token current;
    private Token next;
    
    public SandScriptParser() : this(new SandScriptLexer()) { }

    public SandScriptParser(SandScriptLexer lexer)
    {
        _lexer = lexer;
    }

    private void Advance()
    {
        last = current;
        current = next;
        next = _lexer.LexSingleToken(token => !token.IsTrivia());
    }

    private T Advance<T>(Func<T> action)
    {
        var result = action();
        Advance();
        return result;
    }

    private Token ConsumeToken(TokenType type)
    {
        if (current != type)
            throw UnexpectedToken(type.ToString().ToLower());
        
        Advance();
        return last;
    }

    /// <summary>
    /// Populate Current and next variables with a token
    /// </summary>
    private void InitializeParsing()
    {
        Advance();
        Advance();
    }
    
    public SourceRoot ParseScript(string source)
    {
        _lexer.Feed(source);
        InitializeParsing();
        var rootNodes = new List<SyntaxNode>();

        try
        {
            while (current != TokenType.EOF)
            {
                if (current == "import")
                {
                    // Parse import statement
                } 
                else if (current == "public" || current == "private" || current == "protected" || current == "class")
                {
                    // parse class or method
                }
                else
                {
                    // Parse singular statements
                    rootNodes.Add(ParseStatement());
                }
            }
        }
        catch (SyntaxException)
        {
            throw;
        }

        return new SourceRoot(rootNodes);
    }

    private SyntaxException UnexpectedToken(string expected)
    {
        string message = $"{current.SourceSpan.Start} {current.SourceSpan} Unexpected Token '{current.Value}'. Expected {expected}";
        return new SyntaxException(message);
    }
    
    private SyntaxException UnexpectedToken(Token value, string expected)
    {
        string message = $"{value.SourceSpan.Start} {value.SourceSpan} Unexpected Token '{value.Value}'. Expected {expected}";
        return new SyntaxException(message);
    }
}