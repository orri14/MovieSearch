using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using MovieSearch.Model;
using MovieSearch.iOS.Views;



namespace MovieSearch.iOS.Controllers
{
    public class SearchController : UIViewController
    {
        private const int HorizontalMargin = 20;

        private const int StartY = 150;

        private const int StepY = 150;

        private int _yCoord;

        private UIActivityIndicatorView activitySpinner;

        private PosterDownloadService _downloader;

        public SearchController()
        {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
            _downloader = new PosterDownloadService();
                 
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Search";

            this.View.BackgroundColor = UIColor.FromRGB(70, 0, 0);

            this._yCoord = StartY;

            var titleField = this.createTitleField();

            var searchButton = this.createButton("Search");

            activitySpinner = new Spinner(this.View.Frame).activitySpinner;

            searchButton.TouchUpInside += async (sender, args) =>
            {
                var apiService = new ApiService();

                searchButton.Enabled = false;
                activitySpinner.StartAnimating();

                MovieDbFactory.RegisterSettings(new MyDbSettings());

                titleField.ResignFirstResponder();

                List<FilmInfo> movies = await apiService.getMoviesByTitle(titleField.Text);
                _downloader.downloadPosters(movies);

                this.NavigationController.PushViewController(new MovieListController(movies), true);

                activitySpinner.StopAnimating();
                searchButton.Enabled = true;
            };


            this.View.AddSubview(activitySpinner);
            this.View.AddSubview(titleField);
            this.View.AddSubview(searchButton);
        }

        private UIButton createButton(string title)
        {
            var centerX = this.View.Bounds.Width / 2;
            var buttonWidth = this.View.Bounds.Width - 6*HorizontalMargin;

            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.Frame = new CGRect(centerX - buttonWidth/2, this._yCoord, buttonWidth, 50);
            button.SetTitle(title, UIControlState.Normal);
            button.Font = (UIFont.FromName("HelveticaNeue-Bold", 12f));

            button.Layer.CornerRadius = 6f;
            button.Layer.BorderWidth = 0.5f;

            button.BackgroundColor = UIColor.White;


            button.SetTitleColor(UIColor.Orange, forState: UIControlState.Normal);
            button.SetTitleColor(UIColor.DarkGray, forState: UIControlState.Disabled);

            this._yCoord += StepY;
            return button; 
        }


        public UITextField createTitleField()
        {
            
            UITextField textField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2*HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Enter a title..."
            };
            this._yCoord += StepY;

            return textField;
        }
    }
}