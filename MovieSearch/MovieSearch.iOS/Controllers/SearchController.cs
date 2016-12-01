using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;

using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;


namespace MovieSearch.iOS.Controllers
{
    public class SearchController : UIViewController
    {
        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int StepY = 50;

        private int _yCoord;

        public SearchController()
        {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie Search";

            this.View.BackgroundColor = UIColor.White;

            this._yCoord = StartY;

            var prompt = this.createPrompt();

            var titleField = this.createTitleField();

            var searchButton = this.createButton("Search Movie");

            searchButton.TouchUpInside += async (sender, args) =>
            {

                MovieDbFactory.RegisterSettings("214da67793e3bbe4c504e678b40e82aa", "http://api.themoviedb.org/3/");

                titleField.ResignFirstResponder();

                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(titleField.Text);


                List<FilmInfo> movies = new List<FilmInfo>();
                
                foreach(MovieInfo info in response.Results)
                {
                    FilmInfo film = new FilmInfo();
                    film.title = info.Title;
                    film.year = info.ReleaseDate.Year.ToString();
                    film.rating = info.VoteAverage.ToString().Equals("0") ? "-" : info.VoteAverage.ToString();
                    film.description = info.Overview;
                    film.imageName = info.PosterPath;

                    List<string> genres = new List<string>();
                    foreach(var genre in info.Genres)
                    {
                        genres.Add(genre.ToString());
                    }
                    film.genres = genres;

                    ApiQueryResponse<MovieCredit> credits = await movieApi.GetCreditsAsync(info.Id);

                    List<string> cast = new List<string>();
                    foreach (var actor in credits.Item.CastMembers)
                    {
                        cast.Add(actor.Name);
                    }
                    film.cast = cast;

                    movies.Add(film);
                }



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
                    this.NavigationController.PushViewController(new MovieListController(movies), true);
                }
                catch (Exception e)
                {
                    
                    throw e;
                }

                //Afhverju exception? Er ekki nog ad tjekka hvort response se null?
                /*

                */
                
                
            };

            this.View.AddSubview(prompt);
            this.View.AddSubview(titleField);
            this.View.AddSubview(searchButton);
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