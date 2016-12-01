using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace MovieSearch.iOS.Controllers
{

    class MovieController : UIViewController
    {
        private FilmInfo _movieInfo;

        public MovieController(FilmInfo info)
        {
            this._movieInfo = info;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;
            this.Title = _movieInfo.title;
        }

    }
}
