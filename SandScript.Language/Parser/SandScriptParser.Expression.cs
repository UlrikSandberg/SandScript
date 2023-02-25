using SandScript.Language.Syntax.Expressions;

namespace SandScript.Language.Parser;

public partial class SandScriptParser
{
    // Expressions are parsed according to the following precendence and associativity table
    // https://www.tutorialspoint.com/csharp/csharp_operators_precedence.htm
    
    // We start by supporting looking for + - / * and literals.
    
    private Expression ParseExpression()
    {
        return ParseAdditivePrecedence();
    }

    private Expression ParseAdditivePrecedence()
    {
        var left = ParseMultiplicativePrecedence();
        
        while (current.TokenType is TokenType.Addition or TokenType.Subtraction)
        {
            var op = Advance(ParseArithmeticOperator);
            left = new ArithmeticExpression(left, ParseMultiplicativePrecedence(), op);
        }

        return left;
    }
    
    private Expression ParseMultiplicativePrecedence()
    {
        var left = ParseTerminalExpressions();

        while (current.TokenType is TokenType.Multiply or TokenType.Divide or TokenType.Modulo)
        {
            var op = Advance(ParseArithmeticOperator);
            left = new ArithmeticExpression(left, ParseUnaryPrecedence(), op);
        }

        return left;
    }

    private Expression ParseUnaryPrecedence()
    {
        return ParsePostfixPrecedence();
    }

    private Expression ParsePostfixPrecedence()
    {
        if (current.TokenType == TokenType.OpenParenthesis)
        {
            
        }
        
        return ParseTerminalExpressions();
    }
    
    /**
     * Bottom level expressions
     */
    private Expression ParseTerminalExpressions()
    {
        if (current.TokenCategory == TokenCategory.Literal)
            return ParseLiteral();

        if (current.TokenCategory == TokenCategory.Identifier)
        {
            
        }

        if (current.TokenType == TokenType.Keyword)
        {
            
        }

        if (current.TokenCategory == TokenCategory.Arithmetic)
        {
            
        }
        
        throw UnexpectedToken("expression terminal");
    }

    private ArithmeticOperation ParseArithmeticOperator() => current.TokenType switch
    {
        TokenType.Addition => ArithmeticOperation.Add,
        TokenType.Subtraction => ArithmeticOperation.Subtract,
        TokenType.Multiply => ArithmeticOperation.Multiply,
        TokenType.Divide => ArithmeticOperation.Divide,
        TokenType.Modulo => ArithmeticOperation.Modulo,
        _ => throw UnexpectedToken("Unexpected mathematical operator")
    };

    private Expression ParseLiteral() => current.TokenType switch
    {
        TokenType.StringLiteral => new LiteralExpression(ConsumeToken(TokenType.StringLiteral).Value,
            LiteralType.String),
        TokenType.IntegerLiteral =>
            new LiteralExpression(ConsumeToken(TokenType.IntegerLiteral).Value, LiteralType.Int),
        TokenType.BooleanLiteral => new LiteralExpression(ConsumeToken(TokenType.BooleanLiteral).Value,
            LiteralType.Boolean),
        TokenType.HexaDecimalNumericLiteral => new LiteralExpression(
            ConsumeToken(TokenType.HexaDecimalNumericLiteral).Value.HexStringToNumberString(), LiteralType.Int),
        TokenType.OctaDecimalNumericLiteral => new LiteralExpression(
            ConsumeToken(TokenType.OctaDecimalNumericLiteral).Value.OctalStringToNumberString(), LiteralType.Int),
        TokenType.FloatingPointLiteral => new LiteralExpression(
            ConsumeToken(TokenType.FloatingPointLiteral).Value, LiteralType.Float),
        _ => throw UnexpectedToken("Unexpected literal token"),
    };
}