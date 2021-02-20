using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SandScript.Extensions;

namespace SandScript.Lexer
{
    public class SandLexer
    {
        private readonly string[] _keywords = { Keywords.LET, Keywords.CLASS, Keywords.FUNCTION, Keywords.TRUE, Keywords.FALSE };
        private readonly LexerErrorSink _errorSink;
        private readonly SandscriptSyntaxConfiguration _syntaxConfiguration;

        // The current line of the sourceFile
        private int _line;
        // The current column of respective line
        private int _column;
        // The current char index of the original source
        private int _index;
        // Token count
        private int _tokenCount;
        
        // Keep track of code
        private SourceCode _sourceCode;
        private SourceLocation _tokenStart;
        private char _ch => _sourceCode[_index];
      
        // Keep track of the current token being read
        private readonly StringBuilder _tokenBuilder;

        public SandLexer(LexerErrorSink errorSink)
        {
            _errorSink = errorSink;
            _syntaxConfiguration = SandscriptSyntaxConfiguration.Default();
            _sourceCode = null;
            _tokenBuilder = new StringBuilder();
        }
        
        public SandLexer(LexerErrorSink errorSink, SandscriptSyntaxConfiguration syntaxConfiguration)
        {
            _errorSink = errorSink;
            _syntaxConfiguration = syntaxConfiguration;
            _sourceCode = null;
            _tokenBuilder = new StringBuilder();
        }

        private char Glance(int ahead)
        {
            return _sourceCode[_index + ahead];
        }

        private void AdvanceToNewLine()
        {
            _line++;
            _column = 0;
        }
        
        // Start tokenization from string
        public IEnumerable<Token> Tokenize(string code)
        {
            return Tokenize(new SourceCode(code));
        }
        
        // Start tokenization from sourceCode
        public IEnumerable<Token> Tokenize(SourceCode code)
        {
            // Clear the sandLexer instance ready for the next sourceCode file
            _sourceCode = code;
            _line = 1;
            _tokenCount = 0;
            _tokenBuilder.Clear();
            _index = 0;
            _column = 0;
            _tokenStart = new SourceLocation(_index, _line, _column);

            return StartLexicalAnalysis();
        }

        // Go to the next char of the sourceFile
        private void Advance()
        {
            _column++;
            _index++;
        }

        private bool IsKeyword()
        {
            return _keywords.Contains(_tokenBuilder.ToString());
        }

        // Append the current char to the current token and move to the next _char
        private void Ingest()
        {
            _tokenBuilder.Append(_ch);
            Advance();
        }
        
        private IEnumerable<Token> StartLexicalAnalysis()
        {
            var tokens = new List<Token>();
            
            // Keep reading chars until we read an EOF character
            while (!_ch.IsEOF())
            {
                tokens.Add(ReadNext());
            }

            // Ensure that the last token is EOF
            if (tokens.Count == 0)
            {
                tokens.Add(InstantiateToken(TokenType.EOF));
            }
            else if (tokens[tokens.Count - 1].Type != TokenType.EOF)
            {
                tokens.Add(InstantiateToken(TokenType.EOF));
            }

            return tokens;
        }

        private Token ReadNext()
        {
            // If we are at the end of the file make sure to return EOF token
            if (_ch.IsEOF())
            {
                return InstantiateToken(TokenType.EOF);
            }
            
            // If we are at a new line we should complete a newLine scan
            if (_ch.IsNewLine())
            {
                return ScanNewLine();
            }
            
            // Check for whitespace characters
            if (_ch.IsWhiteSpace())
            {
                return ScanWhiteSpace();
            }

            // Check if the character is a digit
            if (_ch.IsDigit())
            {
                return ScanInteger();
            }

            // If there is a "." followed by digits we should scan for a float
            if (_ch == '.' && Glance(1).IsDigit())
            {
                return ScanFloat();
            }
            
            // Check if we are dealing with string literals
            if (_ch.IsQuotationMarks())
            {
                return ScanStringLiteral();
            }

            // Check if we are dealing with either a line comment or block comment
            if (_ch == '/' && (Glance(1) == '/' || Glance(1) == '*'))
            {
                return ScanComment();
            }

            if (_ch.IsLetter() || _ch == '_')
            {
                return ScanIdentifierOrKeyword();
            }
            
            // If we did not encounter EOF, Newline, Whitespace, digit, floats, strings, comments or identifiers at this
            // point we should look for all sorts of punctuation and separator characters
            if (_ch.IsPunctuation())
            {
                return ScanPunctuation();
            }
            
            // If no rules has been matched at this point scan for an error
            return ScanError();
        }

        // Type identifier section

        // SCANNING SECTION
        private Token ScanPunctuation()
        {
            return _ch switch
            {
                ';' => InstantiateToken(TokenType.Semicolon, true),
                ':' => InstantiateToken(TokenType.Colon, true),
                '{' => InstantiateToken(TokenType.LeftCurlyBracket, true),
                '}' => InstantiateToken(TokenType.RightCurlyBracket, true),
                '[' => InstantiateToken(TokenType.LeftSquareBracket, true),
                ']' => InstantiateToken(TokenType.RightSquareBracket, true),
                '(' => InstantiateToken(TokenType.LeftParenthesis, true),
                ')' => InstantiateToken(TokenType.RightParenthesis, true),
                '>' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.GreaterThanOrEqual, true),
                        '>' => InstantiateToken(TokenType.BitShiftRight, true),
                        _   => InstantiateToken(TokenType.GreaterThan)
                    };
                }),
                '<' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.LessThanOrEqual, true),
                        '<' => InstantiateToken(TokenType.BitShiftLeft, true),
                        '-' => InstantiateToken(TokenType.LeftArrow, true),
                        _ => InstantiateToken(TokenType.LessThan)
                    };
                }),
                '+' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.PlusEqual, true),
                        '+' => InstantiateToken(TokenType.PlusPlus, true),
                        _   => InstantiateToken(TokenType.Plus)
                    };
                }),
                '-' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.MinusEqual, true),
                        '-' => InstantiateToken(TokenType.MinusMinus, true),
                        '>' => InstantiateToken(TokenType.RightArrow, true),
                        _   => InstantiateToken(TokenType.Minus)
                    };
                }),
                '=' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.Equal, true),
                        '>' => InstantiateToken(TokenType.FatArrow, true),
                        _ => InstantiateToken(TokenType.Assignment)
                    };
                }),
                '!' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.NotEqual, true),
                        _ => InstantiateToken(TokenType.Not)
                    };
                }),
                '*' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.MulEqual, true),
                        '*' => InstantiateToken(TokenType.MulMul, true),
                        _ => InstantiateToken(TokenType.Mul)
                    };
                }),
                '/' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.DivideEqual, true),
                        _ => InstantiateToken(TokenType.Divide)
                    };
                }),
                '.' => InstantiateToken(TokenType.Dot, true),
                ',' => InstantiateToken(TokenType.Comma, true),
                '&' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '&' => InstantiateToken(TokenType.BoolAnd, true),
                        '=' => InstantiateToken(TokenType.BitwiseAndEqual, true),
                        _ => InstantiateToken(TokenType.BitwiseAnd)
                    };
                }) ,
                '|' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '|' => InstantiateToken(TokenType.BoolOr, true),
                        '=' => InstantiateToken(TokenType.BitwiseOrEqual, true),
                        _ => InstantiateToken(TokenType.BitwiseOr)
                    };
                }),
                '%' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.ModEqual, true),
                        _ => InstantiateToken(TokenType.Mod)
                    };
                }),
                '^' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '=' => InstantiateToken(TokenType.BitwiseXorEqual, true),
                        '^' => InstantiateToken(TokenType.BitwiseXor, true),
                        _ => InstantiateToken(TokenType.Power)
                    };
                }),
                '?' => CustomTokenInstantiation(() =>
                {
                    return _ch switch
                    {
                        '?' => InstantiateToken(TokenType.DoubleQuestion, true),
                        _ => InstantiateToken(TokenType.Question)
                    };
                }),
                _ => ScanError()
            };
        }

        private Token CustomTokenInstantiation(Func<Token> func)
        {
            Ingest();
            return func();
        }
        
        private Token ScanIdentifierOrKeyword()
        {
            // To get here we had to encounter either "_" or a letter such that any identifier or keyword start with either one of them
            // ingest all the following characters that are valid for the body of the identifier declaration
            while (_ch.IsIdentifierOrkeyword())
            {
                Ingest();
            }
            
            // At this point the ingested content is a valid identifier under the condition that it was followed by either
            // a whitespace, an EOF or some sort of punctuation
            if (!_ch.IsWhiteSpace() && !_ch.IsEOF() && !_ch.IsPunctuation() && !(_syntaxConfiguration.IsSemicolonOptional && _ch.IsNewLine()))
            {
                return ScanError();
            }
            
            // Determine if the ingested content matches a known keyword
            if (IsKeyword())
            {
                return InstantiateToken(TokenType.Keyword);
            }

            return InstantiateToken(TokenType.Identifier);
        }
        
        private Token ScanComment()
        {
            // We only get this far if we encountered "/" followed by another "/" or "*" start by ingesting the first "/"
            Ingest();
            
            // The next character is a star and thus we started block comment
            if (_ch == '*')
            {
                return ScanBlockComment();
            }
            
            // If not * then there had to be a second "/" for the comment which we also need to ingest
            Ingest();
            
            // Consume the rest of the line as a comment that is until NewLine or EOF is reached
            while (!_ch.IsNewLine() && !_ch.IsEOF())
            {
                Ingest();
            }

            return InstantiateToken(TokenType.LineComment);
        }

        private Token ScanBlockComment()
        {
            // We can only reach this part if we ingested "/" and saw a "*" ingest the "*" and move on
            Ingest();
            
            bool IsEndOfComment() => _ch == '*' && Glance(1) == '/';
            while (!IsEndOfComment())
            {
                // If we ingested until EOF without finding the closing comment there is an error
                if (_ch.IsEOF())
                {
                    return InstantiateToken(TokenType.Error);
                }

                // If we encounter newline character we should advance line number and reset column index
                if (_ch.IsNewLine())
                {
                    AdvanceToNewLine();
                }
                
                // Else ingest all characters until we meet end comment pattern
                Ingest();
            }
            
            // When we reach the end of comment we need to also ingest the trailing "*/"
            Ingest(); // --> Ingesting "*"
            Ingest(); // --> Ingesting "/"
            return InstantiateToken(TokenType.BlockComment);
        }
        
        private Token ScanNewLine()
        {
            // Ingest the newline char
            Ingest();

            // Increment the line number and set the line column to zero
            AdvanceToNewLine();
            
            // Return the End of line token
            return InstantiateToken(TokenType.NewLine);
        }

        private Token ScanWhiteSpace()
        {
            // Keep ingesting characters as long as whitespace is true
            while (_ch.IsWhiteSpace())
            {
                Ingest();
            }
            
            // return the scanned whiteSpace token
            return InstantiateToken(TokenType.WhiteSpace);
        }

        private Token ScanStringLiteral()
        {
            // We only get this far based on either " or ' so ingest those characters and ingest until we meet those characters again
            Ingest();

            while (!_ch.IsQuotationMarks())
            {
                if (_ch.IsEOF())
                {
                    ScanError(Severity.Fatal, "Unexpected end of file");
                }
                Ingest();
            }
            
            // We reached the end quotation mark and should remember to include that as well
            Ingest();
            return InstantiateToken(TokenType.String);
        }

        private Token ScanInteger()
        {
            while (_ch.IsDigit())
            {
                Ingest();
            }

            if (_ch == '.')
            {
                return ScanFloat();
            }
            
            // At this point we have a valid integer we would expect either: whitespace, EOF or punctuation, if semicolon
            // is optional EOL is also and option. --> Else if that is not the case report error
            if (!_ch.IsWhiteSpace() && !_ch.IsEOF() && !_ch.IsPunctuation() && !(_syntaxConfiguration.IsSemicolonOptional && _ch.IsNewLine()))
            {
                return ScanError();
            }
            
            return InstantiateToken(TokenType.Integer);
        }

        private Token ScanFloat()
        {
            // As we are only able to get to Scan float on a "." we ingest the . and continue afterwards
            if (_ch == '.')
            {
                Ingest();
            }

            while (_ch.IsDigit())
            {
                Ingest();
            }
            
            // Make sure that we scanned digits after the "."
            if (Glance(-1) == '.')
            {
                return ScanError(message: "Must contain digits after '.'");
            }
            
            // At this point we have valid float literal, we would suspect either whitespace, EOF, EOL or punctuation
            // if we did not come across these make an error.
            if (!_ch.IsWhiteSpace() && !_ch.IsEOF() && !_ch.IsPunctuation() && !(_syntaxConfiguration.IsSemicolonOptional && _ch.IsNewLine()))
            {
                return _ch.IsLetter() ? ScanError(message: "'{0}' is an invalid float value") : ScanError();
            }

            return InstantiateToken(TokenType.Float);
        }

        private Token ScanError(Severity severity = Severity.Fatal, string message = "Unexpected token '{0}'")
        {
            while (!_ch.IsWhiteSpace() && !_ch.IsEOF())
            {
                Ingest();    
            }
            AddError(message, severity);
            return InstantiateToken(TokenType.Error);
        }
        
        private void AddError(string message, Severity severity)
        {
            var span = new SourceSpan(_tokenStart, new SourceLocation(_index, _line, _column));
            _errorSink.AddError(string.Format(message, _tokenBuilder), _sourceCode, severity, span);
        }

        // Creates and returns token of specified type
        private Token InstantiateToken(TokenType type, bool ingest = false)
        {
            if (ingest)
            {
                Ingest();
            }
            
            var end = new SourceLocation(_index, _line, _column);
            var start = _tokenStart;

            _tokenStart = end;
            
            var tokenSymbol = _tokenBuilder.ToString();
            _tokenCount++;
            _tokenBuilder.Clear();
            
            // create a token from current information.
            return new Token(_tokenCount, type, tokenSymbol, new SourceSpan(start, end));
        }
    }
}