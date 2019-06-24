using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSL
{
    class Evaluator
    {
        public List<string> UserVariables = new List<string>();
        public List<string> UserFunctions = new List<string>();

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
                    SyntaxCheck(!CheckLexemes('l', 'l'), "The 'var' keyword expects a literal for the name");

                    string variableName = GetValue(1);
                    SyntaxCheck(UserVariables.Contains(variableName), $"The variable '{variableName}' is declared twice");
                    UserVariables.Add(variableName);

                    Tokens.Add(new VariableDeclaration(variableName));
                    Remove(2);

                    if (GetValue() == "=")
                    {
                        SyntaxCheck(GetType(1) != Lexer.CharType.NewLine, "No expression after assignment");

                        int expressionLength = 0;
                        while (GetType(1 + expressionLength) != Lexer.CharType.NewLine)
                            expressionLength++;

                        Expression expression = new Expression(Lexemes.GetRange(1, expressionLength).ToArray());
                        

                        Remove(1 + expressionLength);
                    }

                    break;
                case "function":
                    SyntaxCheck(!CheckLexemes('l', 'l'), "The 'function' keyword expects a literal for the name");

                    string functionName = GetValue(1);
                    SyntaxCheck(UserFunctions.Contains(functionName), $"The function '{functionName}' is defined twice");
                    UserFunctions.Add(functionName);

                    StringBuilder sb = new StringBuilder();
                    int i = 1;
                    while (GetValue(++i) != ")")
                        sb.Append(GetValue(i));

                    List<Parameter> parameters = new List<Parameter>();
                    foreach (string parameter in sb.ToString().Split(','))
                    {
                        string[] parts = parameter.Split('=');
                        string defaultValue = parts.Length == 2 ? parts[1] : null;

                        parameters.Add(new Parameter(parts[0], new Value(defaultValue)));
                    }

                    Tokens.Add(new FunctionDeclaration(functionName, parameters.ToArray()));
                    Remove(1 + i);

                    break;
                default:

                    break;
            }

            count++;
        }

        private string GetValue(int offset = 0)
        {
            return Lexemes[offset].Text;
        }

        private Lexer.CharType GetType(int offset = 0)
        {
            return Lexemes[offset].Type;
        }

        private void Remove(int amountToRemove = 1)
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

            for (int i = 0; lexemesMatch && i < lexemeTypes.Length; i++)
            {
                if (Lexemes[offset + i].Type != lexemeTypes[i])
                    lexemesMatch = false;
            }

            return lexemesMatch;
        }
    }
}