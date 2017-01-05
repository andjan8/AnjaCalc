using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AnjaCalc
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(CalculatorViewModel calculatorViewModel)
        {
            InitializeComponent();
            this.BindingContext = new HistoryViewModel(calculatorViewModel, Return );
            
        }

        private async void Return()
        {
            await Navigation.PopAsync();
        }
    }

    public class HistoryViewModel : ViewModelBase
    {
        private CalculatorViewModel calculatorViewModel;
        List<ExpressionViewModel> historicalExpressions;
        private Action returnToCalculatorView;

        public List<ExpressionViewModel> HistoricalExpressions
        {
            private set { SetProperty(ref historicalExpressions, value); }
            get { return historicalExpressions; }
        }

        

        public HistoryViewModel(CalculatorViewModel calculatorViewModel, Action returnToCalculatorView)
        {
            this.returnToCalculatorView = returnToCalculatorView;
            this.calculatorViewModel = calculatorViewModel;
            this.HistoricalExpressions = calculatorViewModel.GetHistory().ToArray().Select(e => new ExpressionViewModel(e , calculatorViewModel, this, returnToCalculatorView)).ToList();
            
        }

        internal void DeleteExpression(ExpressionViewModel expressionViewModel)
        {
            calculatorViewModel.DeleteExpression(expressionViewModel.e);
            this.HistoricalExpressions = calculatorViewModel.GetHistory().ToArray().Select(e => new ExpressionViewModel(e, calculatorViewModel, this, returnToCalculatorView)).ToList();
        }

        
    }

    public class ExpressionViewModel : ViewModelBase
    {
        private CalculatorViewModel calculatorViewModel;
        private HistoryViewModel historyViewModel;
        public Expression e;
        private Action close;

        public string FullExpression { get { return e.FullExpression; } }

        public ExpressionViewModel(Expression e, CalculatorViewModel calculatorViewModel, HistoryViewModel historyViewModel, Action close)
        {
            this.e = e;
            this.historyViewModel = historyViewModel;
            this.calculatorViewModel = calculatorViewModel;
            this.close = close;
        }

        public ICommand UseExpressionCommand
        {
            get
            {
                return new Command(
                    execute: () => { calculatorViewModel.SetExpression(this.e);
                        close();
                    },
                    canExecute: () => { return true; });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command(
                    execute: () => { historyViewModel.DeleteExpression(this); },
                    canExecute: () =>
                    {
                        return true;
                    });

            }
        }
    }
}
