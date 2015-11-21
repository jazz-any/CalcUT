using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace CalcUT
{
    public class OperationsPage
    {
        private readonly Window window;
        private readonly Dictionary<string, Button> operationButtons;

        public OperationsPage(Window window)
        {
            this.window = window;

            string[] operations = { "*", "/", "-", "+", "=" };

            operationButtons = operations.ToDictionary(o => o, o => window.Get<Button>(SearchCriteria.ByText(o)));
        }

        public void SendNumber(decimal number)
        {
            foreach (var c in number.ToString())
            {
                operationButtons[c.ToString()].Click();
            }
        }
    }
}