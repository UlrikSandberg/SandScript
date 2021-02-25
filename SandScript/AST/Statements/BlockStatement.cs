using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Statements
{
    public class BlockStatement : Statement
    {
        public IEnumerable<SyntaxNode> Contents { get; }
        public override SyntaxType Type { get; } = SyntaxType.BlockStatement;
        
        public BlockStatement(SourceSpan span, IEnumerable<SyntaxNode> contents) : base(span)
        {
            Contents = contents;
        }
    }
}