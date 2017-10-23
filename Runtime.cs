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
        static Dictionary<string, Token[]> functions = new Dictionary<string, Token[]>();
    }
}
