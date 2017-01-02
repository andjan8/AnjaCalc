using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AnjaCalc
{
    public partial class CalculatorPage : ContentPage
    {
        private enum Orientation
        {
            Landscape,
            Portrait
        }
        public CalculatorPage(CalculatorViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            var orientation = width > height ? Orientation.Landscape : Orientation.Portrait;
            SetUpPageDependingOnOrientation(orientation);

        }

        private void SetUpPageDependingOnOrientation(Orientation orientation)
        {
            int displayRowHeight = 150;
            int displayFontSize = 60;
            int buttonFontSize = 40;
            bool extraButtonsVisible = false;
            if (orientation == Orientation.Landscape)
            {
                displayRowHeight = 80;
                displayFontSize = 40;
                buttonFontSize = 25;
                extraButtonsVisible = true;
            }

            calculatorGrid.RowDefinitions[0].Height = displayRowHeight;
            displayLabel.FontSize = displayFontSize;
            foreach (Button b in calculatorGrid.Children.Where(c => c is Button).ToArray())
            {
                b.FontSize = buttonFontSize;
                b.IsVisible = (int)b.GetValue(Grid.ColumnProperty) > 3 ? extraButtonsVisible : true;
            }

        }
    }
}
