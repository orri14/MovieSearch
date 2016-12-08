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


namespace MovieSearch.Droid
{


    [Activity(Theme = "@style/MyTheme", Label = "Movie list")]
    public class MovieListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.MovieList);
            
            var jsonStr = this.Intent.GetStringExtra("movieList");
            var movieList = JsonConvert.DeserializeObject<List<FilmInfo>>(jsonStr);

            var listview = this.FindViewById<ListView>(Resource.Id.movielistview);
            listview.Adapter = new MovieListAdapter(this, movieList);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitle);
        }
    }
}