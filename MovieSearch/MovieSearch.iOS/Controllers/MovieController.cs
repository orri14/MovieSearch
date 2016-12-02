﻿using DM.MovieApi.MovieDb.Movies;
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

        private const int HorizontalMargin = 20;

        private const int StartY = 80;

        private const int VerticalStep = 30;

        private int _yCoord;

        public MovieController(FilmInfo info)
        {
            this._movieInfo = info;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.FromRGB(90, 0, 0);
            this.Title = _movieInfo.title;

            _yCoord = StartY;

            var titleLabel = this.createTitleLabel();
            var durationAndGenreLabel = this.createDurationAndGenreLabel();
            var descriptionLabel = this.createDescriptionLabel();
            var moviePoster = this.createMoviePoster();
            

            this.View.AddSubview(titleLabel);
            this.View.AddSubview(durationAndGenreLabel);
            this.View.AddSubview(descriptionLabel);
            this.View.AddSubview(moviePoster);
        }

        private UILabel createTitleLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, _yCoord, this.View.Bounds.Width - HorizontalMargin, 50),
                Text = _movieInfo.title + " (" + _movieInfo.year + ")",
                Font = UIFont.FromName("HelveticaNeue-Bold", 16f),
                TextColor = UIColor.FromRGB(218, 165, 32)
            };
            _yCoord += VerticalStep;
            
            return label;
        }

        private UILabel createDurationAndGenreLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, _yCoord, this.View.Bounds.Width - HorizontalMargin, 50),
                Text = _movieInfo.duration + " min | ",
                Font = UIFont.FromName("Helvetica", 10f),
                TextColor = UIColor.White
            };
            

            int numOfGenres = _movieInfo.genres.Count;

            for (int i = 0; i < numOfGenres; i++)
            {
                label.Text += _movieInfo.genres[i];
                label.Text += (i == numOfGenres - 1 ? "" : ", ");
            }

            _yCoord += VerticalStep + 10;


            return label;
        }

        private UILabel createDescriptionLabel()
        {
            UILabel label = new UILabel()
            {
                Frame = new CGRect(HorizontalMargin, _yCoord, this.View.Bounds.Width / 2 - HorizontalMargin, this.View.Bounds.Height),
                Text = _movieInfo.description,
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 15,
                Font = UIFont.FromName("Helvetica", 12f),
                TextColor = UIColor.White
            };

            return label;
        }

        private UIImageView createMoviePoster()
        {
            UIImageView moviePoster = new UIImageView();

            moviePoster.Image = UIImage.FromFile(_movieInfo.imageName);
            moviePoster.Frame = new CGRect(HorizontalMargin, _yCoord, (this.View.Bounds.Width / 2) - 10, ((this.View.Bounds.Width / 2) - 10) * (20/15) );

            return moviePoster;
        }


    }
}
