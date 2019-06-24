namespace TSL
{
    class Value
    {
        public dynamic value;

        public Value(string _value)
        {
            if      (bool.TryParse(_value, out bool returnValueB))
                value = returnValueB;
            else if (double.TryParse(_value, out double returnValueD))
                value = returnValueD;
        }
    }
}
