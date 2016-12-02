using System.Collections.Generic;
using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.iOS.Views;
using MovieSearch.Model;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TopRatedListController : UITableViewController
    {
        private List<FilmInfo> _movieList;
        private bool _reload;
        private ApiService _apiService;
        private PosterDownloadService downloader;

        private UIActivityIndicatorView activitySpinner;
        

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
            this.View.BackgroundColor = UIColor.FromRGB(70, 0, 0);
        }

        public override async void  ViewDidAppear(bool animated) 
        {
            if (this._reload)
            {
                activitySpinner = new Spinner(this.View.Frame).activitySpinner;                
                this.View.AddSubview(activitySpinner);
                activitySpinner.StartAnimating();

                var results = await _apiService.getTopRatedMovies();
                downloader.downloadPosters(results);

                _movieList.AddRange(results);

                this.TableView.ReloadData();
                activitySpinner.StopAnimating();

                this.TableView.Source = new MovieListSource(this._movieList, OnSelectedMovie);
            }

            this._reload = true;
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