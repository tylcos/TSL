using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSL
{
    public class Lexeme
    {
        public readonly string Text;
        public readonly Lexer.CharType Type;

        public Lexeme() { }

        public Lexeme(string _text, Lexer.CharType _type)
        {
            Text = _text;
            Type = _type;
        }
    }

    public class Lexer
    {
        public const string Operators = "=+-*/&|!";
        public const string Separators = ", ";
        public const string Accessors = "{}()[]";

        public List<char> Chars;

        public List<Lexeme> Lexemes { get; } = new List<Lexeme>();



        public Lexer(List<char> chars)
        {
            Chars = chars;
        }

        public Lexer(string str)
        {
            Chars = str.ToCharArray().ToList();
        }



        public Lexeme[] GetLexemes()
        {
            while (Chars.Any())
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

            do
            {
                type = GetType();
                text.Append(GetValue(true));
            }
            while (Chars.Count > 1 && type == GetType() && type != CharType.Accessor);

            return type == CharType.Separator ? null : new Lexeme(text.ToString(), type);



            char GetValue(bool removeChar = false)
            {
                char c = Chars.First();

                if (removeChar)
                    Chars.RemoveAt(0);

                return c;
            }

            CharType GetType(bool removeChar = false)
            {
                CharType _type = GetTypeOfChar(GetValue());

                if (removeChar)
                    Chars.RemoveAt(0);

                return _type;
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

            if (char.IsLetterOrDigit(c) || c == '.')
                return CharType.Literal;

            if (c == '\u0017')
                return CharType.NewLine;

            if (Operators.Contains(c))
                return CharType.Operator;

            if (Separators.Contains(c))
                return CharType.Separator;

            throw new InvalidCharException();
        }
    }
}
