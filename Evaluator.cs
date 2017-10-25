using System;
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

        public List<IToken> GetTokens()
        {
            List<IToken> tokens = new List<IToken>();

            while (Lexemes.Any())
                tokens.Add(GetNextToken());

            return tokens;
        }

        private IToken GetNextToken()
        {
            throw new NotImplementedException();
        }

        private IToken TokenSelector ()
        {
            IToken token;
            string lexemeText = Lexemes[index].Text;

            switch (lexemeText)
            {
                case "var":
                    token = new VariableDeclaration(lexemeText);
                    break;
            }
            token = new VariableDeclaration(lexemeText);
            return token;
        }
    }
}