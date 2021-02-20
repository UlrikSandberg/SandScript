using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST
{
    public class SourceRoot : SyntaxNode
    {
        public SourceCode SourceCode { get; }
        public IEnumerable<SyntaxNode> Children { get; }
        
        public override SyntaxCategory Category { get; } = SyntaxCategory.Root;
        public override SyntaxType Type { get; } = SyntaxType.SourceRoot;

        public SourceRoot(SourceSpan span, SourceCode sourceCode, IEnumerable<SyntaxNode> children) : base(span)
        {
            SourceCode = sourceCode;
            Children = children;
        }
    }
}