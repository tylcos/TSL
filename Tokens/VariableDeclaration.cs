namespace TSL
{
    class VariableDeclaration : IToken
    {
        readonly string Name;



        public VariableDeclaration(string name)
        {
            Name = name;
        }
    }
}
