using System.Linq;
using System.Text;

namespace Turtle_Tokenizer
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

        public static Lexeme GetNextLexeme()
        {
            StringBuilder text = new StringBuilder(32);
            Lexer.CharType type;

            if (Program.chars.Count == 1)
                return new Lexeme(GetValue().ToString(), GetType(true));

            do
            {
                type = GetType();
                text.Append(GetValue(true));
            }
            while (Program.chars.Any() && type == GetType() && type != Lexer.CharType.Accessor);

            Lexeme returnLexeme = new Lexeme(text.ToString(), type);
            return type == Lexer.CharType.Separator ? GetNextLexeme() : returnLexeme;

            char GetValue(bool removeChar = false)
            {
                char c = Program.chars.First();

                if (removeChar)
                    Program.chars.RemoveAt(0);

                return c;
            }

            Lexer.CharType GetType(bool removeChar = false)
            {
                Lexer.CharType _type = Lexer.GetType(GetValue());

                if (removeChar)
                    Program.chars.RemoveAt(0);

                return _type;
            }
        }
    }

    public class Lexer
    {
        public const string Operators = "=+-*/&|!";
        public const string Separators = ", ";
        public const string Accessors = "{}()[]";

        public enum CharType
        {
            Operator,
            Literal,
            Separator,
            Accessor
        }

        public static CharType GetType(char c)
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
    }
}
