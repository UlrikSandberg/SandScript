using System.Collections;
using System.Collections.Generic;
using SandScript.AST.Declarations;
using SandScript.AST.Statements;
using SandScript.Lexer;

namespace SandScript.AST.Expressions
{
    public class LambdaExpression : Expression
    {
        public override SyntaxType Type { get; }
        public IEnumerable<ParameterDeclaration> Parameters { get; }
        public BlockStatement Body { get; }

        public LambdaExpression(SourceSpan span, IEnumerable<ParameterDeclaration> parameters, BlockStatement body) : base(span)
        {
            Parameters = parameters;
            Body = body;
        }
    }
}