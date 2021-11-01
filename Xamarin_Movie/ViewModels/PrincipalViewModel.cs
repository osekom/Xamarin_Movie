using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin_Movie.Class;
using Xamarin_Movie.Models;
using Xamarin_Movie.views;

namespace Xamarin_Movie.ViewModels
{
    public class PrincipalViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Movies> _TopRateMovies;
        public ObservableCollection<Movies> TopRateMovies
        {
            get { return _TopRateMovies; }
            set { Set(ref _TopRateMovies, value); }
        }

        private ObservableCollection<Movies> _UpComingMovies;
        public ObservableCollection<Movies> UpComingMovies
        {
            get { return _UpComingMovies; }
            set { Set(ref _UpComingMovies, value); }
        }

        private ObservableCollection<Movies> _PopularMovies;
        public ObservableCollection<Movies> PopularMovies
        {
            get { return _PopularMovies; }
            set { Set(ref _PopularMovies, value); }
        }

        private string _QuerySearch;
        public string QuerySearch
        {
            get { return _QuerySearch; }
            set
            {
                if (Set(ref _QuerySearch, value))
                    if (_QuerySearch.Length >= 3 || _QuerySearch.Length == 0)
                        FilterCommand.Execute(_QuerySearch);

            }
        }

        private Command<string> _FilterCommand;
        public Command<string> FilterCommand
        {
            get
            {
                return _FilterCommand = _FilterCommand ?? new Command<string>((query) =>
                {
                    if (query.Length >= 3)
                    {
                        TopRateMovies = new ObservableCollection<Movies>(
                            TopList.Where(x => x.title.ToLower().Contains(query.ToLower()))
                            );

                        UpComingMovies = new ObservableCollection<Movies>(
                            UpList.Where(x => x.title.ToLower().Contains(query.ToLower()))
                            );

                        PopularMovies = new ObservableCollection<Movies>(
                            PopularList.Where(x => x.title.ToLower().Contains(query.ToLower()))
                            );
                    }
                    else
                    {
                        TopRateMovies = new ObservableCollection<Movies>(TopList);
                        UpComingMovies = new ObservableCollection<Movies>(UpList);
                        PopularMovies = new ObservableCollection<Movies>(PopularList);
                    }
                });
            }
        }

        private Command<Movies> _SelectedItemCommand;
        public Command<Movies> SelectedItemCommand
        {
            get
            {
                return _SelectedItemCommand = _SelectedItemCommand ?? new Command<Movies>((_movie) =>
                {
                    if (_movie != null)
                    {
                        App.Current.MainPage.Navigation.PushAsync(new ShowMovie(_movie.id));
                    }
                });
            }
        }

        private ObservableCollection<Movies> TopList;
        private ObservableCollection<Movies> UpList;
        private ObservableCollection<Movies> PopularList;

        public PrincipalViewModel()
        {
            IsBusy = true;

            // ejecutamos un hilo para traer los datos del hilo principal de la vista y agregamos el path correspodiente al URL de la imagen.
            Device.BeginInvokeOnMainThread( async() =>
            {
                RestClient client = new RestClient();
                var top_movies = await client.Getmethod<ResponseMovies>(RestClient.EndPoint_GetTopMovies);
                if (top_movies != null && string.IsNullOrEmpty(top_movies.status_message))
                {
                    TopList = new ObservableCollection<Movies>(top_movies.results);
                    TopList.Where(w => !string.IsNullOrEmpty(w.poster_path)).ToList().ForEach(s => s.poster_path = Constantes.URL_IMAGE+s.poster_path);
                    TopRateMovies = new ObservableCollection<Movies>(TopList);
                }

                var up_movies = await client.Getmethod<ResponseMovies>(RestClient.EndPoint_GetUpMovies);
                if (up_movies != null && string.IsNullOrEmpty(up_movies.status_message))
                {
                    UpList = new ObservableCollection<Movies>(up_movies.results);
                    UpList.Where(w => !string.IsNullOrEmpty(w.poster_path)).ToList().ForEach(s => s.poster_path = Constantes.URL_IMAGE + s.poster_path);
                    UpComingMovies = new ObservableCollection<Movies>(UpList);
                }

                var popular_movies = await client.Getmethod<ResponseMovies>(RestClient.EndPoint_GetPopularMovies);
                if (popular_movies != null && string.IsNullOrEmpty(popular_movies.status_message))
                {
                    PopularList = new ObservableCollection<Movies>(popular_movies.results);
                    PopularList.Where(w => !string.IsNullOrEmpty(w.poster_path)).ToList().ForEach(s => s.poster_path = Constantes.URL_IMAGE + s.poster_path);
                    PopularMovies = new ObservableCollection<Movies>(PopularList);
                }
            });

            IsBusy = false;
        }
    }
}
