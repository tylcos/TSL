using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TSL
{
    internal class Program
    {
        const string PROGRAMPATH = @"\Example Files\in.txt";
        const string LEXEMEPATH  = @"\Example Files\out1.txt";
        const string TOKENSPATH  = @"\Example Files\out2.txt";



        public static void Main()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string program = File.ReadAllText(exePath + PROGRAMPATH);

            List<Lexeme> lexemes = new Lexer(program).GetLexemes();
            //IToken[] tokens = new Evaluator(lexemes).GetTokens();

            File.WriteAllLines(exePath + LEXEMEPATH, lexemes.Select(l => l.Type.ToString().PadRight(15) + l.Text.Replace("\n", "\\n")));
            //File.WriteAllLines(TOKENSPATH, );
        }
    }
}