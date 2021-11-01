using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Movie.Models
{
    public class Movies
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
    }

    public class ResponseMovies
    {
        public int page { get; set; }
        public List<Movies> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public string status_message { get; set; }
        public bool success { get; set; }
        public int status_code { get; set; }
    }
}
