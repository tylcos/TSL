using System;
using System.Collections.Generic;
using System.Linq;

namespace TSL
{
    public class Evaluator
    {
        public List<string> Variables = new List<string>();
        public List<string> Functions = new List<string>();

        public List<Lexeme> Lexemes = new List<Lexeme>();

        public List<Token> Tokens { get; } = new List<Token>();

        private int count = 0;



        public Evaluator(Lexeme[] lexemes)
        {
            Lexemes = lexemes.ToList();
        }



        public Token[] GetTokens()
        {
            while (count < Lexemes.Count)
                GetToken();

            return Tokens.ToArray();
        }

        private void GetToken()
        {
            string lexemeText = Lexemes[0].Text;

            switch (lexemeText)
            {
                case "var":
                    SyntaxCheck(!CheckLexemes('l', 'l'), "'var' keyword expects a literal for the name");

                    string variableName = GetValue(1);
                    SyntaxCheck(Variables.Contains(variableName), $"'{variableName}' is already declared");
                    Variables.Add(variableName);

                    Tokens.Add(new VariableDeclaration(variableName));
                    Remove(2);
                    

                    /*
                    if (CheckLexemes(2, 'o', 'l'))
                    {
                        SyntaxCheck(GetValue(2) == "=", "variable decleration and assignment must have a '=' operator");

                        string value = GetValue(3);

                        if (Variables.Contains(value))
                            sb.Append(" val " + value);
                        else
                            sb.Append(" " + GetVariableType(value) + value);

                        index += 4;
                    }*/
                        



                    break;
                case "=":
                    //if (Tokens.Last() ==
                    break;
                case "function":

                    /*
                    if (GetValue(2) == "(")
                    {
                        string paramName = GetValue(3);

                        while (paramName != ")")
                        {
                            if (GetValue(4) == "=")
                            {
                                sb.Append($" paramd {paramName} {GetValue(5)}");
                                offset += 3;
                            }
                            else if (GetValue(4) == "," || GetValue(4) == ")")
                            {
                                sb.Append(" param " + paramName);
                                offset += 2;
                            }

                            paramName = GetValue(3);
                        }
                    }*/

                    break;
                case "":
                    break;
            }

            count++;
        }

        private string GetValue(int offset)
        {
            return Lexemes[offset].Text;
        }

        private void Remove(int amountToRemove)
        {
            Lexemes.RemoveRange(0, amountToRemove);
        }

        public string GetVariableType(string text)
        {
            if (float.TryParse(text, out _))
                return "num";
            if (bool.TryParse(text, out _))
                return "bool";

            return "str";
        }

        public void SyntaxCheck(bool check, string msg)
        {
            if (check)
                throw new InvalidSyntaxException(msg);
        }

        public bool CheckLexemes(params char[] lexemeTypes)
        {
            return CheckLexemes(0, lexemeTypes);
        }

        public bool CheckLexemes(int offset, params char[] lexemeTypes)
        {
            Lexer.CharType[] convertedLexemeTypes = new Lexer.CharType[lexemeTypes.Length];

            for (int i = 0; i < lexemeTypes.Length; i++)
            {
                switch (lexemeTypes[i])
                {
                    case 'a': convertedLexemeTypes[i] = Lexer.CharType.Accessor;  break;
                    case 'l': convertedLexemeTypes[i] = Lexer.CharType.Literal;   break;
                    case 'n': convertedLexemeTypes[i] = Lexer.CharType.NewLine;   break;
                    case 'o': convertedLexemeTypes[i] = Lexer.CharType.Operator;  break;
                    case 's': convertedLexemeTypes[i] = Lexer.CharType.Separator; break;
                }
            }

            return CheckLexemes(offset, convertedLexemeTypes);
        }

        public bool CheckLexemes(int offset, params Lexer.CharType[] lexemeTypes)
        {
            bool lexemesMatch = true;

            for (int i = 0; i < lexemeTypes.Length && lexemesMatch; i++)
            {
                if (Lexemes[offset + i].Type != lexemeTypes[i])
                    lexemesMatch = false;
            }

            return lexemesMatch;
        }
    }
}