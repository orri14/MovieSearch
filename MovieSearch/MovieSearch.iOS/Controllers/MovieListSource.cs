using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Model;

 
namespace MovieSearch.iOS.Controllers
{
    class MovieListSource : UITableViewSource
    {
        public readonly NSString MovieListCellId = new NSString("MovieListCell");

        private List<FilmInfo> _movielist;

        private Action<int> _onSelectedMovie;

 
        public MovieListSource(List<FilmInfo> movieList, Action<int> onSelectedMovie)
        {
            this._movielist = movieList;
            this._onSelectedMovie = onSelectedMovie;
        }
        

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CustomCell) tableView.DequeueReusableCell(this.MovieListCellId);

            if (cell == null)
            {
                cell = new CustomCell((NSString) this.MovieListCellId);
            }
            int row = indexPath.Row;

            //Þurfti að breya þessu úr string í movielist.. gæti komið vitlaust út í appi

            cell.UpdateCell(this._movielist[row]);

            return cell;
        }

        
        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return this._movielist.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovie(indexPath.Row);
        }
        
    }
}
