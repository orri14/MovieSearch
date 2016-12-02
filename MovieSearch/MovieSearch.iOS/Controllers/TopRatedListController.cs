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
        private bool _reload;
        private ApiService _apiService;

        UIActivityIndicatorView activitySpinner;
        

        public TopRatedListController()
        {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
            _movieList = new List<FilmInfo>();
            _reload = true;
            _apiService = new ApiService();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Top Rated";
            this.View.BackgroundColor = UIColor.Black;
        }

        public override async void  ViewDidAppear(bool animated) 
        {
            if (this._reload)
            {
                var centerX = this.View.Frame.Width / 2;
                var centerY = this.View.Frame.Height / 2;

                activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
                activitySpinner.Frame = new CGRect(
                    centerX - activitySpinner.Frame.Width / 2,
                    centerY - activitySpinner.Frame.Height - 20,
                    activitySpinner.Frame.Width,
                    activitySpinner.Frame.Height);

                activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
                this.View.AddSubview(activitySpinner);
                activitySpinner.StartAnimating();

                var results = await _apiService.getTopRatedMovies();
                _movieList.AddRange(results);
            }

            this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            if (this._reload)
            {
                this._movieList.Clear();
                this.TableView.ReloadData();
            }
        }

        private void OnSelectedMovie(int row)
        {
            this.NavigationController.PushViewController(new MovieController(this._movieList[row]), true);
            this._reload = false;
        }

      

    }
}