using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Turtle_Tokenizer
{
    class Program
    {
        static Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
        static Dictionary<string, Token[]> functions = new Dictionary<string, Token[]>();

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

            List<Lexeme> lexemes = new List<Lexeme>();

            while (chars.Any())
            {
                lexemes.Add(Lexeme.GetNextLexeme());
                Console.WriteLine(lexemes.Last().Text + string.Join("", Enumerable.Repeat(' ', 10 - lexemes.Last().Text.Length)) + lexemes.Last().Type);
            }

            Console.Read();
        }
    }

    public class Evaluator
    {
        Lexeme[] Lexemes { get; set; }
        Token[]  Tokens  { get; set; }

        public Evaluator(Lexeme[] _lexemes)
        {
            Lexemes = _lexemes;
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

    public class Token
    {
        public readonly TokenType type;

        public int LineNumber;
        public int CharNumber;


        public Token(TokenType _type)
        {
            type = _type;
        }

        public enum TokenType
        {
            VariableDeclaration,
            FunctionDeclaration,
            Assignment,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Not,
            And,
            Or,
            ArrayAccess,
            Parameter,
            Call,
            String,
            Int,
            Double,
            Bool

        }
    }
}