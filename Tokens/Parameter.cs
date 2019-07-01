namespace TSL
{
    class Parameter : IToken
    {
        readonly string ParameterName;
        readonly Value DefaultValue;



        public Parameter(string parameterName, Value defaultValue)
        {
            ParameterName = parameterName;
            DefaultValue = defaultValue;
        }
    }
}
