using System;
using System.Collections.Generic;
using SandScript.AST;
using SandScript.AST.Statements;
using SandScript.Lexer;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private BlockStatement ParseScope()
        {
            // Gather a list of syntaxNodes for the current block of code
            var contents = new List<SyntaxNode>();
            // Get a pointer to the first token of the block
            var start = _current;

            // Consume '{' and continue to parse statements until we reach '}'
            MakeBlock(() => { contents.Add(ParseStatement()); });
            return new BlockStatement(CreateSpan(start), contents);
        }

        private SyntaxNode ParseStatement()
        {
            SyntaxNode value = null;
            if (_current == TokenType.Keyword)
            {
                value = _current.Symbol switch
                {
                    Keywords.TRUE => ParseExpression(),
                    Keywords.FALSE => ParseExpression(),
                    Keywords.IF => ParseIfStatement(),
                    Keywords.DO => ParseDoWhileStatement(),
                    Keywords.WHILE => ParseWhileStatement(),
                    Keywords.FOR => ParseForStatement(),
                    Keywords.SWITCH => ParseSwitchStatement(),
                    Keywords.RETURN => ParseReturnStatement(),
                    Keywords.LET => ParseVariableDeclaration(),
                    _ => throw UnexpectedToken("Found an unexpected token when parsing statements")
                };
            } 
            else if (_current == TokenType.Semicolon)
            {
                    
            }
            else
            {
                
            }

            return value;
        }

        private SyntaxNode ParseIfStatement()
        {
            return null;
        }

        private SyntaxNode ParseDoWhileStatement()
        {
            return null;
        }

        private SyntaxNode ParseWhileStatement()
        {
            return null;
        }

        private SyntaxNode ParseForStatement()
        {
            return null;
        }

        private SyntaxNode ParseSwitchStatement()
        {
            return null;
        }

        private SyntaxNode ParseReturnStatement()
        {
            return null;
        }
    }
}