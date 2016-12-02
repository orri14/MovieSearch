using System;
using DM.MovieApi;


namespace MovieSearch
{
    public class MyDbSettings : IMovieDbSettings
    {
        public MyDbSettings()
        {
        }

        public string ApiKey
        {
            get
            {
                string key = "214da67793e3bbe4c504e678b40e82aa";
                return key;

                throw new NotImplementedException();
            }
        }

        public string ApiUrl
        {
            get
            {
                string url = "http://api.themoviedb.org/3/";
                return url;

                throw new NotImplementedException();
            }
        }
    }
}
