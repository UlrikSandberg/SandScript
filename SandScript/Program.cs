using System;
using System.IO;
using System.Linq;

namespace SandScript
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var codeFile = "/Users/ulriksandberg/Projects/Unity/SandScript/SandScript/code.txt";

            var text = File.ReadAllText(codeFile);
            
            var virtualCompiler = new VirtualCompiler(text);

            var virtualProgram = virtualCompiler.Compile();
            
            // Use the virtual program to execute code which 
            
        }
    }
}