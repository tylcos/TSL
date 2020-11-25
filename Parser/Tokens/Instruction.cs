using System.Collections.Generic;

public class Instruction
{
    public string text;

    public Instruction (string name)
    {
        text = name;
    }
}

public class Parameter
{
    public string text;

    public Parameter (string name)
    {
        text = name;
    }

    enum ParameterType
    {
        Replace,
        Add,
        Check,
        Test,
        Continuous
    }

}