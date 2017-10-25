using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSL
{
    public interface IToken
    {

    }

    public class Conditionals : IToken
    {

    }

    public class Syntax : IToken
    {

    }

    public class Assignment : IToken
    {
        public string VariableName;
        public Literal Value;
    }

    public class Operator : IToken
    {

    }

    public class ComparisonOperator : Operator
    {

    }

    public class Literal : IToken
    {

    }



    class If : Conditionals
    {
        ComparisonOperator Comparison;

    }

    class IfElse : Conditionals
    {

    }

    class For : Conditionals
    {

    }

    class While : Conditionals
    {

    }

    class VariableDeclaration : Syntax
    {
        public readonly string VariableName;

        public VariableDeclaration(string variableName)
        {
            VariableName = variableName;
        }
    }

    class FunctionDeclaration : Syntax
    {
        string FunctionName;
        Parameter[] Parameters;
        IToken[] FunctionBody;

        public FunctionDeclaration(string functionName, Parameter[] parameters, IToken[] functionBody)
        {
            FunctionName = functionName;
            Parameters = parameters;
            FunctionBody = functionBody;
        }
    }

    class ArrayAccess : Syntax
    {

    }

    class Parameter : Syntax
    {
        Literal Value;

        public Parameter(Literal value)
        {
            Value = value;
        }
    }

    class Get : Syntax
    {

    }

    class Call : Syntax
    {

    }

    class VariableAssignment : Assignment
    {
        public string VariableName;
        public Literal Value;

        public VariableAssignment(string variableName, Literal value)
        {
            VariableName = variableName;
            Value = value;
        }
    }

    class AdditionAssignment : Assignment
    {

    }
    class SubtractionAssignment : Assignment
    {

    }
    class MultiplicationAssignment : Assignment
    {

    }
    class DivisionAssignment : Assignment
    {

    }
    class Ternary : Operator
    {

    }
    class Addition : Operator
    {

    }
    class Subtraction : Operator
    {

    }
    class Multiplication : Operator
    {

    }
    class Division : Operator
    {

    }
    
    class Not : Operator
    {

    }
    class And : Operator
    {

    }
    class Or : Operator
    {

    }

    class Equality : ComparisonOperator
    {

    }

    class Inequality : ComparisonOperator
    {

    }

    class GreaterThan : ComparisonOperator
    {

    }

    class LessThan : ComparisonOperator
    {

    }

    class GreaterThanEqual : ComparisonOperator
    {

    }

    class LessThanEqual : ComparisonOperator
    {

    }

    class String : Literal
    {
        string Value;
    }
    class Number : Literal
    {
        decimal Value;
    }
    class Bool : Literal
    {
        bool Value;
    }
}
