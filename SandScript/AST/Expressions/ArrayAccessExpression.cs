using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class ArrayAccessExpression : Expression
    {
        public override SyntaxType Type { get; } = SyntaxType.ArrayAccessExpression;
        public Expression Reference { get; }
        public IEnumerable<Expression> Arguments { get; }
        
        public ArrayAccessExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments) : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}