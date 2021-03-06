using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.Model;
using Java.IO;
using Square.Picasso;


namespace MovieSearch.Droid
{
    
    public class MovieListAdapter : BaseAdapter<FilmInfo>
    {
        private Activity _context;

        private List<FilmInfo> _movieList;

        public MovieListAdapter(Activity context, List<FilmInfo> movieList)
        {
            this._context = context;
            this._movieList = movieList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);
            }

            var movie = this._movieList[position];

            int minNumOfAct = Math.Min(3, movie.cast.Count);

            string cast = "";

            for (int i = 0; i < minNumOfAct; i++)
            {
                cast += (movie.cast[i] == null) ? "" : movie.cast[i];
                cast += (i == 2) ? "" : ", ";
            }


            view.FindViewById<TextView>(Resource.Id.titleText).Text = movie.title;
            view.FindViewById<TextView>(Resource.Id.yearText).Text = movie.year;
            view.FindViewById<TextView>(Resource.Id.actorsText).Text = cast;

            string ImageUrl = "http://image.tmdb.org/t/p/w154";

            Picasso.With(this._context)
           .Load(String.Concat(ImageUrl, movie.imageName))
           .Into(view.FindViewById<ImageView>(Resource.Id.picture));
            return view;
        }

        public override int Count
        {
            get
            {
                return this._movieList.Count;
            }
        }

        public override FilmInfo this[int position]
        {
            get
            {
                return this._movieList[position];
            }
        }
    }
}