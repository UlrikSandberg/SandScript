using SandScript.AST.Expressions;
using SandScript.Lexer;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private bool IsPrefixUnaryOperator()
        {
            
        }

        private bool IsSuffixUnaryOperator()
        {
            
        }
        
        private Expression ParseExpression()
        {
            return ParseAssignmentExpression();
        }

        private Expression ParseAssignmentExpression()
        {
            var left = ParseLogical
        }

        private Expression ParseLogicalExpression()
        {
        }

        private Expression ParseEqualityExpression()
        {
            var left
        }

        private Expression ParseRelationalExpression()
        {
            
        }

        private Expression ParseBitwiseExpression()
        {
            
        }

        private Expression ParseShiftExpressions()
        {
            
        }

        private Expression ParseAdditiveExpressions()
        {
            
        }

        private Expression ParseMultiplicativeExpression()
        {
            
        }

        private UnaryOperator ParsePrefixUnaryOperator()
        {
            
        }

        private UnaryOperator ParseSuffixUnaryOperator()
        {
            
        }

        private Expression ParsePrimaryExpression()
        {
            
        }

        private Expression ParseUnaryExpression()
        {
            var unaryOperator = UnaryOperator.Default;
            SourceLocation? start = null;

            if (IsPrefixUnaryOperator())
            {
                
            }
            
        }
    }
}