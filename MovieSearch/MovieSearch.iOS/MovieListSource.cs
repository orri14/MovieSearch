using System;
using System.Collections.Generic;
using System.Text;
using DM.MovieApi.MovieDb.Movies;
using Foundation;
using UIKit;

namespace MovieSearch.iOS
{
    class MovieListSource : UITableViewSource
    {
        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        private List<MovieInfo> _movielist;

        public MovieListSource(List<MovieInfo> movieList)
        {
            this._movielist = movieList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(this.MovieListCellId);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, (NSString) this.MovieListCellId);
            }
            int row = indexPath.Row;

            //Þurfti að breya þessu úr string í movielist.. gæti komið vitlaust út í appi
            cell.TextLabel.Text = this._movielist[row].ToString();
            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this._movielist.Count;
        }
    }
}
