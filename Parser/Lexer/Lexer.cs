using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSL
{
    public record Lexeme(string Text, CharType Type);


    public class Lexer
    {
        public List<Lexeme> Lexemes { get; private set; }

        private readonly char[] program;
        private int pos;


        public const string Operators  = "=+-*/&|!^%<>";
        public const string Separators = ",";
        public const string Accessors  = ".{}()[]";
        public const string Whitespace = " \r\n";



        public Lexer(string programString)
        {
            program = programString.ToCharArray();
            pos = 0;
        }


        /// <summary>
        /// Returns all parsable lexemes from the program
        /// </summary>
        /// <returns>List of parsed lexemes</returns>
        public List<Lexeme> GetLexemes()
        {
            Lexemes = new List<Lexeme>();


            while (CharsRemaining())
            {
                Lexeme nextLexeme = GetNextLexeme();

                if (nextLexeme != null)
                    Lexemes.Add(nextLexeme);
            }


            return Lexemes;
        }

        /// <summary>
        /// Returns the next parsable lexeme if it's available
        /// </summary>
        /// <returns>Next lexeme or null if there are no more characters</returns>
        private Lexeme GetNextLexeme()
        {
            var text = new StringBuilder(16);
            bool inQuotes = false;

            CharType currentType = GetTypeOfChar(program[pos]);
            CharType nextType = currentType;
            while (CharsRemaining())
            {
                char c = program[pos++];
                currentType = nextType; // Prevents classifying character twice
                if (CharsRemaining())
                    nextType = GetTypeOfChar(program[pos]);


                if (currentType == CharType.Whitespace)
                    continue;
                if (c == '"')
                    inQuotes = !inQuotes;


                text.Append(c);

                // Continue if currently parsing a string, the next char is of the same type as the current, and stop if an accessor was reached 
                if (inQuotes || (currentType == nextType && currentType != CharType.Accessor))
                    continue;

                break;
            }


            return new Lexeme(text.ToString(), currentType);
        }

        private bool CharsRemaining() => pos < program.Length;



        // Could convert to an ASCII table for performance
        public static CharType GetTypeOfChar(char c)
        {
            if (Accessors.Contains(c))
                return CharType.Accessor;

            if (char.IsLetterOrDigit(c) || "\"".Contains(c))
                return CharType.Literal;

            if (Whitespace.Contains(c))
                return CharType.Whitespace;

            if (Operators.Contains(c))
                return CharType.Operator;

            if (Separators.Contains(c))
                return CharType.Separator;

            throw new InvalidCharException();
        }
    }


    public enum CharType
    {
        Accessor,
        Literal,
        Whitespace,
        Operator,
        Separator
    }
}
