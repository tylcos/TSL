using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TSL
{
    public record Lexeme(string Text, CharType Type);


    public class Lexer
    {
        public List<Lexeme> Lexemes { get; private set; }

        private readonly char[] program;
        private int pos;


        public Lexer(string programString)
        {
            program = programString.ToCharArray();
            pos = 0;
        }


        /// <summary>
        /// Returns all parsable lexemes from the program
        /// </summary>
        /// <returns>List of parsed lexemes</returns>
        public List<Lexeme> GetLexemes()
        {
            Lexemes = new List<Lexeme>();


            while (CharsRemaining())
            {
                Lexeme nextLexeme = GetNextLexeme();

                if (nextLexeme != null)
                    Lexemes.Add(nextLexeme);
            }


            return Lexemes;
        }

        /// <summary>
        /// Returns the next parsable lexeme if it's available
        /// </summary>
        /// <returns>Next lexeme or null if there are no more characters</returns>
        private Lexeme GetNextLexeme()
        {
            var text = new StringBuilder(16);
            int flags = 0; // Bit 0: in quotes, Bit 1: in single line comment, Bit 2: in multi line comment


            // Each iteration will have access to the current character, current type, next type
            CharType currentType = GetTypeOfChar(program[pos]);
            CharType nextType = currentType;
            while (CharsRemaining())
            {
                char c = program[pos++];
                currentType = nextType; // Prevents classifying character twice
                if (CharsRemaining())
                    nextType = GetTypeOfChar(program[pos]);


                // Condition checking for dealing with strings and comments, order of if statments matters severely
                // Might want to convert to a DFA as any normal program character goes through a bunch of if statments unnecessarily 
                if ((flags & 0b001) != 0 && c != '"') // Within string
                {
                    text.Append(c);
                    continue;
                }
                else if (currentType == CharType.Comment) // Start of single comment or start or end of multi line comment
                { 
                    flags = nextType == CharType.Comment ? flags ^ 0b100 : flags | 0b010;
                    continue;
                }
                else if ((flags & 0b010) != 0 && currentType == CharType.Newline) // In single line comment and new line reached
                {
                    flags ^= 0b010;
                    continue;
                }
                else if ((flags & 0b110) != 0) // Within comment
                {
                    continue;
                }
                else if (c == '"') // Start or end of string
                {
                    text.Append(c);
                    flags ^= 0b001;
                }
                else if (currentType == CharType.Whitespace) // Whitespace
                {
                    continue;
                }
                else // Normal program
                {
                    text.Append(c);
                }


                // Only continue if currently parsing a string or comment, the next char is of the same type as the current, and stop if an accessor was reached 
                if (flags != 0 || (currentType == nextType && currentType != CharType.Accessor))
                    continue;

                break;
            }


            if ((flags & 0b001) != 0) // Unfinished string
                throw new NotImplementedException();
            if ((flags & 0b100) != 0) // Unfinished multi line comment
                throw new NotImplementedException();
            if (currentType == CharType.Whitespace)
                return null;

            return new Lexeme(text.ToString(), currentType);
        }

        private bool CharsRemaining() => pos < program.Length;



        // Maps each character to a CharType
        // Accessor   : .(){}[]
        // Literal    : abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_
        // Whitespace :  \r
        // Operator   : =+-*/\&|!^%<>,
        // Comment    : #
        // Newline    : \n

        // Pretty print
        // for (int i = 0; i < 128; i++) Console.WriteLine($"{i, 3}   {(char)i}   {(CharType)TypeTable[i]}");
        private static readonly byte[] TypeTable = new byte[] { 0, 0, 0,  0, 0, 0, 0, 0, 0, 0, 32, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                                                4, 8, 2, 16, 0, 8, 8, 2, 1, 1,  8, 8, 8, 8, 1, 8, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 8, 8, 8, 8, 
                                                                0, 2, 2,  2, 2, 2, 2, 2, 2, 2,  2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 8, 1, 8, 2, 
                                                                0, 2, 2,  2, 2, 2, 2, 2, 2, 2,  2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 8, 1, 8, 0 
        };

        public static CharType GetTypeOfChar(char c) => c > 127 ? CharType.Literal : (CharType)TypeTable[c];
    }


    public enum CharType : byte
    {
        Invalid    = 0,
        Accessor   = 1,
        Literal    = 2,
        Whitespace = 4,
        Operator   = 8,
        Comment    = 16,
        Newline    = 32
    }
}