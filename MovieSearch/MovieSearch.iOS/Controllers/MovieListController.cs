
using System;
using System.Collections.Generic;
using System.Text;
using DM.MovieApi.MovieDb.Movies;
using UIKit;


namespace MovieSearch.iOS.Controllers
{
    class MovieListController : UITableViewController
    {
        private List<FilmInfo> _movieList;

        public MovieListController(List<FilmInfo> movieList)
        {
            this._movieList = movieList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = "Search result";
            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        private void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieController(this._movieList[row]), true);
        }
    }
}
