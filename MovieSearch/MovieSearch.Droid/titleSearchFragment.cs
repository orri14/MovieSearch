
using System;

using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using Fragment = Android.Support.V4.App.Fragment;

namespace MovieSearch.Droid
{
    using MovieSearch.Model;
    using Android.Views.InputMethods;

    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class TitleSearchFragment : Fragment
    {
        private ApiService _api;
        private ProgressBar _spinner;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _api = new ApiService();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var rootView = inflater.Inflate(Resource.Layout.TitleSearch, container, false);

            // Get our UI controls from the loaded layout:
            EditText titleEditText = rootView.FindViewById<EditText>(Resource.Id.titleEditText);
            Button searchButton = rootView.FindViewById<Button>(Resource.Id.searchButton);

            _spinner = rootView.FindViewById<ProgressBar>(Resource.Id.progressSpinner);
            _spinner.Visibility = ViewStates.Invisible;

            searchButton.Click += async (object sender, EventArgs e) =>
            {
                searchButton.Visibility = ViewStates.Gone;
                _spinner.Visibility = ViewStates.Visible;

                List<FilmInfo> movies = await _api.getMoviesByTitle(titleEditText.Text);

                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("MovieList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);

                searchButton.Visibility = ViewStates.Visible;
                _spinner.Visibility = ViewStates.Invisible;
            };

            return rootView;
        }
    }
}