using System;
using System.Security.Policy;
using SandScript.Lexer;
using SandScript.Parser;


namespace SandScript
{
    public class VirtualCompiler
    {
        private readonly string _code;
        private readonly SandLexer _lexer;
        private readonly SandParser _parser;
        
        public VirtualCompiler(string code, SandscriptSyntaxConfiguration syntaxConfiguration)
        {
            _code = code;
            var errorSink = new LexerErrorSink();
            _lexer = new SandLexer(errorSink, syntaxConfiguration);
            _parser = new SandParser(errorSink, syntaxConfiguration);
        }

        public VirtualProgram Compile()
        {
            // Tokenize the code
            var sourceCode = new SourceCode(_code);
            var tokens = _lexer.Tokenize(sourceCode);
            var ast = _parser.ParseFile(sourceCode, tokens);
            
            // return the ast as a virtual program... Attach additional properties to the virtual program to access fx.
            // unity objects from the code

            return new VirtualProgram();
        }
    }
}