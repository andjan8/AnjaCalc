using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnjaCalc.Controls
{
    public class LongPressButton : Button
    {
        private bool pressed;
        public event EventHandler Pressed;
        public event EventHandler Released;


        public LongPressButton()
        {
            Pressed += (sender, args) => { pressed = true; };
            Released += (sender, args) => { pressed = false; };
            System.Diagnostics.Debug.WriteLine("Created");
        }

        public virtual void OnPressed()
        {
            System.Diagnostics.Debug.WriteLine("Pressed");
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnReleased()
        {
            System.Diagnostics.Debug.WriteLine("REleaased");
            Released?.Invoke(this, EventArgs.Empty);
        }






    }
}
