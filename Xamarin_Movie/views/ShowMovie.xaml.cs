using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Movie.ViewModels;

namespace Xamarin_Movie.views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowMovie : ContentPage
    {
        public ShowMovie(int MovieID)
        {
            InitializeComponent();
            this.BindingContext = new ShowViewModel(MovieID);
        }
    }
}