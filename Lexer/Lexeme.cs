namespace TSL
{
    public class Lexeme
    {
        public readonly string Text;
        public readonly Lexer.CharType Type;

        public Lexeme(string _text, Lexer.CharType _type)
        {
            Text = _text;
            Type = _type;
        }
    }
}
