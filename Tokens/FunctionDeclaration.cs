namespace TSL
{
    class FunctionDeclaration : IToken
    {
        readonly string FunctionName;
        readonly Parameter[] Parameters;



        public FunctionDeclaration(string functionName, params Parameter[] parameters)
        {
            FunctionName = functionName;
            Parameters = parameters;
        }
    }
}
