using System.Diagnostics.SymbolStore;
using SandScript.Extensions;

namespace SandScript.Lexer
{
    public class Token
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
    }
}