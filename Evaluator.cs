using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TSL
{
    public class Evaluator
    {
        public List<string> Variables = new List<string>();
        public List<string> Functions = new List<string>();

        Lexeme[] Lexemes { get; set; }
        int Index = 0;

        public Evaluator(List<Lexeme> lexemes)
        {
            Lexemes = lexemes.ToArray();
        }

        public List<string> GetTokens()
        {
            StringBuilder sb = new StringBuilder();

            if (Lexemes[Index].Type != Lexer.CharType.Literal)
                throw new Exception(); // REDO

            string lexemeText = Lexemes[Index].Text;

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
                        int offset = 0;

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
                default:
                    break;
            }

            throw new NotImplementedException();

            string GetValue(int offset)
            {
                return Lexemes[Index + offset].Text;
            }
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