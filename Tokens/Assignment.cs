namespace TSL
{
    class Assignment : IToken
    {
        readonly string VariableName;
        readonly Expression Value;



        public Assignment(string variableName, Expression value)
        {
            VariableName = variableName;
            Value = value;
        }
    }
}
