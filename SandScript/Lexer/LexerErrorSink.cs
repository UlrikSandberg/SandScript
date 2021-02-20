using System.Collections;
using System.Collections.Generic;

namespace SandScript.Lexer
{
    public class LexerErrorSink : IEnumerable<LexerError>
    {
        private List<LexerError> errors;
        public IEnumerable<LexerError> Errors => errors.AsReadOnly();
        public bool HasErrors => errors.Count > 0;

        public LexerErrorSink()
        {
            errors = new List<LexerError>();
        }

        public void AddError(string message, SourceCode code, Severity severity, SourceSpan span)
        {
            errors.Add(new LexerError(message, code.GetLines(span.Start.Line, span.End.Line), severity, span));
        }

        public void Clear()
        {
            errors.Clear();
        }
        
        public IEnumerator<LexerError> GetEnumerator()
        {
            return errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}