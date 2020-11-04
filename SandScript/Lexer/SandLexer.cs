using System;
using System.Collections.Generic;
using System.Text;

namespace SandScript.Lexer
{
    public class SandLexer
    {
        private readonly LexerErrorSink _errorSink;
        
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
        private char _ch => _sourceCode[_index];
      
        // Keep track of the current token being read
        private readonly StringBuilder _tokenBuilder;

        public SandLexer(LexerErrorSink errorSink)
        {
            _errorSink = errorSink;
            _sourceCode = null;
            _tokenBuilder = new StringBuilder();
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

            return StartLexicalAnalysis();
        }

        // Go to the next char of the sourceFile
        private void Advance()
        {
            _column++;
            _index++;
        }

        // append the current char to the current token and move to the next _char
        private void Ingest()
        {
            _tokenBuilder.Append(_ch);
       
            Advance();
        }
        
        private IEnumerable<Token> StartLexicalAnalysis()
        {
            var tokens = new List<Token>();
            
            while (!IsEOF())
            {
                tokens.Add(ReadNext());
            }

            // Ensure that the last token is EOF
            if (tokens.Count == 0)
            {
                tokens.Add(InstantiateToken(TokenType.EOF));
            }
            else if(tokens[tokens.Count - 1].Type != TokenType.EOF)
            {
                tokens.Add(InstantiateToken(TokenType.EOF));
            }

            return tokens;
        }

        private Token ReadNext()
        {
            // If we are at the end of the file make sure to return EOF token
            if (IsEOF())
            {
                return InstantiateToken(TokenType.EOF);
            }
            
            // Check if we are at a newline
            if (IsNewLine())
            {
                return ScanNewLine();
            }
            
            // Check for whitespace characters
            if (IsWhiteSpace())
            {
                return ScanWhiteSpace();
            }
            
            // Check for digit
            
            // Check for punctuation .45f
            
            // Check for comment
            
            // Check for letter
            
            // check string literal " ", ' '
            
            // check separators ; , : ( ) [ ] { } . ? 
            
            // operators + - / * = % && || < > 
            
            
            // Else scan error token...

            return InstantiateToken(TokenType.EOF);
        }

        // Type identifier section
        private bool IsEOF()
        {
            return _ch == '\0';
        }

        private bool IsNewLine()
        {
            return _ch == '\n';
        }

        private bool IsWhiteSpace()
        {
            // Accept EOF as whiteSpace but not '\n'
            return (char.IsWhiteSpace(_ch) || IsEOF()) && !IsNewLine();
        }
        
        // SCANNING SECTION
        private Token ScanNewLine()
        {
            // ingest the new line char
            Ingest();

            // Increment the number of line and set the line column to zero
            _line++;
            _column = 0;
            
            // Return the End of line toke 
            return InstantiateToken(TokenType.EOL);
        }

        private Token ScanWhiteSpace()
        {
            // Keep ingesting characters as long as whitespace is true
            while (IsWhiteSpace())
            {
                Ingest();
            }
            
            // return the scanned whiteSpace token
            return InstantiateToken(TokenType.WhiteSpace);
        }
        

        // Creates and returns token of specified type
        private Token InstantiateToken(TokenType type)
        {
            _tokenCount++;
            var tokenSymbol = _tokenBuilder.ToString();
            _tokenBuilder.Clear();
            
            // create a token from current information.
            return new Token(_tokenCount, type, tokenSymbol, _line, _column);
        }

        // Scan unknown error token
        private void ScanErrorToken()
        {
            
        }

        // add contextual error to the errorSink.
        private void AddError(string msg)
        {
            
        }
    }
}

/*
private readonly string[] _keywords =
        {
            "class",
            "interface",
            "if",
            "else",
            "function",
            "true",
            "false",
            "for",
            "while",
            "foreach",
            "break",
            "void",
            "bool",
            "string",
            "let",
            "switch",
            "case",
            "do",
            "null"
        };
        
        // Legal characters in SandScript
        private readonly string _legalCharacters = "abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ 0123456789 ,.;:-_*!#%&/()[]{}=?+";
        
        // Legal identifier characters
        private readonly string _legalIdentiferCharacters = "";
        
        // RegEx for number analysis
        private string RegExNumber = $"[0-9]*\\.?[0-9]+";

        // RegEx for identifer analysis
        private string RegExIdentifier = "";
        
        */