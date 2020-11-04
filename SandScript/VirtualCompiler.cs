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
        
        public VirtualCompiler(string code)
        {
            _code = code;
            _errorSink = new LexerErrorSink();
            _lexer = new SandLexer(_errorSink);
        }

        public VirtualProgram Compile()
        {
            // Tokenize the code
            var tokens = _lexer.Tokenize(_code);
            Console.WriteLine(tokens);
            
            // Print out the tokens of this code
            
            // parse the tokens and return an AST
            
            
            return new VirtualProgram();
        }
    }
}