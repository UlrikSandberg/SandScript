using System.Collections.Generic;
using SandScript.AST;
using SandScript.Lexer;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private SourceRoot ParseRoot()
        {
            var syntaxTree = new List<SyntaxNode>();
            var start = _current.CodeSpan.Start;

            while (_current == Keywords.CLASS || _current == Keywords.FUNCTION)
            {
                // Parse class declarations
                
                // Parse function declarations
            }

            if (_syntaxConfiguration.IsRootLevelStatementsAllowed)
            {
                var statements = new List<SyntaxNode>();
                var statementsStart = _current.CodeSpan.Start;
    
                while (_current != TokenType.EOF)
                {
                    // Parse statements
                }
                
                // wrap the statements in anonumous block
            }
            
            
            return null;
        }
    }
}