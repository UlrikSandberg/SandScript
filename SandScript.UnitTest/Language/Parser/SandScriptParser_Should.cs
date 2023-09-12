using SandScript.Language.Lexer;
using SandScript.Language.Parser;

namespace SandScript.UnitTest.Language.Parser;

public class SandScriptParser_Should
{

    [Fact]
    public void Something()
    {
        var parser = new SandScriptParser();
        var sourceCode2 = "var a = 10 + 7 - 3 * 4 / 5 % 7";

        var ast = parser.ParseScript(sourceCode2);
        
        Assert.True(true);
    }
}