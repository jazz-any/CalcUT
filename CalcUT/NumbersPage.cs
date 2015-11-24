using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace CalcUT
{
    public class NumbersPage
    {
        private readonly Dictionary<string, Button> symbolButtons;

        public NumbersPage(Window window)
        {
            var symbolToButtonNameMappings = Enumerable.Range(0, 10)
                .Select(i => i.ToString()).Concat(new [] {"." })
                .ToDictionary(i => i, i => i);
            symbolToButtonNameMappings["-"] = "+/-";

            symbolButtons = symbolToButtonNameMappings
                .ToDictionary(mapping => mapping.Key,
                    mapping => window.Get<Button>(SearchCriteria.ByText(mapping.Value)));
        }
            
        public void SendNumber(decimal number)
        {
            var absoluteNumber = Math.Abs(number);

            foreach (var c in absoluteNumber.ToString())
            {
                symbolButtons[c.ToString()].Click();
            }

            if (number < 0)
            {
                symbolButtons["-"].Click();
            }
        }
    }
}