using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using DM.MovieApi.MovieDb.Movies;

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

            this.BackgroundColor = UIColor.FromRGB(70, 0, 0);

            this._titleLabel = new UILabel()
            {
                Font = UIFont.FromName("HelveticaNeue-Bold", 12f),
                TextColor = UIColor.FromRGB(218, 165, 32)
            };

            this._actorsLabel = new UILabel()
            {
                Font = UIFont.FromName("Helvetica", 8f),
                TextColor = UIColor.White
            };

            this._ratingLabel = new UILabel()
            {
                Font = UIFont.FromName("HelveticaNeue-Bold", 10f),
                TextColor = UIColor.FromRGB(218, 165, 32),
                TextAlignment = UITextAlignment.Center
            };

            this.ContentView.AddSubviews(new UIView[] { this._imageView, this._titleLabel, this._actorsLabel, this._ratingLabel });
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this._imageView.Frame = new CGRect(this.ContentView.Bounds.Width - 60, 5, 33, 33);
            this._titleLabel.Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25);
            this._actorsLabel.Frame = new CGRect(5, 25, this.ContentView.Bounds.Width, 20);
            this._ratingLabel.Frame = new CGRect(this.ContentView.Bounds.Width - 10, 10, 5, 25);
        }

        public void UpdateCell(FilmInfo info)
        {
            //this._imageView.Image = UIImage.FromFile(info.imageName);
            this._titleLabel.Text = info.title + " (" + info.year + ")";


            this._actorsLabel.Text = "";
            int minNumOfAct = Math.Min(3, info.cast.Count);

            for(int i = 0; i < minNumOfAct; i++)
            {
                this._actorsLabel.Text += (info.cast[i] == null) ? "" : info.cast[i];
                this._actorsLabel.Text += ((info.cast[i] == null) || (i == 2)) ? "" : ", ";
            }
            
            this._ratingLabel.Text = info.rating;

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}
