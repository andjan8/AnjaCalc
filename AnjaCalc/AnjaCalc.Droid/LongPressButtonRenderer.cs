using Xamarin.Forms;
using AnjaCalc.Droid;
using Xamarin.Forms.Platform.Android;
using AnjaCalc.Controls;
using Android.Views;
using System;
using AnjaCalc.Droid.Renderer;

[assembly: ExportRenderer(typeof(Button), typeof(LongPressButtonRenderer))]
namespace AnjaCalc.Droid.Renderer
{
    public class LongPressButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var customButton = e.NewElement as LongPressButton;

            var thisButton = Control as Android.Widget.Button;
            thisButton.Touch += (object sender, TouchEventArgs args) =>
            {
                if (args.Event.Action == MotionEventActions.Down)
                {
                    customButton.OnPressed();
                }
                else if (args.Event.Action == MotionEventActions.Up)
                {
                    customButton.OnReleased();
                }
            };
        }
    }
}
