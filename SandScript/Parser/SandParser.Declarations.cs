using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using SandScript.AST;
using SandScript.AST.Declarations;
using SandScript.Lexer;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private SourceRoot ParseRoot()
        {
            var syntaxTree = new List<SyntaxNode>();
            var start = _current.CodeSpan.Start;

            while (_current == Keywords.CLASS || _current == Keywords.FUNCTION)
            {
                // Parse class declarations
                if (_current == Keywords.CLASS)
                {
                    syntaxTree.Add(ParseClassDeclaration());
                }

                // Parse function declarations
                if (_current == Keywords.FUNCTION)
                {
                    
                }
            }

            if (_syntaxConfiguration.IsRootLevelStatementsAllowed)
            {
                var statements = new List<SyntaxNode>();
                var statementsStart = _current.CodeSpan.Start;
    
                while (_current != TokenType.EOF)
                {
                    // Parse statements
                }
                
                // wrap the statements in anonumous block
            }
            
            
            return null;
        }

        private ClassDeclaration ParseClassDeclaration()
        {
            // Create four list to hold all information relevant to a class
            var constructors = new List<ConstructorDeclaration>();
            var properties = new List<PropertyDeclaration>();
            var methods = new List<MethodDeclaration>();
            var fields = new List<FieldDeclaration>();

            // Ingest the keyword token class and the preceding EXPECTED identifier.
            var classKeyword = IngestKeyword(Keywords.CLASS);
            var className= IngestIdentifier().Symbol;
            
            MakeBlock(() =>
            {
                var classMember = ParseClassMember();
                switch (classMember.Type)
                {
                    case SyntaxType.PropertyDeclaration:
                        properties.Add(classMember as PropertyDeclaration);
                        break;
                    case SyntaxType.FieldDeclaration:
                        fields.Add(classMember as FieldDeclaration);
                        break;
                    case SyntaxType.MethodDeclaration:
                        methods.Add(classMember as MethodDeclaration);
                        break;
                    case SyntaxType.ConstructorDeclaration:
                        constructors.Add(classMember as ConstructorDeclaration);
                        break;
                }
            });
            return new ClassDeclaration(CreateSpan(classKeyword), className, constructors, fields, methods, properties);
        }

        private SyntaxNode ParseClassMember()
        {
            return _current.Symbol switch
            {
                Keywords.LET => ParsePropertyDeclaration(),
                Keywords.FUNCTION => ParseMethodDeclaration(),
                Keywords.CONSTRUCTOR => ParseConstructorDeclaration(),
                _ => ParseFieldDeclaration()
            };
        }

        // Parse a constructor declaration
        private ConstructorDeclaration ParseConstructorDeclaration()
        {
            // Start by ingesting the constructor keyword followed by the constructor identifier
            var constructorKeywordToken = IngestKeyword(Keywords.CONSTRUCTOR);
            var constructorIdentifier = IngestIdentifier().Symbol;
            
            // Parse the parameter list and parse constructor scope
            var parameters = ParseParameterList();
            var body = ParseScope();

            return new ConstructorDeclaration(CreateSpan(constructorKeywordToken), constructorIdentifier, parameters, body);
        }

        private FieldDeclaration ParseFieldDeclaration()
        {
            
        }

        private MethodDeclaration ParseMethodDeclaration()
        {
            
        }

        private PropertyDeclaration ParsePropertyDeclaration()
        {
            
        }
        
        private VariableDeclaration ParseVariableDeclaration()
        {
            
        }

        private ParameterDeclaration ParseParameterDeclaration()
        {
            // each parameter is at least an identifier of type object 
            var parameterIdentifier = IngestIdentifier();
            var parameterType = "object";

            // If a colon exists after the identifier we read the type
            if (_current == TokenType.Colon)
            {
                Ingest();
                parameterType = IngestIdentifier().Symbol;
            }
            
            return new ParameterDeclaration(CreateSpan(parameterIdentifier), parameterIdentifier.Symbol, parameterType);
        }

        private IEnumerable<ParameterDeclaration> ParseParameterList()
        {
            var parameters = new List<ParameterDeclaration>();
            MakeBlock(() =>
            {
                // If the first token we encounter is the closing parenthesis then return right away
                // as we are done with the parameterList
                if (_current == TokenType.RightParenthesis)
                {
                    return;
                }
                
                // Else parse the next parameter
                parameters.Add(ParseParameterDeclaration());
                
            }, TokenType.LeftParenthesis, TokenType.RightParenthesis);
            return parameters;
        }
    }
}