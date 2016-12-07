using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware.Input;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using MovieSearch.Model;
using System.Collections.Generic;
using DM.MovieApi;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "MovieSearch.Droid", Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
	{

        private PosterDownloadService _downloader;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            _downloader = new PosterDownloadService();

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);


            var fragments = new Android.Support.V4.App.Fragment[]
                               {
                                    //new titleSearchFragment()
                               
                               };
            var titles = CharSequence.ArrayFromStringArray(new[]
                                                               {
                                                                   "Search"
                                                                   
                                                               });

            // Get our button from the layout resource,
            // and attach an event to it
            EditText titleSearch = this.FindViewById<EditText>(Resource.Id.titleSearchText);
            Button searchButton = this.FindViewById<Button>(Resource.Id.searchButton);

            
            searchButton.Click += async (sender, args) =>
            {
                var apiService = new ApiService();
                MovieDbFactory.RegisterSettings(new MyDbSettings());

                List<FilmInfo> movies = await apiService.getMoviesByTitle(titleSearch.Text);
                movies = await _downloader.downloadPosters(movies);

                var intent = new Intent(this, typeof(MovieListActivity));
                intent.PutExtra("personList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);
                
            };

        }
    }
}


