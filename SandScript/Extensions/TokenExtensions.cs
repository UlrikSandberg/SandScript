using SandScript.Lexer;

namespace SandScript.Extensions
{
    public static class TokenExtensions
    {
        public static bool IsTrivia(this Token token)
        {
            return token.TokenCategory == TokenCategory.Whitespace || token.TokenCategory == TokenCategory.Comment;
        }
        public static TokenCategory AsTokenCategory(this TokenType token)
        {
            switch (token)
            {
                case TokenType.RightArrow:
                case TokenType.LeftArrow:
                case TokenType.FatArrow:
                case TokenType.Colon:
                case TokenType.Semicolon:
                case TokenType.Comma:
                case TokenType.Dot:
                    return TokenCategory.Punctuation;

                case TokenType.Equal:
                case TokenType.NotEqual:
                case TokenType.Not:
                case TokenType.LessThan:
                case TokenType.LessThanOrEqual:
                case TokenType.GreaterThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.Minus:
                case TokenType.MinusEqual:
                case TokenType.MinusMinus:
                case TokenType.Mod:
                case TokenType.ModEqual:
                case TokenType.MulMul:    
                case TokenType.Mul:
                case TokenType.MulEqual:
                case TokenType.Plus:
                case TokenType.PlusEqual:
                case TokenType.PlusPlus:
                case TokenType.Question:
                case TokenType.DoubleQuestion:
                case TokenType.DivideEqual:
                case TokenType.Divide:
                case TokenType.BoolOr:
                case TokenType.BoolAnd:
                case TokenType.BitwiseXorEqual:
                case TokenType.BitwiseXor:
                case TokenType.BitwiseOrEqual:
                case TokenType.BitwiseOr:
                case TokenType.BitwiseAndEqual:
                case TokenType.BitwiseAnd:
                case TokenType.BitShiftLeft:
                case TokenType.BitShiftRight:
                case TokenType.Power:    
                case TokenType.Assignment:
                    return TokenCategory.Operator;

                case TokenType.BlockComment:
                case TokenType.LineComment:
                    return TokenCategory.Comment;

                case TokenType.NewLine:
                case TokenType.WhiteSpace:
                    return TokenCategory.Whitespace;

                case TokenType.LeftCurlyBracket:
                case TokenType.LeftSquareBracket:
                case TokenType.LeftParenthesis:
                case TokenType.RightCurlyBracket:
                case TokenType.RightSquareBracket:
                case TokenType.RightParenthesis:
                    return TokenCategory.Grouping;

                case TokenType.Identifier:
                    return TokenCategory.Identifier;
                
                case TokenType.Keyword:
                    return TokenCategory.Keyword;

                case TokenType.String:
                case TokenType.Integer:
                case TokenType.Float:
                    return TokenCategory.Constant;

                case TokenType.Error:
                    return TokenCategory.Invalid;

                default: 
                    return TokenCategory.Unknown;
            }
        }
    }
}