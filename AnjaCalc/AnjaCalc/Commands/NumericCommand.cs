using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AnjaCalc.Commands
{
    public class NumericCommand<T> : ICommand  
    {

        public NumericCommand()
        {
            this.CanExecuteChanged += NumericCommand_CanExecuteChanged;
        }
        public void NumericCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            //command.ChangeCanExecute();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
        }
    }
}
