using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;

namespace CalcUT
{
    public class ResultPage
    {
        private readonly Window window;
        public Button resultButton;
        public TextBox resultField;
        public TextBox resultScreen;
        public SearchCriteria errorMessage;
        public SearchCriteria okButton;

        public ResultPage(Window window)
        {
            this.window = window;

            var edits = window.GetMultiple(SearchCriteria.ByControlType(ControlType.Edit)).Cast<TextBox>().ToArray();
            resultField = edits.First(edit => edit.Text == "0");
            resultScreen = edits.First(edit => edit != resultField);
            resultButton = window.Get<Button>(SearchCriteria.ByText("="));
            errorMessage = SearchCriteria.ByText("Operation cannot be perfomed");
            okButton = SearchCriteria.ByText("OK");
        }

        public Result GetResult()
        {
            resultButton.Click();
            var modalWindow = window.ModalWindows().FirstOrDefault();
            if (modalWindow != null)
            {
                var errorMessage = modalWindow.Get<Label>().Text;
                modalWindow.Get<Button>(okButton).Click();
                return new Result {IsSuccessful = false, ErrorMessage = errorMessage};
            }
            var result = resultField.Text;

            return new Result {IsSuccessful = true, Value = result};
        }
    }

    public class Result
    {
        public bool IsSuccessful { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }
    }
}