using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin_Movie.Class;
using Xamarin_Movie.Models;

namespace Xamarin_Movie.ViewModels
{
    public class ShowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { Set(ref _IsBusy, value); }
        }

        private DetailMovie _DetailMovie;
        public DetailMovie DetailMovie
        {
            get { return _DetailMovie; }
            set { Set(ref _DetailMovie, value); }
        }

        public ShowViewModel(int MovieID)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                RestClient client = new RestClient();
                var MovieDetail = await client.Getmethod<DetailMovie>(new Uri($"{Constantes.URL_API}/{MovieID.ToString()}?api_key={Constantes.API_KEY}&language=en-US"));
                if (MovieDetail != null && string.IsNullOrEmpty(MovieDetail.status_message))
                {
                    string Generos = "";
                    MovieDetail.backdrop_path = Constantes.URL_IMAGE + MovieDetail.backdrop_path;
                    MovieDetail.production_companies.ForEach(pc => pc.logo_path = Constantes.URL_IMAGE + pc.logo_path);
                    MovieDetail.genres.ForEach(pc => Generos = Generos + pc.name + " ");
                    MovieDetail.gender = Generos;
                    if (MovieDetail.production_companies.Count > 0)
                    {
                        MovieDetail.production = MovieDetail.production_companies[0].name;
                    }
                    DetailMovie = MovieDetail;
                }

            });
            IsBusy = false;

        }
    }
}
