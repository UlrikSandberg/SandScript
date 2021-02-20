using System;
using System.Security.Policy;
using SandScript.Lexer;


namespace SandScript
{
    public class VirtualCompiler
    {
        private readonly string _code;
        private readonly SandLexer _lexer;
        private readonly LexerErrorSink _errorSink;
        
        public VirtualCompiler(string code, SandscriptSyntaxConfiguration syntaxConfiguration)
        {
            _code = code;
            _errorSink = new LexerErrorSink();
            _lexer = new SandLexer(_errorSink, syntaxConfiguration);
        }

        public VirtualProgram Compile()
        {
            // Tokenize the code
            var tokens = _lexer.Tokenize(_code);
            foreach (var token in tokens)
            {
                Console.WriteLine(token.ToString());
            }
            
            // Print out the tokens of this code
            
            // parse the tokens and return an AST
            
            
            return new VirtualProgram();
        }
    }
}