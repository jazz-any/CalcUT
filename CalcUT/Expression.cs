using System.Collections.Generic;
using System.Linq;

namespace CalcUT
{
    public abstract class Expression
    {
        public static IEnumerable<Expression> Parse(string expressionString)
        {
            const char divisionOperator = '/';
            var numbers = expressionString.Split(divisionOperator).Select(number => decimal.Parse(number)).ToList();
            var expressions = new List<Expression> {new Number {Value = numbers.First()}};
            foreach (var number in numbers.Skip(1))
            {
                expressions.Add(new DivisionOperation());
                expressions.Add(new Number {Value = number});
            }

            return expressions;
        }

        public abstract class Operation : Expression
        {
            public abstract string Operator { get; }
        }

        public class DivisionOperation : Operation
        {
            public override string Operator
            {
                get { return "/"; }
            }
        }

        public class Number : Expression
        {
            public decimal Value { get; set; }
        }
    }
}