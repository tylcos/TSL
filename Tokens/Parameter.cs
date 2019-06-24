namespace TSL
{
    class Parameter : Token
    {
        string ParameterName;
        Value DefaultValue;

        public Parameter(string parameterName, Value defaultValue)
        {
            ParameterName = parameterName;
            DefaultValue = defaultValue;
        }
    }
}
