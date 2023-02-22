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

    private Token ScanOperator()
    {
        if (!IsOperator())
            throw new LexicalException(nameof(ScanOperator), $"Expected on of '{operators}'", Current.ToString());

        switch (Current)
        {
            case '+':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.AssignmentAddition);
                }

                if (Current == '+')
                {
                    Consume();
                    return CreateToken(TokenType.PlusPlus);
                }

                return CreateToken(TokenType.Addition);
            
            case '-':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.AssignmentSubtraction);
                }

                if (Current == '-')
                {
                    Consume();
                    return CreateToken(TokenType.MinusMinus);
                }

                return CreateToken(TokenType.AssignmentSubtraction);
            
            case '*':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.AssignmentMultiply);
                }

                if (Current == '*')
                {
                    Consume();
                    return CreateToken(TokenType.MultMult);
                }

                return CreateToken(TokenType.Multiply);
            
            case '/':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.AssignmentDivide);
                }

                return CreateToken(TokenType.Divide);
            
            case '%':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.AssignmentModulo);
                }

                return CreateToken(TokenType.Modulo);
            
            case '=':
                Consume();
                if (Current == '>')
                {
                    return CreateToken(TokenType.FatArrow);
                }

                if (Current == '=')
                {
                    return CreateToken(TokenType.LogicalEqual);
                }

                return CreateToken(TokenType.Assignment);
            
            case '!':
                Consume();
                if (Current == '=')
                {
                    Consume();
                    return CreateToken(TokenType.LogicalNotEqual);
                }

                return CreateToken(TokenType.LogicalNot);
            
            case '&':
                Consume();
                if (Current == '&')
                {
                    Consume();
                    return CreateToken(TokenType.LogicalAnd);
                }

                return CreateToken(TokenType.BitwiseAnd);
                
            case '|':
                Consume();
                if (Current == '|')
                {
                    Consume();
                    return CreateToken(TokenType.LogicalOr);
                }

                return CreateToken(TokenType.BitwiseOr);
            
            case '^':
                Consume();
                return CreateToken(TokenType.BitwiseXor);

            case '?':
                Consume();
                return CreateToken(TokenType.QuestionMark);
            
            case ':':
                Consume();
                return CreateToken(TokenType.Colon);
            
            case '>':
                Consume();
                if (Current == '=')
                {
                    return CreateToken(TokenType.LogicalGreaterOrEqual);
                }

                if (Current == '>')
                {
                    return CreateToken(TokenType.BitwiseShiftRight);
                }

                return CreateToken(TokenType.LogicalGreater);
            
            case '<':
                Consume();
                if (Current == '=')
                {
                    return CreateToken(TokenType.LogicalLessOrEqual);
                }

                if (Current == '<')
                {
                    return CreateToken(TokenType.BitwiseShiftLeft);
                }
                
                return CreateToken(TokenType.LogicalLess);
            
            default:
                return ScanUnexpectedToken();
        }
    }

    private Token ScanPunctuation()
    {
        if (!IsPunctuation())
            throw new LexicalException(nameof(ScanPunctuation), $"Expected one of {punctuation}", Current.ToString());
        
        switch (Current)
        {
            case '{':
                Consume();
                return CreateToken(TokenType.OpenCurlyBracket);
            case '}':
                Consume();
                return CreateToken(TokenType.CloseCurlyBracket);
            case '[':
                Consume();
                return CreateToken(TokenType.OpenSquareBracket);
            case ']':
                Consume();
                return CreateToken(TokenType.CloseSquareBracket);
            case '(':
                Consume();
                return CreateToken(TokenType.OpenParenthesis);
            case ')':
                Consume();
                return CreateToken(TokenType.CloseParenthesis);
            case '.':
                Consume();
                return CreateToken(TokenType.Dot);
            case ';':
                Consume();
                return CreateToken(TokenType.SemiColon);
            case ',':
                Consume();
                return CreateToken(TokenType.Comma);
            
            default:
                return ScanUnexpectedToken(Severity.Fatal, "Unexpected Token '{0}' - Expected punctuation" + punctuation);
        }
    }

    private Token ScanNumber(bool scanningFloat)
    {
        while (IsDigit())
        {
            Consume();
        }

        if (Current == 'f')
        {
            Advance();
            return CreateToken(TokenType.FloatingPointLiteral);
        }

        if (!scanningFloat && Current == '.')
        {
            Consume();
            return ScanNumber(true);
        }
        
        // After consuming hex digits we expect there to be either whitespace eof or symbols
        if (!IsEOF() && !IsWhiteSpace() && !IsPunctuation() && !IsOperator())
        {
            return ScanUnexpectedToken();
        }

        if (scanningFloat)
        {
            return CreateToken(TokenType.FloatingPointLiteral);
        }

        return CreateToken(TokenType.IntegerLiteral);
    }

    private Token ScanHex()
    {
        if (!IsHexadecimal())
            throw new LexicalException(nameof(ScanHex), "0x", $"{Current}${Next}");
        
        // Consume both '0' and 'x'
        Consume();
        Consume();

        while (IsHexDigit(Current))
        {
            Consume();
        }
        
        // After consuming hex digits we expect there to be either whitespace eof or symbols
        if (!IsEOF() && !IsWhiteSpace() && !IsPunctuation() && !IsOperator())
        {
            return ScanUnexpectedToken();
        }

        return CreateToken(TokenType.HexaDecimalNumericLiteral);
    }

    private Token ScanOctal()
    {
        if (!IsOctal())
            throw new LexicalException(nameof(ScanOctal), "0c", $"{Current}${Next}");
        
        // Consume both '0' and 'c'
        Consume();
        Consume();

        while (IsOctalDigit())
        {
            Consume();
        }
        
        // After consuming octal digits we expect there to be either whitespace, eof etc
        if (!IsEOF() && !IsWhiteSpace() && !IsPunctuation() && !IsOperator())
        {
            return ScanUnexpectedToken();
        }

        return CreateToken(TokenType.OctaDecimalNumericLiteral);
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
