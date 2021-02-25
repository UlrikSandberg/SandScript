using System;
using System.Collections.Generic;
using System.Linq;
using SandScript.AST;
using SandScript.Extensions;
using SandScript.Lexer;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private readonly LexerErrorSink _errorSink;
        private readonly SandscriptSyntaxConfiguration _syntaxConfiguration;

        private SourceCode _sourceCode;
        private IEnumerable<Token> _tokens;
        private int _index;
        private bool _error;

        private Token _current => _tokens.ElementAtOrDefault(_index) ?? _tokens.Last();
        private Token _last => Glance(-1);
        private Token _next => Glance(1);

        public SandParser() : this(new LexerErrorSink(), SandscriptSyntaxConfiguration.Default()) {}
        
        public SandParser(LexerErrorSink errorSink, SandscriptSyntaxConfiguration syntaxConfiguration)
        {
            _errorSink = errorSink;
            _syntaxConfiguration = syntaxConfiguration;
        }

        public SourceRoot ParseFile(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            InitializeParser(sourceCode, tokens);
            try
            {
                return ParseRoot();
            }
            catch (SyntaxException ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private void InitializeParser(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            _sourceCode = sourceCode;
            _tokens = tokens.Where(t => !t.IsTrivia()).ToArray();
            _index = 0;
        }
        
        private Token Glance(int ahead)
        {
            return _tokens.ElementAtOrDefault(_index + ahead) ?? _tokens.Last();
        }
        
        private void Advance()
        {
            _index++;
        }

        // A block of code is scoped code often residing inside "{" and "}", Make block statement
        private void MakeBlock(Action action, TokenType blockStartType = TokenType.LeftCurlyBracket,
            TokenType blockCloseType = TokenType.RightCurlyBracket)
        {
            // Start of by ingesting the next token in the line under the expectation that it is of blockStartType
            IngestByType(blockStartType);
            MakeStatement(action, blockCloseType);
        }

        // Construct a statement that is ingest all tokens until we meet the specified closingType, use
        // passed action to construct the statement
        private void MakeStatement(Action action, TokenType closingType = TokenType.Semicolon)
        {
            try
            {
                // Until we meet the specified closingType or the end of the file call the action to keep processing
                // the current statement
                while (_current != closingType && _current != TokenType.EOF)
                {
                    action();
                }
            }
            catch (SyntaxException ex)
            {
                Console.WriteLine(ex);
                while (_current != closingType && _current != TokenType.EOF)
                {
                    Ingest();
                }
            }
            finally
            {
                if (_error)
                {
                    if (_last == closingType)
                    {
                        _index--;
                    }

                    if (_current != closingType)
                    {
                        while (_current != closingType && _current != TokenType.EOF)
                        {
                            Ingest();
                        }
                    }

                    _error = false;
                }

                if (closingType == TokenType.Semicolon)
                {
                    IngestSemicolon();
                }
                else
                {
                    IngestByType(closingType);
                }
            }
        }

        private Token Ingest()
        {
            var token = _current;
            Advance();
            return token;
        }
        
        private Token IngestByType(TokenType type)
        {
            if (_current != type)
            {
                throw UnexpectedToken(type);
            }

            return Ingest();
        }

        // Ingest the next token under the expectation that the token has to be a keyword
        private Token IngestKeyword(string keyword)
        {
            // Make sure the current token is of type keyword and the keyword match
            if (_current != TokenType.Keyword && _current != keyword)
            {
                throw UnexpectedToken(keyword);
            }
            return Ingest();
        }

        private Token IngestIdentifier()
        {
            return IngestByType(TokenType.Identifier);
        }

        private Token IngestSemicolon()
        {
            if (!_syntaxConfiguration.IsSemicolonOptional || _current == TokenType.Semicolon)
            {
                return IngestByType(TokenType.Semicolon);
            }

            return _current;
        }

        private SyntaxException UnexpectedToken(TokenType type)
        {
            return UnexpectedToken(type.ToString());
        }

        private SyntaxException UnexpectedToken(string expected)
        {
            Advance();
            var value = string.IsNullOrEmpty(_last?.Symbol) ? _last?.Type.ToString() : _last?.Symbol;
            var message = $"Unexpected '{value}'. Expected '{expected}'";
            return SyntaxError(Severity.Error, message, _last?.CodeSpan);
        }

        private SyntaxException SyntaxError(Severity severity, string message, SourceSpan span = null)
        {
            _error = true;
            AddError(severity, message, span);
            return new SyntaxException(message);
        }

        private void AddError(Severity severity, string message, SourceSpan span = null)
        {
            _errorSink.AddError(message, _sourceCode, severity, span ?? CreateSpan(_current));
        }
        
        private SourceSpan CreateSpan(SourceLocation start, SourceLocation end)
        {
            return new SourceSpan(start, end);
        }

        private SourceSpan CreateSpan(Token start)
        {
            return CreateSpan(start.CodeSpan.Start, _current.CodeSpan.End);
        }

        private SourceSpan CreateSpan(SyntaxNode start)
        {
            return CreateSpan(start.Span.Start, _current.CodeSpan.End);
        }

        private SourceSpan CreateSpan(SourceLocation start)
        {
            return CreateSpan(start, _current.CodeSpan.End);
        }
    }
}