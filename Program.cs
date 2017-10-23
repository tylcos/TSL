using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TSL
{
    class Program
    {
        static List<Token> tokens = new List<Token>();

        public static List<char> chars;

        static readonly string TEXTPATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\a.txt";

        public static void Main()
        {
            string lines = File.ReadAllText(TEXTPATH);
            chars = Regex.Replace(Regex.Replace(Regex.Replace(lines
                , @"\/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*\/+|\t|\/\/.*", "") // Removes comments, multiline comments, and tabs
                , @"\n|\r", " ")                                                // Replaces new lines
                , @"\s+", " ")                                                  // Removes excess whitespace
                .Trim().ToCharArray().ToList();

            Lexer lexer = new Lexer(chars);
            List<Lexeme> lexemes = lexer.GetLexemes();



            Evaluator eval = new Evaluator(lexemes);
            tokens = eval.GetTokens();


            Console.Read();
        }
    }

    public class Variable
    {
        public readonly Type Type;
        public readonly object Value;

        public Variable(Type _type, object _value)
        {
            Type = _type;
            Value = _value;
        }

        public T GetValue<T>()
        {
            return (T)Value;
        }
    }
}