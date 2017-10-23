namespace TSL
{
    public class Token
    {
        public readonly TokenType type;

        public Token(TokenType _type)
        {
            type = _type;
        }

        public enum TokenType
        {
            VariableDeclaration,
            FunctionDeclaration,
            Assignment,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            AdditionAssignment,
            SubtractionAssignment,
            MultiplicationAssignment,
            DivisionAssignment,
            Not,
            And,
            Or,
            ArrayAccess,
            Parameter,
            Call,
            String,
            Number,
            Bool
        }
    }
}