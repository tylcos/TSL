using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TSL
{
    internal class Program
    {
        public static List<char> chars;

        static readonly string TEXTPATH   = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\a.txt";
        static readonly string LEXEMEPATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\b.txt";

        static readonly string NEWLINE    = '\u0017'.ToString();

        public static void Main()
        {
            string lines = File.ReadAllText(TEXTPATH);
            chars = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(lines
                , @"\/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*\/+|\t|\/\/.*", "") // Removes comments, multiline comments, and tabs
                , @"\n|\r", NEWLINE)                                            // Replaces new lines with unicode linefeed
                , @"\u0017+", NEWLINE)                                          // Removes excess newlines
                , @"\s+", " ")                                                  // Removes excess whitespace
                .Trim().ToCharArray().ToList();

            Lexeme[] lexemes = new Lexer(chars).GetLexemes();
            Token[]  tokens  = new Evaluator(lexemes).GetTokens();

            File.WriteAllLines(LEXEMEPATH, PrintList(lexemes));
        }

        static string[] PrintList (Lexeme[] list)
        {
            string[] printlist = new string[list.Length];

            for (int i = 0; i < list.Length; i++)
                printlist[i] = list[i].Type + new String(' ', 15 - list[i].Type.ToString().Length) + list[i].Text;

            return printlist;
        }
    }
}