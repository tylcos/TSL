using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace TSL
{
    class Evaluator
    {
        public Lexeme[] Lexemes;
        public List<IToken> Tokens { get; } = new List<IToken>();
        public int pos;



        public List<string> UserVariables = new List<string>();
        public List<string> UserFunctions = new List<string>();



        private readonly char[] SpecialLiterals = ".\"".ToCharArray();



        public Evaluator(Lexeme[] lexemes)
        {
            Lexemes = lexemes;
        }



        public IToken[] GetTokens()
        {
            while (pos < Lexemes.Length)
                GetToken();

            return Tokens.ToArray();
        }

        private void GetToken()
        {
            string lexemeText = GetValue();

            switch (lexemeText)
            {
                case "var":
                    SyntaxAssert(LexemesMatch(1, 'l'), "The 'var' keyword expects a literal for the name");

                    string variableName = GetValue(1);
                    SyntaxAssert(variableName.IndexOfAny(SpecialLiterals) == -1, $"Invaild variable name for '{variableName}'");
                    SyntaxAssert(!UserVariables.Contains(variableName), $"The variable '{variableName}' is declared twice");
                    UserVariables.Add(variableName);

                    Tokens.Add(new VariableDeclaration(variableName));
                    MovePos(2);



                    if (GetValue() == "=")
                    {
                        SyntaxAssert(GetType(1) != Lexer.CharType.NewLine, "No expression after assignment");

                        int expressionLength = 1; // Includes assignment operator
                        while (GetType(expressionLength) != Lexer.CharType.NewLine)
                            expressionLength++;

                        Tokens.Add(new Assignment(variableName, new Expression(Lexemes, expressionLength))); // Wrong

                        MovePos(expressionLength);
                    }

                    break;

                case "def":
                    SyntaxAssert(LexemesMatch(1, 'l'), "The 'def' keyword expects a literal for the name");

                    string functionName = GetValue(1);
                    SyntaxAssert(functionName.IndexOfAny(SpecialLiterals) == -1, $"Invaild variable name for '{functionName}'");
                    SyntaxAssert(!UserFunctions.Contains(functionName), $"The function '{functionName}' is defined twice");
                    UserFunctions.Add(functionName);
                    MovePos(2);



                    SyntaxAssert(GetValue() == "(", "The 'def' keyword expects parenthesis around the parameters");

                    StringBuilder sb = new StringBuilder();

                    int i = 0; // Number of lexemes after open parenthesis
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
                    MovePos(1 + i);

                    break;

                default:
                    MovePos(1);

                    break;
            }
        }



        private string GetValue(int offset = 0)
        {
            return Lexemes[pos + offset].Text;
        }

        private Lexer.CharType GetType(int offset = 0)
        {
            return Lexemes[pos + offset].Type;
        }

        private void MovePos(int amountToRemove = 1)
        {
            pos += amountToRemove;
        }

        public int GetVariableType(string text)
        {
            if (text.All(char.IsDigit))
                return 0;
            if (text.Contains(".") && double.TryParse(text, out _))
                return 1;
            if (bool.TryParse(text, out _))
                return 2;

            return 3;
        }

        public void SyntaxAssert(bool check, string msgIfFalse)
        {
            if (!check)
                throw new InvalidSyntaxException(msgIfFalse);
        }
        


        public bool LexemesMatch(int offset = 0, params char[] lexemeTypes)
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

            return LexemesMatch(offset, convertedLexemeTypes);
        }

        public bool LexemesMatch(int offset, params Lexer.CharType[] lexemeTypes)
        {
            for (int i = 0; i < lexemeTypes.Length; i++)
                if (Lexemes[pos + offset + i].Type != lexemeTypes[i])
                    return false;

            return true;
        }
    }
}