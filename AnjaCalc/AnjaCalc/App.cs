using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AnjaCalc
{
    public class App : Application
    {
        CalculatorViewModel viewModel;
        public App()
        {
            viewModel = new CalculatorViewModel();
            viewModel.RestoreState(Current.Properties);
            MainPage = new CalculatorPage(viewModel);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            viewModel.SaveState(Current.Properties);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
