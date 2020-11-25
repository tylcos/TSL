using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TSL
{
    internal class Program
    {
        static readonly string PROGRAMPATH = @"\Example Files\in.txt";
        static readonly string LEXEMEPATH  = @"\Example Files\out1.txt";
        static readonly string TOKENSPATH  = @"\Example Files\out2.txt";



        public static void Main()
        {
            string program = File.ReadAllText(PROGRAMPATH);

            List<Lexeme> lexemes = new Lexer(program).GetLexemes();
            IToken[] tokens = new Evaluator(lexemes).GetTokens();

            File.WriteAllLines(LEXEMEPATH, lexemes.Select(l => l.Type.ToString().PadRight(15) + l.Text));
            //File.WriteAllLines(TOKENSPATH, );
        }
    }
}