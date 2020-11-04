using System;

namespace SandScript
{
    public class SourceCode
    {
        private readonly string _sourceContent;
        private readonly string[] _lines;

        public SourceCode(string sourceCode)
        {
            _sourceContent = sourceCode;
            _lines = sourceCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        // Create indexer for code
        public char this[int index]
        {
            get
            {
                var sourceArray = _sourceContent.ToCharArray();
                return sourceArray.Length - 1 < index ? '\0' : sourceArray[index];
            }
        }
    }
}