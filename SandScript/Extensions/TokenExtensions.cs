using SandScript.Lexer;

namespace SandScript.Extensions
{
    public static class TokenExtensions
    {
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

                /*case TokenKind.Equal:
                case TokenKind.NotEqual:
                case TokenKind.Not:
                case TokenKind.LessThan:
                case TokenKind.LessThanOrEqual:
                case TokenKind.GreaterThan:
                case TokenKind.GreaterThanOrEqual:
                case TokenKind.Minus:
                case TokenKind.MinusEqual:
                case TokenKind.MinusMinus:
                case TokenKind.Mod:
                case TokenKind.ModEqual:
                case TokenKind.Mul:
                case TokenKind.MulEqual:
                case TokenKind.Plus:
                case TokenKind.PlusEqual:
                case TokenKind.PlusPlus:
                case TokenKind.Question:
                case TokenKind.DoubleQuestion:
                case TokenKind.DivEqual:
                case TokenKind.Div:
                case TokenKind.BooleanOr:
                case TokenKind.BooleanAnd:
                case TokenKind.BitwiseXorEqual:
                case TokenKind.BitwiseXor:
                case TokenKind.BitwiseOrEqual:
                case TokenKind.BitwiseOr:
                case TokenKind.BitwiseAndEqual:
                case TokenKind.BitwiseAnd:
                case TokenKind.BitShiftLeft:
                case TokenKind.BitShiftRight:
                case TokenKind.Assignment:
                    return TokenCategory.Operator;*/

                /*case TokenKind.BlockComment:
                case TokenKind.LineComment:
                    return TokenCategory.Comment;*/

                /*case TokenKind.NewLine:
                case TokenKind.WhiteSpace:
                    return TokenCategory.WhiteSpace;*/

                /*case TokenKind.LeftBrace:
                case TokenKind.LeftBracket:
                case TokenKind.LeftParenthesis:
                case TokenKind.RightBrace:
                case TokenKind.RightBracket:
                case TokenKind.RightParenthesis:
                    return TokenCategory.Grouping;*/

                /*case TokenKind.Identifier:
                    return TokenCategory.Identifier;*/
                
                /*case TokenKind.Keyword:
                    return TokenCategory.Keyword;*/

                /*case TokenKind.StringLiteral:
                case TokenKind.IntegerLiteral:
                case TokenKind.FloatLiteral:
                    return TokenCategory.Constant;*/

                /*case TokenKind.Error:
                    return TokenCategory.Invalid;*/

                /*default: 
                    return TokenCategory.Unknown;*/
            }

            return TokenCategory.Unknown;
        }
    }
}