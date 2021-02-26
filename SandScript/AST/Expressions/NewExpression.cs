using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class NewExpression : Expression
    {
        public override SyntaxType Type { get; } = SyntaxType.NewExpression;
        public Expression Reference { get; }
        public IEnumerable<Expression> Arguments { get; }

        public NewExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments) : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}