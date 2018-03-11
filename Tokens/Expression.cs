using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSL
{
    class Expression : Token
    {
        List<Symbol> inputSymbols = new List<Symbol>();

        public Expression(Lexeme[] lexemes)
        {
            inputSymbols = ParseLexemes(lexemes);
        }

        public double Evaulate() // Implement in runtime
        {
            return 0;
        }

        public List<Symbol> ParseLexemes (Lexeme[] lexemes)
        {
            List<Symbol> returnSymbols = new List<Symbol>();

            for (int i = 0; i < lexemes.Length; i++)
            {
                if (lexemes[i].Type == Lexer.CharType.Operator)
                    returnSymbols.Add(new Symbol(lexemes[i].Text, Symbol.SymbolType.Operator));
                else if (double.TryParse(lexemes[i].Text, out double result))
                    returnSymbols.Add(new Constant(result));
                else if (lexemes[i].Type == Lexer.CharType.Accessor && "()".Contains(lexemes[i].Text))
                    returnSymbols.Add(new Symbol(lexemes[i].Text, Symbol.SymbolType.Punctuation));
                else
                {
                    char[] chars = lexemes[i].Text.ToCharArray();
                    if (chars.All(c => char.IsLetterOrDigit(c) || c == '.')) // Test for valid variable/function name
                    {
                        if (lexemes[i + 1].Text == "(")
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(lexemes[i].Text + "(");
                            while (lexemes[i + 2].Text != ")") // Get end position of arguments
                                sb.Append(lexemes[i++].Text);

                            sb.Append(")");
                            i += 3;
                            returnSymbols.Add(new Symbol(sb.ToString(), Symbol.SymbolType.Function));
                        }
                        else
                            returnSymbols.Add(new Symbol(lexemes[i].Text, Symbol.SymbolType.Variable));
                    }
                }
            }

            return returnSymbols;
        }
    }

    

    class Symbol
    {
        public string Value;
        public SymbolType Type;

        public Symbol(string value, SymbolType type)
        {
            Value = value;
            Type = type;
        }

        public enum SymbolType
        {
            Constant, Variable, Function, Operator, Punctuation
        }
    }

    class Constant : Symbol
    {
        public new double Value;

        public Constant(double value) : base("", SymbolType.Constant)
        {
            Value = value;
        }
    }
}
