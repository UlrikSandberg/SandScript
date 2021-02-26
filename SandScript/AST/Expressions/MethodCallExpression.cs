using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class MethodCallExpression : Expression
    {
        public override SyntaxType Type { get; } = SyntaxType.MethodCallExpression;
        public Expression Reference { get; }
        public IEnumerable<Expression> Arguments { get; }

        public MethodCallExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments) : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}