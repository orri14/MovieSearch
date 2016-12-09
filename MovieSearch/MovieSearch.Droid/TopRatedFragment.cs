using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V4.App;
using Android.Widget;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    using Android.OS;
    using Android.Views;

    using MovieSearch.Model;

    public class TopRatedFragment : Fragment
    {
        private ApiService _api;
        private ProgressBar _spinner;
        private View _rootView;
        private ListView _listView;
        private List<FilmInfo> _movies;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _movies = new List<FilmInfo>();
            _api = new ApiService();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            _rootView = inflater.Inflate(Resource.Layout.TopRatedList, container, false);
            _listView = _rootView.FindViewById<ListView>(Resource.Id.movielistview);
            return _rootView;
        }

        public async Task downloadTopRated()
        {
            this._movies.Clear();

            _spinner = _rootView.FindViewById<ProgressBar>(Resource.Id.progressSpinner);
            _spinner.Visibility = ViewStates.Visible;

            _movies  = await _api.getTopRatedMovies();
           
            _spinner.Visibility = ViewStates.Invisible;

            this._listView.Adapter = new MovieListAdapter(this.Activity, _movies);
            this._listView.ItemClick += this.itemClick;
        }

        void itemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this.Context, typeof(MovieSelectedInfoActivity));
            intent.PutExtra("movie", JsonConvert.SerializeObject(_movies[e.Position]));
            this.StartActivity(intent);
        }
    }
}
 