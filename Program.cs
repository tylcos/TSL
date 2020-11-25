using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace TSL
{
    internal class Program
    {
        static readonly string TEXTPATH   = "a.txt";
        static readonly string LEXEMEPATH = "b.txt";

        

        public static void Main()
        {
            string lines = File.ReadAllText(TEXTPATH);
            
            Lexeme[] lexemes = new Lexer(lines).GetLexemes();
            IToken[] tokens  = new Evaluator(lexemes).GetTokens();

            File.WriteAllLines(LEXEMEPATH, PrintList(lexemes));
        }



        static string[] PrintList (Lexeme[] list)
        {
            return list.Select(l => l.Type.ToString().PadRight(15) + l.Text).ToArray();
        }
    }
}