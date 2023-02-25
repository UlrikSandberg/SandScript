namespace SandScript.Language.Syntax;

public class SourceRoot
{
    private readonly List<SyntaxNode> _rootNodes;

    public SourceRoot(List<SyntaxNode> rootNodes)
    {
        _rootNodes = rootNodes;
    }
}