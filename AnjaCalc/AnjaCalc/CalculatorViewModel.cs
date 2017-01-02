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
            get { return display; }
        }
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
        private Command<Operators> operatorCommand;
        private Command<string> numericCommand;
        private Command deleteCommand;
        private Command calculateCommand;

        public ICommand NumericCommand
        {
            get
            {
                if (numericCommand == null)
                {
                    numericCommand = new Command<string>(
                 execute: (string parameter) =>
                 {
                     int numberPressed = Convert.ToInt32(parameter);
                     Append(numberPressed);
                 },
                  canExecute: (string parameter) =>
                  {
                      return CanAddToExpression();
                  });
                }
                return numericCommand;
            }
        }
        public ICommand OperatorCommand
        {
            get
            {
                if (operatorCommand == null)
                {
                    operatorCommand = new Command<Operators>(
                        execute: (Operators _operator) =>
                        {
                            Append(_operator);
                        },
                        canExecute: (Operators parameter) =>
                        {
                            return CanExecute(parameter);
                        });
                }
                return operatorCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new Command(
                        execute: () =>
                        {
                            answerShowing = false;
                            Calculator.Erase();
                            Display = Calculator.Expression;
                            RefreshCanExecutes();
                        },
                        canExecute: () =>
                        {
                            return Calculator.Expression.Length > 0;
                        });
                }
                return deleteCommand;
            }
        }
        public ICommand CalculateCommand
        {
            get
            {
                if (calculateCommand == null)
                {
                    calculateCommand = new Command(
                            execute: () =>
                            {
                                answerShowing = true;
                                Calculator.Calculate();
                                Display = Calculator.Expression;
                                RefreshCanExecutes();
                            },
                            canExecute: () =>
                            {
                                return Calculator.Expression.Length > 0;
                            });
                }
                return calculateCommand;
            }
        }

        public CalculatorViewModel()
        {
            _calculator = new Calculator.Calculator();
        }
        
        private bool CanExecute(Operators parameter)
        {
            if(parameter == Operators.Unknown)
            { return false; }
            else if (parameter != Operators.RightParanthesis)
            {
                return Calculator.Expression.Length > 0 && !operatorCharacters.Contains(Calculator.Expression[Calculator.Expression.Length - 1].ToString()) ? CanAddToExpression() : false;
            }
            else if (parameter == Operators.RightParanthesis)
            {
                int numberOfOpeningParenthesis = Calculator.Expression.ToCharArray().Where(c => c == '(').Count();
                int numberOfClosingParenthesis = Calculator.Expression.ToCharArray().Where(c => c == ')').Count();
                return numberOfOpeningParenthesis > numberOfClosingParenthesis ? CanAddToExpression() : false;
            }
            else
            {
                return false;
            }
        }

        public void RefreshCanExecutes()
        {
            ((Command)NumericCommand).ChangeCanExecute();
            ((Command)OperatorCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
            ((Command)CalculateCommand).ChangeCanExecute();
        }

        private bool CanAddToExpression()
        {
            return !(Calculator.Expression.Length > this.maxDisplayLength);
        }

        private void Append(Operators _operator)
        {
            answerShowing = false;
            Calculator.Append(_operator);
            Display = Calculator.Expression;
            RefreshCanExecutes();
        }

        private void Append(int numberPressed)
        {
            if (answerShowing)
            {
                answerShowing = false;
                Calculator.Clear();
            }
            Calculator.Append(numberPressed);
            this.Display = Calculator.Expression;
            RefreshCanExecutes();
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

