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
        private Window window;
        private Dictionary<string, Button> symbolToButtonMappings;

        public NumbersPage(Window window)
        {
            this.window = window;

            var symbolToButtonNameMappings = Enumerable.Range(0, 10)
                .Select(i => i.ToString()).Concat(new [] {"." })
                .ToDictionary(i => i, i => i);
            symbolToButtonNameMappings["-"] = "+/-";

            symbolToButtonMappings = symbolToButtonNameMappings
                .ToDictionary(mapping => mapping.Key,
                    mapping => window.Get<Button>(SearchCriteria.ByText(mapping.Value)));

            //var searchCriterias = buttonNames.Select(name => SearchCriteria.ByText(name));
            //buttons = searchCriterias.Select(criteria => window.Get<Button>(criteria)).ToArray();
        }
        // window.Get<Button>(SearchCriteria.ByText("1"))

        public void Parse(decimal number)
        {
            foreach (var c in number.ToString())
            {
                symbolToButtonMappings[c.ToString()].Click();
            }
        }

        //public void Parse(decimal number)
        //{
        //    foreach (var c in number.ToString())
        //    {
        //        var searchCriteria = SearchCriteria.ByText(c.ToString());
        //        var button = window.Get<Button>(searchCriteria);
        //        button.Click();
        //    }
        //}
    }
}