using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;
using MovieDownload;
using System.Net;


namespace MovieSearch.iOS.Controllers
{
    public class SearchController : UIViewController
    {
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        UIActivityIndicatorView activitySpinner;

        public SearchController()
        {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Search";

            this.View.BackgroundColor = UIColor.FromRGB(70, 0, 0); ;

            this._yCoord = StartY;

            var titleField = this.createTitleField();

            var searchButton = this.createButton("Search");


            var centerX = this.View.Frame.Width / 2;
            var centerY = this.View.Frame.Height / 2;

            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activitySpinner.Frame = new CGRect(
                centerX - activitySpinner.Frame.Width / 2,
                centerY - activitySpinner.Frame.Height - 20,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);


            searchButton.TouchUpInside += async (sender, args) =>
            {
                var apiService = new ApiService();


                searchButton.Enabled = false;
                activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
                this.View.AddSubview(activitySpinner);
                activitySpinner.StartAnimating();

                MovieDbFactory.RegisterSettings(new MyDbSettings());

                //MovieDbFactory.RegisterSettings("214da67793e3bbe4c504e678b40e82aa", "http://api.themoviedb.org/3/");


                titleField.ResignFirstResponder();

                List<FilmInfo> movies = await apiService.getMoviesByTitle(titleField.Text);

                this.NavigationController.PushViewController(new MovieListController(movies), true);

                activitySpinner.StopAnimating();
                searchButton.Enabled = true;
            };

            this.View.AddSubview(titleField);
            this.View.AddSubview(searchButton);
        }

        private UIButton createButton(string title)
        {
            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            button.SetTitle(title, UIControlState.Normal);
            button.Font = (UIFont.FromName("HelveticaNeue-Bold", 12f));
            button.SetTitleColor(UIColor.FromRGB(218, 165, 32), forState: UIControlState.Normal);
            button.SetTitleColor(UIColor.DarkGray, forState: UIControlState.Disabled);
            this._yCoord += StepY;
            return button; 
        }

        public UILabel createMovieLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, (this.View.Bounds.Height / 2), this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;

            return label;
        }


        public UITextField createTitleField()
        {
            UITextField textField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, (this.View.Bounds.Height / 2), this.View.Bounds.Width - 2 * HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Enter a title..."
            };
            this._yCoord += StepY;

            return textField;
        }
    }
}