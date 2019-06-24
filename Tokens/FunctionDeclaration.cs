namespace TSL
{
    class FunctionDeclaration : Token
    {
        string FunctionName;
        Parameter[] Parameters;

        public FunctionDeclaration(string functionName, params Parameter[] parameters)
        {
            FunctionName = functionName;
            Parameters = parameters;
        }
    }
}
