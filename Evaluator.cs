using System.Collections.Generic;
using System.Linq;

namespace TSL
{
    public class Evaluator
    {
        public readonly string[] keywords = { "var", "function", "true", "false" };

        public List<string> variables = new List<string>();
        public List<string> functions = new List<string>();

        Lexeme[] Lexemes { get; set; }
        int index = 0;

        public Evaluator(List<Lexeme> _lexemes)
        {
            Lexemes = _lexemes.ToArray();
        }

        public List<Token> GetTokens()
        {
            List<Token> tokens = new List<Token>();

            while (Lexemes.Any())
                tokens.Add(GetNextToken());

            return tokens;
        }

        public Token GetNextToken()
        {
            index++;

            Lexeme first = Lexemes[index];

            return new Token(Token.TokenType.Addition);
        }

        private Token TokenSelector ()
        {
            Token token;

            switch (Lexemes[index].Type)
            {
                case Lexer.CharType.Literal:
                    break;
                case Lexer.CharType.Operator:
                    break;
                case Lexer.CharType.Separator:
                    break;
                case Lexer.CharType.Accessor:
                    break;
            }

            return token;
        }
    }
}