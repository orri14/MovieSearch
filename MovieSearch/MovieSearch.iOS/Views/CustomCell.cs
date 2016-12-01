using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;

namespace MovieSearch.iOS
{
    public class CustomCell : UITableViewCell
    {
        private UILabel _titleLabel, _actorsLabel, _ratingLabel;
        private UIImageView _imageView;

        public CustomCell(NSString cellId)
            : base(UITableViewCellStyle.Default, cellId)
        {
            this._imageView = new UIImageView();

            this._titleLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
            };

            this._actorsLabel = new UILabel()
            {
                Font = UIFont.FromName("Cochin-BoldItalic", 12f),
                TextColor = UIColor.FromRGB(90, 51, 0),
            };

            this._ratingLabel = new UILabel()
            {
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._titleLabel, this._actorsLabel, this._ratingLabel });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this._imageView.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 10, 25);
            this._titleLabel.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25);
            this._actorsLabel.Frame = new CGRect(100, 25, 100, 20);
            this._ratingLabel.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 10, 25);
        }

        public void UpdateCell(FilmInfo info)
        {
            this._imageView.Image = UIImage.FromFile(info.imageName);
            Console.WriteLine(info.imageName);
            Console.WriteLine(this._imageView);
            this._titleLabel.Text = info.title + " (" + info.year + ")";
            this._actorsLabel.Text = "";
            
            /*
            for(int i = 0; i < 3; i++)
            {
                this._actorsLabel += info.cast;
            }
            */
            
            this._ratingLabel.Text = info.rating;

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}
