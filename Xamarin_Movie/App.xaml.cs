using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Movie.Class;
using Xamarin_Movie.views;
using Xamarin_Movie.Render;

namespace Xamarin_Movie
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new NetworkState();

            MainPage = new CustomNavigationPage(new Principal());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
