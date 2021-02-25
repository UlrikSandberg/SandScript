using System.Collections;
using System.Collections.Generic;
using SandScript.AST.Statements;
using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class ConstructorDeclaration : Declaration
    {
        public IEnumerable Parameters { get; }
        public BlockStatement Body { get; }
        public override SyntaxType Type { get; } = SyntaxType.ConstructorDeclaration;
        
        public ConstructorDeclaration(SourceSpan span, string name, IEnumerable<ParameterDeclaration> parameters, BlockStatement body) : base(span, name)
        {
            Parameters = parameters;
            Body = body;
        }
    }
}