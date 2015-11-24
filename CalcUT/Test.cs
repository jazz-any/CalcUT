using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Xml.Linq;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;  
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace CalcUT
{
    [TestFixture]
    public class Test
    {

        private NumbersPage numbersPage;
        private OperationsPage operationsPage;
        private ResultPage resultPage;
        private Window window;


        [SetUp]
        public void SetUp()
        {
            var applicationDirectory = TestContext.CurrentContext.TestDirectory;
            var applicationPath = Path.Combine(@"D:\users\anastasiia.vasenko\Desktop\PracticalTask",
                "AvalonCalculator.exe");
            Application application = Application.Launch(applicationPath);
            window = application.GetWindow("WPF Calculator", InitializeOption.NoCache);

            //window.Get<Button>(SearchCriteria.ByText("1")).Click();
            numbersPage = new NumbersPage(window);
            operationsPage = new OperationsPage(window);
            resultPage = new ResultPage(window);

        }

        [TearDown]
        public void CloseWindow()
        {
            window.Dispose();
        }

        public static IEnumerable<XElement> DivisionTestCases = ExternalDataHelper.Parser();

        [Test]
        [TestCaseSource("DivisionTestCases")]
        public void TestCase(XElement testDataElement)
        {
            var testCaseInput = testDataElement.Element("Input").Value;
            var expressions = Expression.Parse(testCaseInput);
            var testCaseResult = testDataElement.Element("Result")?.Value;
            var testCaseError = testDataElement.Element("Error")?.Value;

            foreach (var expression in expressions)
            {
                if (expression is Expression.Number)
                {
                    var numberExpression = (Expression.Number) expression;
                    numbersPage.SendNumber(numberExpression.Value);
                }
                else if (expression is Expression.Operation)
                {
                    var operation = (Expression.Operation) expression;
                    operationsPage.SendOperation(operation.Operator);
                }
            }

            var result = resultPage.GetResult();
            if (testCaseResult != null)
            {
                Assert.That(result.IsSuccessful, Is.True);
                Assert.That(result.Value, Is.EqualTo(testCaseResult));
            }

            if (testCaseError != null)
            {
                Assert.That(result.IsSuccessful, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo(testCaseError));
            }
        }
    }

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

        //public static class ExpressionParser
        //{


        //    public static Expression Parse(string expressionString)
        //    {

        //    }
        //}

        //public abstract class Expression
        //{
        //    public abstract decimal Value { get; }        
        //}

        //public class Number : Expression
        //{
        //    public static Regex NumberRegex = new Regex("(\\d+)");

        //    public Number(decimal value)
        //    {
        //        Value = value;
        //    }

        //    public override decimal Value { get; }
        //}

        //public abstract class BinaryOperation : Expression
        //{
        //    public abstract Expression Left { get; }
        //    public abstract Expression Right { get; }
        //}

        //public class DivisionOperation : BinaryOperation
        //{
        //    public DivisionOperation(Expression left, Expression right)
        //    {
        //        Left = left;
        //        Right = right;
        //    }

        //    public override decimal Value
        //    {
        //        get { return Left.Value/Right.Value; }
        //    }

        //    public override Expression Left { get; }
        //    public override Expression Right { get; }
        //}
    }
}
