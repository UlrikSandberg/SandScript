using System.Diagnostics.SymbolStore;

namespace SandScript.Lexer
{
    public class Token
    {
        public int Id { get; private set; }
        public TokenType Type { get; private set; }
        public TokenCategory Category { get; private set; }
        public string Symbol { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }
        
        public Token(int id, TokenType type, string symbol, int line, int column)
        {
            Id = id;
            Type = type;
            Symbol = symbol;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return $"ID: {Id} \t Type: {Type.ToString()} \t Line: {Line}[{Column}] \t Symbol: {Symbol}";
        }
    }
}