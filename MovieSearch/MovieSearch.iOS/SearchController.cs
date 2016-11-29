using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<string> _movies;


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = this.createPrompt();

            var titleField = this.createTitleField();

            var movieLabel = this.createMovieLabel();

            var searchButton = this.createButton("Search Movie");

            searchButton.TouchUpInside += async (sender, args) =>
            {
                titleField.ResignFirstResponder();

                MovieDbFactory.RegisterSettings(new MyDbSettings());

                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;


                //UIACTIVITYINDICATOR HERNA!
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(titleField.Text);
                //------------------


                /*
                 * 
                 activitySpinner.Frame = new CGRect (
                centerX - (activitySpinner.Frame.Width / 2) ,
                centerY - activitySpinner.Frame.Height - 20 ,
                activitySpinner.Frame.Width,
                activitySpinner.Frame.Height);
            activitySpinner.AutoresizingMask = UIViewAutoresizing.All;
            AddSubview (activitySpinner);
            activitySpinner.StartAnimating ();
                 */




                try
                {
                    this.NavigationController.PushViewController(new MovieListController(response.Results.ToList()), true);
                }
                catch (Exception e)
                {
                    
                    throw e;
                }

                //Afhverju exception? Er ekki nog ad tjekka hvort response se null?
                /*
                try
                {
                    MovieInfo info = response.Results[0];
                    movieLabel.Text = info.Title;
                } catch(Exception e) { throw e;}
                */

                
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(titleField);
            this.View.AddSubview(searchButton);
            this.View.AddSubview(movieLabel);
        }

        private UIButton createButton(string title)
        {
            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.Frame = new CGRect(HorizontalMargin, this._yCoord, this.View.Bounds.Width - 2 * HorizontalMargin, 50);
            button.SetTitle(title, UIControlState.Normal);
            this._yCoord += StepY;
            return button; 
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
    }
}