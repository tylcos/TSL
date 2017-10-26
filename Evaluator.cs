using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public List<string> GetTokens()
        {
            StringBuilder sb = new StringBuilder();

            if (Lexemes[index].Type != Lexer.CharType.Literal)
                throw new Exception(); // REDO

            string lexemeText = Lexemes[index].Text;

            switch (lexemeText)
            {
                case "var":
                    sb.Append("var ");
                    sb.Append(Lexemes[index + 1].Text);

                    if (Lexemes[index + 2].Text == "=")
                    {
                        sb.Append(" " + GetVariableType(Lexemes[index + 3].Text));
                        sb.Append(" " + Lexemes[index + 3].Text);
                        index += 4;
                    }
                    else
                        index += 2;
                    break;
                case "function":
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }

        public string GetVariableType (string text)
        {
            if (float.TryParse(text, out float test1))
                return "num";
            if (bool.TryParse(text, out bool test2))
                return "bool";

            return "str";
        }
    }
}