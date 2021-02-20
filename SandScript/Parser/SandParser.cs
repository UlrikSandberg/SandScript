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
    }
}