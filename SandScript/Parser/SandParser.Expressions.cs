using SandScript.AST.Expressions;

namespace SandScript.Parser
{
    public partial class SandParser
    {
        private Expression ParseExpression()
        {
            return ParseAssignmentExpression();
        }

        private Expression ParseAssignmentExpression()
        {
            
        }
    }
}