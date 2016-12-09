using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;

namespace MovieSearch
{
    public class ApiService
    {
        private IApiMovieRequest _movieApi;

        public ApiService()
        {
            MovieDbFactory.RegisterSettings(new MyDbSettings());
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        public async Task<List<FilmInfo>> getMoviesByTitle(string title)
        {
            ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(title);
            return await formatResponse(response);

        }

        public async Task<List<FilmInfo>> getTopRatedMovies()
        {
            ApiSearchResponse<MovieInfo> response = await _movieApi.GetTopRatedAsync();
            return await formatResponse(response);
        }

        private async Task<List<FilmInfo>> formatResponse(ApiSearchResponse<MovieInfo> response)
        {
            List<FilmInfo> result = new List<FilmInfo>();

            try
            {
                if (response.Results != null)
                {
                    foreach (MovieInfo info in response.Results)
                    {
                        FilmInfo film = new FilmInfo();
                        film.title = info.Title;
                        film.year = info.ReleaseDate.Year.ToString();
                        film.rating = info.VoteAverage.ToString().Equals("0") ? "-" : info.VoteAverage.ToString();
                        film.description = info.Overview;

                        film.imageName = info.PosterPath;

                        List<string> genres = new List<string>();


                        if (info.Genres != null)
                        {
                            foreach (var genre in info.Genres)
                            {
                                if (genre != null)
                                {
                                    genres.Add(genre.Name);
                                }
                            }
                        }

                        film.genres = genres;

                        //Get the movie duration
                        ApiQueryResponse<Movie> movie = await _movieApi.FindByIdAsync(info.Id);

                        if (movie != null && movie.Item != null)
                        {
                            film.duration = "";
                            if (movie.Item.Runtime != null)
                            {
                                film.duration = movie.Item.Runtime.ToString();
                            }
                        }

                        ApiQueryResponse<MovieCredit> credits = null;

                        //Get the cast of a movie
                        try
                        {
                            credits = await _movieApi.GetCreditsAsync(info.Id);

                        }
                        catch (Exception)
                        {
                           
                        }
                        
                        List<string> cast = new List<string>();

                        if (credits != null && credits.Item != null)
                        {
                            if (credits.Item.CastMembers != null)
                            {
                                foreach (var actor in credits.Item.CastMembers)
                                {
                                    cast.Add(actor.Name);
                                }
                            }
                        }

                        film.cast = cast;

                        result.Add(film);
                    }
                }
            }
            catch (Exception)
            {
                
            }

            
            return result;
        }
    }
}