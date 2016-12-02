﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;

namespace MovieSearch.iOS
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

            foreach (MovieInfo info in response.Results)
            {
                FilmInfo film = new FilmInfo();
                film.title = info.Title;
                film.year = info.ReleaseDate.Year.ToString();
                film.rating = info.VoteAverage.ToString().Equals("0") ? "-" : info.VoteAverage.ToString();
                film.description = info.Overview;

                film.imageName = "interstellar";

                List<string> genres = new List<string>();

               
                foreach (var genre in info.Genres)
                {
                    genres.Add(genre.ToString());
                }
                film.genres = genres;

                ApiQueryResponse<MovieCredit> credits = await _movieApi.GetCreditsAsync(info.Id);

                List<string> cast = new List<string>();

                if (credits.Item.CastMembers != null)
                {
                    foreach (var actor in credits.Item.CastMembers)
                    {
                        cast.Add(actor.Name);
                    }
                }
                
                film.cast = cast;

                result.Add(film);
            }

            return result;
        }
    }
}