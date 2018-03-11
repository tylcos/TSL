namespace TSL
{
    class Assignment : Token
    {
        string VariableName;
        Expression Value;

        public Assignment(string variableName, Expression value)
        {
            VariableName = variableName;
            Value = value;
        }
    }
}
