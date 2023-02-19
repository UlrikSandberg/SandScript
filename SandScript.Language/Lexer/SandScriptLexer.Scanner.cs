namespace SandScript.Language.Lexer;

public partial class SandScriptLexer
{
    /***********
     * All scanners assume that they are called in the appropriate context.
     */ 
    
    private Token ScanComment()
    {
        if (!IsComment())
            throw new LexicalException(nameof(ScanComment), "/* or //", $"{Current}{Next}");
            
        // Ingest the first '/' of the comment into the tokenBuilder
        Consume();
        
        // Check block comment
        var blockComment = Current == '*';

        // If block comment we need to ingest until we are currently on '/' and the last char was '*'
        if (blockComment)
        {
            while (!IsEOF() && Current != '/' && Last != '*')
            {
                if (IsNewLine())
                    ScanNewLine(false);
                
                Consume();   
            }
            
            // The while loop breaks when we are on the comment blocks ending forward slash '/' 
            // We need to consume that char as part of the BlockComment token.
            Consume();
        }
        else // if line comment, we ingest until we meet '\n'
        {
            while (!IsEOF() && Current != '\n')
            {
                Consume();
            }
        }
        
        return CreateToken(blockComment ? TokenType.BlockComment : TokenType.LineComment);
    }

    private Token? ScanNewLine(bool createToken)
    {
        if (!IsNewLine())
            throw new LexicalException(nameof(ScanNewLine), '\n', Current);
        
        Consume();
        line++;
        column = 0;
        
        return createToken ? CreateToken(TokenType.NewLine) : null;
    }

    private Token ScanWhiteSpace()
    {
        if (!IsWhiteSpace())
        {
            while (IsWhiteSpace())
            {
                Consume();
            }
        }

        return CreateToken(TokenType.Whitespace);
    }

    private Token ScanStringLiteral()
    {
        return null;
    }

    // Somewhere in the lexical analysis we have encountered an unexpected char
    // Scan until we meet either whitespace, EOF or a punctuation. We will consume.
    private Token ScanUnexpectedToken(Severity severity = Severity.Fatal, string message = "Unexpected Token '{0}'")
    {
        while (!IsWhiteSpace() && !IsEOF())
        {
            Consume();
        }
        
        AddError(string.Format(message, tokenBuilder), severity);
        return CreateToken(TokenType.Error);
    }
}