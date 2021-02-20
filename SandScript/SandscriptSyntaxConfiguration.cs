using System;

namespace SandScript
{
    public class SandscriptSyntaxConfiguration
    {
        public bool IsSemicolonOptional { get; set; }
        public bool IsRootLevelStatementsAllowed { get; set; }

        public static SandscriptSyntaxConfiguration Default()
        {
            return new SandscriptSyntaxConfiguration
            {
                IsSemicolonOptional = false,
                IsRootLevelStatementsAllowed = false
            };
        }
        public static SandscriptSyntaxConfiguration OptionalSemicolon()
        {
            return new SandscriptSyntaxConfiguration
            {
                IsSemicolonOptional = true
            };
        }

        public static SandscriptSyntaxConfiguration Builder(Action<SandscriptSyntaxConfiguration> ctx)
        {
            var config = new SandscriptSyntaxConfiguration();
            ctx(config);
            return config;
        }
    }
}