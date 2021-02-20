using System;
using System.Diagnostics.SymbolStore;
using SandScript.Extensions;

namespace SandScript.Lexer
{
    public class Token : IEquatable<Token>
    {
        public int Id { get; }
        public TokenType Type { get; }
        public string Symbol { get; }
        public SourceSpan CodeSpan { get; }
        public TokenCategory TokenCategory { get; }

        public Token(int id, TokenType type, string symbol, SourceSpan codeSpan)
        {
            Id = id;
            Type = type;
            Symbol = symbol;
            CodeSpan = codeSpan;
            TokenCategory = type.AsTokenCategory();
        }

        public override string ToString()
        {
            return $"ID: {Id} \t Type: {Type.ToString()} \t\t Line: {CodeSpan}] \t\t Symbol: {Symbol}";
        }

        public static bool operator !=(Token left, string right)
        {
            return left?.Symbol != right;
        }

        public static bool operator !=(string left, Token right)
        {
            return right?.Symbol != left;
        }

        public static bool operator !=(Token left, TokenType right)
        {
            return left?.Type != right;
        }

        public static bool operator !=(TokenType left, Token right)
        {
            return right?.Type != left;
        }

        public static bool operator ==(Token left, string right)
        {
            return left?.Symbol == right;
        }

        public static bool operator ==(string left, Token right)
        {
            return right?.Symbol == left;
        }

        public static bool operator ==(Token left, TokenType right)
        {
            return left?.Type == right;
        }

        public static bool operator ==(TokenType left, Token right)
        {
            return right?.Type == left;
        }

        public override bool Equals(object obj)
        {
            if (obj is Token)
            {
                return Equals((Token)obj);
            }
            return base.Equals(obj);
        }

        public bool Equals(Token other)
        {
            if (other == null)
            {
                return false;
            }
            return other.Symbol == Symbol &&
                   other.CodeSpan == CodeSpan &&
                   other.Type == Type;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode() ^ CodeSpan.GetHashCode() ^ Type.GetHashCode();
        }
    }
}