using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using MovieSearch.Model;
using CoreGraphics;

namespace MovieSearch.iOS.Controllers
{

    class MovieController : UIViewController
    {
        private FilmInfo _movieInfo;
        private nfloat HorizontalMargin;
        private nfloat VerticalStep;

        public MovieController(FilmInfo info)
        {
            this._movieInfo = info;
            this.HorizontalMargin = this.View.Frame.Width / 10;
            this.VerticalStep = this.View.Frame.Height / 10;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = _movieInfo.title;

            var titleLabel = this.createTitleLabel();
            var durationAndGenreLabel = this.createDurationAndGenreLabel();
            var descriptionLabel = this.createDescriptionLabel();
            var moviePoster = new UIImageView();

            this.View.AddSubview(titleLabel);
            this.View.AddSubview(durationAndGenreLabel);
            this.View.AddSubview(descriptionLabel);
        }

        public UILabel createTitleLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, VerticalStep, this.View.Bounds.Width, VerticalStep),
                Text = _movieInfo.title + " (" + _movieInfo.year + " )"
            };
            
            return label;
        }

        public UILabel createDurationAndGenreLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, VerticalStep * 2, this.View.Bounds.Width, VerticalStep),
                Text = _movieInfo.duration + " | "
            };

            int numOfGenres = _movieInfo.genres.Count;

            for (int i = 0; i < numOfGenres; i++)
            {
                label.Text += _movieInfo.genres[i];
                label.Text += (i == numOfGenres - 1 ? "" : ", ");
            }


            return label;
        }

        public UILabel createDescriptionLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin * 5, VerticalStep * 3, this.View.Bounds.Width, VerticalStep * 7),
                Text = _movieInfo.description
            };

            return label;
        }

    }
}
