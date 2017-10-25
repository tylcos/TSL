using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSL
{
    class Runtime
    {
        static Dictionary<string, Literal> variables = new Dictionary<string, Literal>();
        static Dictionary<string, IToken[]> functions = new Dictionary<string, IToken[]>();
    }
}
