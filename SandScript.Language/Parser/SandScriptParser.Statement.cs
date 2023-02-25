using SandScript.Language.Syntax;

namespace SandScript.Language.Parser;

public partial class SandScriptParser
{
    private SyntaxNode ParseStatement()
    {
        SyntaxNode node = null;
        
        // Start by going through all the different keywords
        if (current == TokenType.Keyword)
        {
            switch (current.Value)
            {
                case "var":
                    node = ParseVariableDeclaration();
                    break;
            }
        }
        
        return node;
    }
}