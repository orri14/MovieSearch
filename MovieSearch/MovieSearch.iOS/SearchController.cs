using System;
using CoreGraphics;
using UIKit;
using MovieSearch;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearch.iOS
{
    public class SearchController : UIViewController
    {
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        public UILabel createPrompt()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50),
                Text = "Enter words in movie title: "
            };
            this._yCoord += StepY;

            return label;
        }

        public UITextField createTitleField()
        {
            UITextField textField = new UITextField()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50),
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Djöflaeyjan"
            };
            this._yCoord += StepY;

            return textField;
        }

        public UILabel createMovieLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width, 50)
            };
            this._yCoord += StepY;

            return label;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = this.createPrompt();

            var titleField = this.createTitleField();

            var searchButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchButton.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            searchButton.SetTitle("Get movie", UIControlState.Normal);
            this._yCoord += StepY;

            var movieLabel = this.createMovieLabel();


            searchButton.TouchUpInside += async (sender, args) =>
            {
                titleField.ResignFirstResponder();


                MovieDbFactory.RegisterSettings(new MyDbSettings());
                
                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(titleField.Text);


                try
                {
                    MovieInfo info = response.Results[0];
                    movieLabel.Text = info.Title;
                } catch(Exception e) { throw e;}

                
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(titleField);
            this.View.AddSubview(searchButton);
            this.View.AddSubview(movieLabel);
        }
    }
}