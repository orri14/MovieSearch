using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.Model;
using Newtonsoft.Json;
using Square.Picasso;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "MovieSelectedInfoActivity", Icon = "@drawable/icon" ,MainLauncher = false)]
    public class MovieSelectedInfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MovieSelectInfo);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Selected Movie";

            var jsonStr = this.Intent.GetStringExtra("movie");
            var movie = JsonConvert.DeserializeObject<FilmInfo>(jsonStr);

            this.FindViewById<TextView>(Resource.Id.titleText).Text = movie.title;

            string genres = "";


            var countGenres = movie.genres.Count;
            for (int i = 0; i < countGenres; i++)
            {
                genres += movie.genres[i];
                genres += (i == countGenres - 1 ? "" : ", ");
            }

            this.FindViewById<TextView>(Resource.Id.duration).Text = movie.duration + " minutes";

            this.FindViewById<TextView>(Resource.Id.genres).Text = genres;

            this.FindViewById<TextView>(Resource.Id.description).Text = movie.description;

            var imageView = this.FindViewById<ImageView>(Resource.Id.picture);
            string ImageUrl = "http://image.tmdb.org/t/p/w500";

            Picasso.With(this)
           .Load(String.Concat(ImageUrl, movie.imageName))
           .Into(imageView);
        }
    }
}