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
            throw new LexicalException(nameof(ScanWhiteSpace), " ", Current.ToString());
            
        while (IsWhiteSpace())
        {
            Consume();
        }

        return CreateToken(TokenType.Whitespace);
    }

    private Token ScanStringLiteral()
    {
        if (!IsStringLiteral())
            throw new LexicalException(nameof(ScanWhiteSpace), '"', Current);
        
        // Start by Advancing for the actual string literal value we do not want to include the initial '"'
        Advance();

        while (Current != '"')
        {
            // If we encounter EOF before ending quotation marks throw exception
            if (IsEOF())
                throw new LexicalException("Encountered unexpected end of file while scanning StringLiteral.");

            // If we encounter backslash we should 
            if (Current == '\\')
            {
                Advance();
                char c = ScanEscapeSequence();
                Advance();
                tokenBuilder.Append(c);
                continue;
            }
            
            Consume();
        }
        
        // End by advancing beyond the end '"' we dont want it to be part of the actual string value.
        Advance();
        
        return CreateToken(TokenType.StringLiteral);
    }

    private Token ScanIdentifier()
    {
        if (!IsIdentifier())
            throw new LexicalException(nameof(ScanIdentifier), "_ or alphanumeric", Current.ToString());
        
        while (IsLetterOrDigit() || Current == '_')
        {
            if (IsEOF())
                throw new LexicalException("Encountered unexpected EOF");
            
            Consume();
        }

        return keywords.Contains(tokenBuilder.ToString())
            ? CreateToken(TokenType.Keyword)
            : CreateToken(TokenType.Identifier);
    }
    
    private char ScanEscapeSequence()
    {
        switch (Current)
        {
            case '\\':
                return '\\';
            case 'n':
                return '\n';
            case 't':
                return '\t';
            case '"':
                return '"';
            case 'b':
                return '\b';
            case 'r':
                return '\r';
            
            default:
                return '\0';
        }
    }

    // Somewhere in the lexical \n analysis we have encountered an unexpected char
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
