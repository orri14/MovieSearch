using System;
using System.Collections.Generic;
using System.Text;
using DM.MovieApi.MovieDb.Movies;
using UIKit;

namespace MovieSearch.iOS
{
    class MovieListController : UITableViewController
    {
        private List<MovieInfo> _movieList;

        public MovieListController(List<MovieInfo> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            this.View.BackgroundColor = UIColor.White;
            this.Title = "Movie List";
            this.TableView.Source = new MovieListSource(this._movieList);
        }
    }
}
