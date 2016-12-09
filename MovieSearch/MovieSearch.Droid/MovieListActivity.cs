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
        private List<FilmInfo> _movieList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.MovieList);
            
            var jsonStr = this.Intent.GetStringExtra("MovieList");

            _movieList = JsonConvert.DeserializeObject<List<FilmInfo>>(jsonStr);

            var listview = this.FindViewById<ListView>(Resource.Id.movielistview);
            listview.Adapter = new MovieListAdapter(this, _movieList);
            listview.ItemClick += this.itemClick;

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = this.GetString(Resource.String.ToolbarTitle);
        }

        void itemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(MovieSelectedInfoActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(_movieList[e.Position]));
            this.StartActivity(intent);
        }
    }
}