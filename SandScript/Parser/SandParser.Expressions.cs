using SandScript.AST.Expressions;
using SandScript.Extensions;
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
            var left = ParseLogicalExpression();
            
            if (!_current.Type.IsAssignmentOperator()) return left;
            
            var op = ParseBinaryOperator();
            var right = ParseAssignmentExpression();
                
            return new BinaryExpression(CreateSpan(left), left, right, op);
        }

        private BinaryOperator ParseBinaryOperator()
        {
            var token = Ingest();
            var binaryOperator = token.Type.AsBinaryOperator();
            if (binaryOperator != BinaryOperator.Error)
            {
                return binaryOperator;
            }
            
            _index--;
            throw UnexpectedToken("Binary Operator");
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