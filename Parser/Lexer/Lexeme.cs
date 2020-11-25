namespace TSL
{
    public class Lexeme
    {
        public readonly string Text;
        public readonly Lexer.CharType Type;

        public Lexeme(string text, Lexer.CharType type)
        {
            Text = text;
            Type = type;
        }
    }
}
