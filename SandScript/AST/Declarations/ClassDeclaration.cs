using System.Collections;
using System.Collections.Generic;
using SandScript.Lexer;

namespace SandScript.AST.Declarations
{
    public class ClassDeclaration : Declaration
    {
        public IEnumerable<ConstructorDeclaration> Constructors { get; }
        public IEnumerable<FieldDeclaration> Fields { get; }
        public IEnumerable<MethodDeclaration> Methods { get; }
        public IEnumerable<PropertyDeclaration> Properties { get; }

        public override SyntaxType Type { get; } = SyntaxType.ClassDeclaration;
        
        public ClassDeclaration(SourceSpan span, string name, 
            IEnumerable<ConstructorDeclaration> constructors,
            IEnumerable<FieldDeclaration> fields,
            IEnumerable<MethodDeclaration> methods,
            IEnumerable<PropertyDeclaration> properties) : base(span, name)
        {
            Constructors = constructors;
            Fields = fields;
            Methods = methods;
            Properties = properties;
        }
    }
}