using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;



namespace TSL
{
    public class Lexer
    {
        public const string Operators = "=+-*/&|!^%<>";
        public const string Separators = ", ";
        public const string Accessors = "{}()[]";

        static readonly string NEWLINE = '\u0017'.ToString();

        public char[] Chars;
        public int pos;

        public List<Lexeme> Lexemes { get; } = new List<Lexeme>();


        
        public Lexer(string lines)
        {
            Chars = Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(lines
                , @"\/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*\/+|\t|\/\/.*", "") // Removes comments, multiline comments, and tabs
                , @"\n|\r", NEWLINE)                                            // Replaces new lines with unicode linefeed
                , @"\u0017+", NEWLINE)                                          // Removes excess newlines
                , @"\s+", " ")                                                  // Removes excess whitespace
                .Trim().ToCharArray();
        }



        public Lexeme[] GetLexemes()
        {
            while (CharsRemaining())
            {
                Lexeme newLexeme = GetNextLexeme();

                if (newLexeme != null)
                    Lexemes.Add(newLexeme);
            }

            return Lexemes.ToArray();
        }

        private Lexeme GetNextLexeme()
        {
            StringBuilder text = new StringBuilder(32);
            CharType type;
            bool inQuotes = false;

            do
            {
                char c = GetValue(true);
                text.Append(c);

                type = GetTypeOfChar(c);

                if (c == '"')
                    inQuotes = !inQuotes;
            }
            while (CharsRemaining() && (inQuotes || (type == GetType() && type != CharType.Accessor)));

            return type == CharType.Separator ? null : new Lexeme(text.ToString(), type);



            char GetValue(bool removeChar = false)
            {
                char c = Chars[pos];

                if (removeChar)
                    pos++;

                return c;
            }

            CharType GetType(bool removeChar = false)
            {
                return GetTypeOfChar(GetValue(removeChar));
            }
        }

        public enum CharType
        {
            Accessor,
            Literal,
            NewLine,
            Operator,
            Separator
        }

        public static CharType GetTypeOfChar(char c)
        {
            if (Accessors.Contains(c))
                return CharType.Accessor;

            if (char.IsLetterOrDigit(c) || ".\"".Contains(c))
                return CharType.Literal;

            if (c == '\u0017')
                return CharType.NewLine;

            if (Operators.Contains(c))
                return CharType.Operator;

            if (Separators.Contains(c))
                return CharType.Separator;

            throw new InvalidCharException();
        }



        bool CharsRemaining()
        {
            return pos < Chars.Length - 1;
        }
    }
}
