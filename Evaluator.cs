using System;
using System.Collections.Generic;
using System.Text;

namespace TSL
{
    public class Evaluator
    {
        public List<string> Variables = new List<string>();
        public List<string> Functions = new List<string>();

        Lexeme[] Lexemes { get; }
        int Index = 0;

        public Evaluator(Lexeme[] lexemes)
        {
            Lexemes = lexemes;
        }

        public string[] GetTokens()
        {
            List<string> tokens = new List<string>();

            while (Index < Lexemes.Length)
                tokens.Add(GetToken());

            return tokens.ToArray();
        }

        private string GetToken()
        {
            StringBuilder sb = new StringBuilder();

            string lexemeText = Lexemes[Index].Text;
            int offset = 0;

            switch (lexemeText)
            {
                case "var":
                    sb.Append("var ");
                    sb.Append(GetValue(1));

                    if (GetValue(2) == "=")
                    {
                        sb.Append(" " + GetVariableType(GetValue(3)));
                        sb.Append(" " + GetValue(3));
                        Index += 4;
                    }
                    else
                        Index += 2;

                    break;
                case "function":
                    sb.Append("func ");
                    sb.Append(GetValue(1));

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
                    }

                    break;
            }

            Index += offset;

            throw new NotImplementedException();
        }

        private string GetValue(int indexOffset)
        {
            return Lexemes[Index + indexOffset].Text;
        }

        public string GetVariableType (string text)
        {
            if (float.TryParse(text, out _))
                return "num";
            if (bool.TryParse(text, out _))
                return "bool";

            return "str";
        }
    }
}