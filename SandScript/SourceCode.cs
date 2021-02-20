using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SandScript.Lexer;

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

        public string GetLines(int index)
        {
            return _lines[index];
        }

        public string[] GetLines(int start, int end)
        {
            var lines = _lines.ToList();
            return lines.GetRange(start, end - start).ToArray();
        }

        public string GetSpan(SourceSpan span)
        {
            int start = span.Start.Index;
            int length = span.Length;
            return _sourceContent.Substring(start, length);
        }
    }
}