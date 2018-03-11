using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSL
{
    class Runtime
    {
        public readonly Dictionary<string, double> GlobalVariables = new Dictionary<string, double>();
        public List<string> GlobalFunctions = new List<string>();



        public void Run()
        {
        }
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

    public enum VarType
    {
        String,
        Number,
        Bool
    }
}
