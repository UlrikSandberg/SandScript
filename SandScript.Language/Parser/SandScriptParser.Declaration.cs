using SandScript.Language.Syntax;
using SandScript.Language.Syntax.Declarations;

namespace SandScript.Language.Parser;

public partial class SandScriptParser
{
    private SyntaxNode ParseVariableDeclaration(bool allowDefault = true)
    {
        string variableType;
        string variableName;
        SyntaxNode value = null;
        
        ParseTypeAndName(out variableType, out variableName, "var");

        if (allowDefault && current == TokenType.Assignment)
        {
            Advance();
            value = ParseExpression();
        }

        return new VariableDeclaration();
    }

    private void ParseTypeAndName(out string type, out string name, params string[] keyWords)
    {
        if (keyWords.Contains(current.Value))
        {
            type = ConsumeToken(TokenType.Keyword).Value;
        }
        else
        {
            type = ConsumeToken(TokenType.Identifier).Value;
        }

        name = ConsumeToken(TokenType.Identifier).Value;
    }
}