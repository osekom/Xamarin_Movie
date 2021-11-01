using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Movie.Class
{
    public class RestClient
    {
        public static HttpClient client = null;
        public static Uri EndPoint_GetTopMovies = new Uri($"{Constantes.URL_API}/top_rated?api_key={Constantes.API_KEY}&language=en-US&page=1");
        public static Uri EndPoint_GetUpMovies = new Uri($"{Constantes.URL_API}/upcoming?api_key={Constantes.API_KEY}&language=en-US&page=1");
        public static Uri EndPoint_GetPopularMovies = new Uri($"{Constantes.URL_API}/popular?api_key={Constantes.API_KEY}&language=en-US&page=1");

        public RestClient()
        {
            if (client == null)
            {
                client = new HttpClient();
            }
        }

        //getVersion
        public async Task<T> Getmethod<T>(Uri URL)
        {
            if (Constantes.IsConnect)
            {
                try
                {
                    var response = await client.GetAsync(URL);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (response.Content != null)
                        {
                            var jsonstring = await response.Content.ReadAsStringAsync();
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstring);
                        }
                        else
                        {
                            return default(T);
                        }
                    }
                    else
                    {
                        return default(T);
                    }
                }
                catch (Exception e)
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

    }
}
