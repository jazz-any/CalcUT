using System.IO;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems.WindowItems;

namespace CalcUT
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void A()
        {
            var applicationDirectory = TestContext.CurrentContext.TestDirectory;
            var applicationPath = Path.Combine(@"D:\users\anastasiia.vasenko\Desktop\PracticalTask",
                "AvalonCalculator.exe");
            Application application = Application.Launch(applicationPath);
            using (Window window = application.GetWindow("WPF Calculator", InitializeOption.NoCache))
            {
                //window.Get<Button>(SearchCriteria.ByText("1")).Click();
                var page = new NumbersPage(window);
                page.Press(3);
            }
        }
    }
}
