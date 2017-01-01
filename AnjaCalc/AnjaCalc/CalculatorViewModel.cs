using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Calculator;

namespace AnjaCalc
{
    public class CalculatorViewModel : ViewModelBase
    {
        string display = "";
        private int maxDisplayLength = 45;
        private bool answerShowing = false;
        private string[] operatorCharacters = new string[] { "+", "-", "/", "*" };
        public string Display
        {
            private set { SetProperty(ref display, value); }
            get { return display; } }
        private Calculator.Calculator _calculator;
        private Calculator.Calculator Calculator
        {
            get
            {
                if (_calculator == null)
                {
                    _calculator = new Calculator.Calculator();
                }
                return _calculator;
            }
        }

        public ICommand NumericCommand { get; private set; }
        public ICommand LeftParanthesisCommand { get; private set; }
        public ICommand RightParanthesisCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand ClearDisplayCommand { get; private set; }
        public ICommand PlusCommand { get; private set; }
        public ICommand MinusCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand CommaCommand { get; private set; }
        public ICommand DivisionCommand { get; private set; }
        public ICommand CalculateCommand { get; private set; }

        public CalculatorViewModel()
        {
            _calculator = new Calculator.Calculator();
            SetUpNumericCommand();
            SetUpParenthesisCommands();
            SetUpBackspaceCommand();
            SetUpClearDisplayCommand();
            SetUpPlusCommand();
            SetUpSubtractCommand();
            SetUpMultiplyCommand();
            SetUpCommaCommand();
            SetUpDivisionCommand();
            SetUpCalculateCommand();

        }

        private void SetUpCalculateCommand()
        {
            CalculateCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = true;
                                Calculator.Calculate();
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return Calculator.Expression.Length > 0; });
        }

        private void SetUpDivisionCommand()
        {
            DivisionCommand = new Command(
                execute: () =>
                {
                    answerShowing = false;
                    Calculator.Append(Operators.Divide);
                    Display = Calculator.Expression;
                    RefreshCanExecutes();
                },
                canExecute: () => { return Calculator.Expression.Length > 0 && !operatorCharacters.Contains(Calculator.Expression[Calculator.Expression.Length - 1].ToString()) ? CanAddToExpression() : false; });
        }

        private void SetUpCommaCommand()
        {
            CommaCommand = new Command(
                            execute: () =>
                            {
                                if (answerShowing)
                                {
                                    answerShowing = false;
                                    Calculator.Clear();
                                }

                                Calculator.Append(Operators.Comma);
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return Calculator.Expression.Length > 0 && !operatorCharacters.Contains(Calculator.Expression[Calculator.Expression.Length - 1].ToString()) ? CanAddToExpression() : false; });
        }

        private void SetUpMultiplyCommand()
        {
            MultiplyCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = false;
                                Calculator.Append(Operators.Multiply);
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return Calculator.Expression.Length > 0 && !operatorCharacters.Contains(Calculator.Expression[Calculator.Expression.Length - 1].ToString()) ? CanAddToExpression() : false; });
        }

        private void SetUpSubtractCommand()
        {
            MinusCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = false;
                                Calculator.Append(Operators.Subtract);
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return CanAddToExpression(); });
        }

        private void SetUpPlusCommand()
        {
            PlusCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = false;
                                Calculator.Append(Operators.Add);
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => {

                                return TempCanAdd();
                            });
        }

        public bool TempCanAdd()
        {
            return Calculator.Expression.Length > 0 && !operatorCharacters.Contains(Calculator.Expression[Calculator.Expression.Length - 1].ToString()) ? CanAddToExpression() : false;
        }

        private void SetUpClearDisplayCommand()
        {
            ClearDisplayCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = false;
                                Calculator.Clear();
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return Calculator.Expression.Length > 0; });
        }

        private void SetUpBackspaceCommand()
        {
            BackspaceCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = false;
                                Calculator.Erase();
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () => { return Calculator.Expression.Length > 0; });
        }

        private void SetUpParenthesisCommands()
        {
            LeftParanthesisCommand = new Command(
                execute: () => {
                    if (answerShowing)
                    {
                        answerShowing = false;
                        Calculator.Clear();
                        Display = Calculator.Expression;
                    }
                    Calculator.Append(Operators.LeftParanthesis);
                    Display = Calculator.Expression;
                },
                canExecute: () => {
                    return CanAddToExpression();
                });


            RightParanthesisCommand = new Command(
            execute: () => {
                if (answerShowing)
                {
                    answerShowing = false;
                    Calculator.Clear();
                    Display = Calculator.Expression;
                }
                Calculator.Append(Operators.RightParanthesis);
                Display = Calculator.Expression;
            },
            canExecute: () => {
                int numberOfOpeningParenthesis = Calculator.Expression.ToCharArray().Where(c => c == '(').Count();
                int numberOfClosingParenthesis = Calculator.Expression.ToCharArray().Where(c => c == ')').Count();
                return numberOfOpeningParenthesis > numberOfClosingParenthesis ? CanAddToExpression() : false;
            });
        }

        private void SetUpNumericCommand()
        {
            NumericCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    int numericValue = Convert.ToInt32(parameter);
                    if (answerShowing)
                    {
                        answerShowing = false;
                        Calculator.Clear();
                    }

                    Calculator.Append(numericValue);
                    Display = Calculator.Expression;
                    RefreshCanExecutes();
                    
                },
                canExecute: (string parameter) =>
                {
                    return CanAddToExpression();
                });
        }

        private bool CanAddToExpression()
        {
            return !(Calculator.Expression.Length >= maxDisplayLength);
        }

        void RefreshCanExecutes()
        {
            ((Command)NumericCommand).ChangeCanExecute();
            ((Command)LeftParanthesisCommand).ChangeCanExecute();
            ((Command)RightParanthesisCommand).ChangeCanExecute();
            ((Command)BackspaceCommand).ChangeCanExecute();
            ((Command)ClearDisplayCommand).ChangeCanExecute();
            ((Command)PlusCommand).ChangeCanExecute();
            ((Command)MinusCommand).ChangeCanExecute();
            ((Command)MultiplyCommand).ChangeCanExecute();
            ((Command)CommaCommand).ChangeCanExecute();
            ((Command)DivisionCommand).ChangeCanExecute();
            ((Command)CalculateCommand).ChangeCanExecute();
        }
        
        public void SaveState(IDictionary<string, object> dictionary)
        {
            dictionary["Display"] = Display;
        }

        public void RestoreState(IDictionary<string, object> dictionary)
        {
            Display = GetDictionaryEntry(dictionary, "Display", "");
            RefreshCanExecutes();
        }

        public T GetDictionaryEntry<T>(IDictionary<string, object> dictionary,
                                        string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
                return (T)dictionary[key];

            return defaultValue;
        }
    }
}
