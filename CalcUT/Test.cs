using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
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
            var applicationPath = Path.Combine(applicationDirectory, "AvalonCalculator.exe");
            Application application = Application.Launch(applicationPath);
            window = application.GetWindow("WPF Calculator", InitializeOption.NoCache);

            numbersPage = new NumbersPage(window);
            operationsPage = new OperationsPage(window);
            resultPage = new ResultPage(window);

        }

        [TearDown]
        public void CloseWindow()
        {
            window.Dispose();
        }

        public static IEnumerable<XElement> DivisionTestCases = ExternalDataHelper.GetTestCaseData();

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
}
