namespace TSL
{
    class VariableDeclaration : Token
    {
        string Name;

        public VariableDeclaration(string name)
        {
            Name = name;
        }
    }
}
