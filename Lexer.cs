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

        public Lexer(List<char> chars)
        {
            Chars = chars;
        }

        public Lexer(string str)
        {
            Chars = str.ToCharArray().ToList();
        }

        public enum CharType
        {
            Operator,
            Literal,
            Separator,
            Accessor
        }

        public static CharType GetTypeOfChar(char c)
        {
            if (char.IsLetterOrDigit(c) || c == '.')
                return CharType.Literal;

            if (Operators.Contains(c))
                return CharType.Operator;

            if (Accessors.Contains(c))
                return CharType.Accessor;

            if (Separators.Contains(c))
                return CharType.Separator;

            throw new InvalidCharException();
        }

        public List<Lexeme> GetLexemes()
        {
            List<Lexeme> lexemes = new List<Lexeme>();

            while (Chars.Any())
                lexemes.Add(GetNextLexeme());

            return lexemes;
        }

        public Lexeme GetNextLexeme()
        {
            StringBuilder text = new StringBuilder(32);
            CharType type;

            if (Chars.Count == 1)
                return new Lexeme(GetValue().ToString(), GetType(true));

            do
            {
                type = GetType();
                text.Append(GetValue(true));
            }
            while (Chars.Any() && type == GetType() && type != CharType.Accessor);

            Lexeme returnLexeme = new Lexeme(text.ToString(), type);
            return type == CharType.Separator ? GetNextLexeme() : returnLexeme;

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
    }
}
