using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSL
{
    class Runtime
    {
        static Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
        static Dictionary<string, string[]> functions = new Dictionary<string, string[]>();
    }

    public class Variable
    {

    }

    public class Str : Variable
    {

    }

    public class Number : Variable
    {

    }

    public class Bool : Variable
    {

    }

    public class StrArray : Variable
    {

    }

    public class NumberArray : Variable
    {

    }

    public class BoolArray : Variable
    {

    }
}
