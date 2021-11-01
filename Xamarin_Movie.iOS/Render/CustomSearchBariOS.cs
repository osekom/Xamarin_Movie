using Foundation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin_Movie.iOS.Render;
using Xamarin_Movie.Render;

[assembly: ExportRenderer(typeof(CustomSearchBar), typeof(CustomSearchBariOS))]
namespace Xamarin_Movie.iOS.Render
{
    public class CustomSearchBariOS : SearchBarRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SearchBar.TextProperty.PropertyName)
                Control.Text = Element.Text;

            if (e.PropertyName != SearchBar.CancelButtonColorProperty.PropertyName && e.PropertyName != SearchBar.TextProperty.PropertyName)
                base.OnElementPropertyChanged(sender, e);

        }
    }
}