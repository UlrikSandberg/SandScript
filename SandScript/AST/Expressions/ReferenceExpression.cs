using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class ReferenceExpression : Expression
    {
        public override SyntaxType Type { get; } = SyntaxType.ReferenceExpression;
        public IEnumerable<Expression> References { get; }
        
        public ReferenceExpression(SourceSpan span, IEnumerable<Expression> references) : base(span)
        {
            References = references;
        }
    }
}