namespace SandScript
{
    public class SandscriptSyntaxConfiguration
    {
        public bool IsSemicolonOptional { get; private set; }

        public static SandscriptSyntaxConfiguration Default()
        {
            return new SandscriptSyntaxConfiguration
            {
                IsSemicolonOptional = false
            };
        }
        public static SandscriptSyntaxConfiguration OptionalSemicolon()
        {
            return new SandscriptSyntaxConfiguration
            {
                IsSemicolonOptional = false
            };
        }
    }
}