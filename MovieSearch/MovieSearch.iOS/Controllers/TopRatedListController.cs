using System.Collections.Generic;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TopRatedListController : UITableViewController
    {
        private List<FilmInfo> _movieList;

        UIActivityIndicatorView activitySpinner;

        public TopRatedListController()
        {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);

            var centerX = this.View.Frame.Width / 2;
            var centerY = this.View.Frame.Height / 2;

            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - activitySpinner.Frame.Width / 2,
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
        }

        public override async void  ViewDidLoad() 
        {
            base.ViewDidLoad();
            this.Title = "Top Rated";
            this.View.BackgroundColor = UIColor.Black;

            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            this.View.AddSubview(activitySpinner);
            activitySpinner.StartAnimating();

            var apiService = new ApiService();

            _movieList = await apiService.getTopRatedMovies();
            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        private void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieController(this._movieList[row]), true);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);


        }

    }
}