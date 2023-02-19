namespace SandScript.Language;

public class Token
{
    public string Value { get; }
    public TokenCategory TokenCategory => GetCategory(TokenType);
    public TokenType TokenType { get; }
    public SourceCode SourceCode { get; }
    public SourceSpan SourceSpan { get; }

    public Token(string value, TokenType type, SourceCode sourceCode, SourceLocation start, SourceLocation end)
    {
        Value = value;
        TokenType = type;
        SourceCode = sourceCode;
        SourceSpan = new SourceSpan(sourceCode, start, end);
    }


    public TokenCategory GetCategory(TokenType type)
    {
        switch (type)
        {
            case TokenType.BOF:
            case TokenType.EOF:
            case TokenType.Invalid:
            case TokenType.Error:
                return TokenCategory.Global;
            
            case TokenType.BlockComment:
            case TokenType.LineComment:
            case TokenType.Whitespace:
            case TokenType.NewLine:
                return TokenCategory.Trivia;
            
            case TokenType.StringLiteral:
            case TokenType.BooleanLiteral:
            case TokenType.IntegerLiteral:
            case TokenType.HexaDecimalNumericLiteral:
            case TokenType.OctaDecimalNumericLiteral:
            case TokenType.FloatingPointLiteral:
                return TokenCategory.Literal;
            
            case TokenType.Identifier:
            case TokenType.Keyword:
                return TokenCategory.Identifier;
            
            case TokenType.Dot:
            case TokenType.Comma:
            case TokenType.SemiColon:
            case TokenType.Colon:
            case TokenType.FatArrow:
            case TokenType.OpenParenthesis:
            case TokenType.CloseParenthesis:
            case TokenType.OpenSquareBracket:
            case TokenType.CloseSquareBracket:
            case TokenType.OpenCurlyBracket:
            case TokenType.CloseCurlyBracket:
                return TokenCategory.Punctuation;
            
            case TokenType.Addition:
            case TokenType.Subtraction:
            case TokenType.Divide:
            case TokenType.Multiply:
            case TokenType.PlusPlus:
            case TokenType.MinusMinus:
            case TokenType.MultMult:
                return TokenCategory.Arithmetic;
            
            case TokenType.Assignment:
            case TokenType.AssignmentAddition:
            case TokenType.AssignmentSubtraction:
            case TokenType.AssignmentMultiply:
            case TokenType.AssignmentDivide:
                return TokenCategory.Assignment;
            
            case TokenType.LogicalLess:
            case TokenType.LogicalLessOrEqual:
            case TokenType.LogicalGreater:
            case TokenType.LogicalGreaterOrEqual:
            case TokenType.LogicalEqual:
            case TokenType.LogicalNotEqual:
            case TokenType.LogicalAnd:
            case TokenType.LogicalOr:
            case TokenType.LogicalNot:
                return TokenCategory.Logical;
            
            case TokenType.BitwiseAnd:
            case TokenType.BitwiseOr:
            case TokenType.BitwiseXor:
                return TokenCategory.Bitwise;

            default:
                return TokenCategory.None;
        }
    }
    
    public bool IsInvalid()
    {
        return TokenType == TokenType.Invalid;
    }

    public bool IsTrivia()
    {
        return TokenCategory == TokenCategory.Trivia;
    }
}