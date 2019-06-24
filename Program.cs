using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TSL
{
    internal class Program
    {
        static readonly string TEXTPATH   = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\a.txt";
        static readonly string LEXEMEPATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\b.txt";

        

        public static void Main()
        {
            string lines = File.ReadAllText(TEXTPATH);
            
            Lexeme[] lexemes = new Lexer(lines).GetLexemes();
            Token[]  tokens  = new Evaluator(lexemes).GetTokens();

            File.WriteAllLines(LEXEMEPATH, PrintList(lexemes));
        }



        static string[] PrintList (Lexeme[] list)
        {
            string[] printlist = new string[list.Length];

            for (int i = 0; i < list.Length; i++)
                printlist[i] = list[i].Type + new string(' ', 15 - list[i].Type.ToString().Length) + list[i].Text;

            return printlist;
        }
    }
}