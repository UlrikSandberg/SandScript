using System;
using SandScript.AST.Expressions;
using SandScript.Lexer;

namespace SandScript.Extensions
{
    public static class TokenExtensions
    {
        public static bool IsTrivia(this Token token)
        {
            return token.TokenCategory == TokenCategory.Whitespace || token.TokenCategory == TokenCategory.Comment;
        }

        public static BinaryOperator AsBinaryOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.Plus: return BinaryOperator.Add;
                case TokenType.Minus: return BinaryOperator.Sub;
                case TokenType.Assignment: return BinaryOperator.Assign;
                case TokenType.PlusEqual: return BinaryOperator.AddAssign;
                case TokenType.MinusEqual: return BinaryOperator.SubAssign;
                case TokenType.MulEqual: return BinaryOperator.MulAssign;
                case TokenType.DivideEqual: return BinaryOperator.DivAssign;
                case TokenType.ModEqual: return BinaryOperator.ModAssign;
                case TokenType.BitwiseAndEqual: return BinaryOperator.AndAssign;
                case TokenType.BitwiseOrEqual: return BinaryOperator.OrAssign;
                case TokenType.BitwiseXorEqual: return BinaryOperator.XorAssign;
                case TokenType.BitwiseAnd: return BinaryOperator.BitwiseAnd;
                case TokenType.BitwiseOr: return BinaryOperator.BitwiseOr;
                case TokenType.BitwiseXor: return BinaryOperator.BitwiseXor;
                case TokenType.Equal: return BinaryOperator.Equal;
                case TokenType.NotEqual: return BinaryOperator.NotEqual;
                case TokenType.BoolOr: return BinaryOperator.LogicalOr;
                case TokenType.BoolAnd: return BinaryOperator.LogicalAnd;
                case TokenType.Mul: return BinaryOperator.Mul;
                case TokenType.Divide: return BinaryOperator.Div;
                case TokenType.Mod: return BinaryOperator.Div;
                case TokenType.GreaterThan: return BinaryOperator.GreaterThan;
                case TokenType.LessThan: return BinaryOperator.LessThan;
                case TokenType.GreaterThanOrEqual: return BinaryOperator.GreaterThanOrEqual;
                case TokenType.LessThanOrEqual: return BinaryOperator.LessThanOrEqual;
                case TokenType.BitShiftLeft: return BinaryOperator.LeftShift;
                case TokenType.BitShiftRight: return BinaryOperator.RightShift;
            }

            return BinaryOperator.Error;
        }

        public static bool IsAdditiveOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.Plus:
                case TokenType.Minus:
                    return true;
                default : return false;
            }
        }

        public static bool IsAssignmentOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.Assignment:
                case TokenType.PlusEqual:
                case TokenType.MinusEqual:
                case TokenType.MulEqual:
                case TokenType.DivideEqual:
                case TokenType.ModEqual:
                case TokenType.BitwiseAndEqual:
                case TokenType.BitwiseOrEqual:
                case TokenType.BitwiseXorEqual:
                    return true;
                default: return false;
            }
        }

        public static bool IsBitwiseOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.BitwiseAnd:
                case TokenType.BitwiseOr:
                case TokenType.BitwiseXor:
                    return true;
                default: return false;
            }
        }

        public static bool IsEqualityOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.Equal:
                case TokenType.NotEqual:
                    return true;
                default: return false;
            }
        }

        public static bool IsLogicalOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.BoolAnd:
                case TokenType.BoolOr:
                    return true;
                default: return false;
            }
        }

        public static bool IsMultiplicativeOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.Mul:
                case TokenType.Divide:
                case TokenType.Mod:
                    return true;
                default: return false;
            }
        }

        public static bool IsPrefixUnaryOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.PlusPlus:
                case TokenType.MinusMinus:
                case TokenType.MulMul:
                case TokenType.Not:
                case TokenType.Minus:
                    return true;
                default: return false;
            }
        }

        public static bool IsRelationalOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.GreaterThan:
                case TokenType.LessThan:
                case TokenType.GreaterThanOrEqual:
                case TokenType.LessThanOrEqual:
                    return true;
                default: return false;
            }
        }

        public static bool IsShiftOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.BitShiftLeft:
                case TokenType.BitShiftRight:
                    return true;
                default: return false;
            }
        }

        public static bool IsSuffixUnaryOperator(this TokenType token)
        {
            switch (token)
            {
                case TokenType.PlusPlus:
                case TokenType.MinusMinus:
                case TokenType.MulMul:
                    return true;
                
                default: return false;
            }
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