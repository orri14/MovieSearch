using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MovieDownload;
using MovieSearch.Model;

namespace MovieSearch.Droid
{
    public class PosterDownloadService
    {
        private ImageDownloader downloader;

        public PosterDownloadService()
        {
            downloader = new ImageDownloader(new StorageClient());
        }

        public async Task<List<FilmInfo>> downloadPosters(List<FilmInfo> movies)
        {
            foreach (FilmInfo film in movies)
            {
                var posterlink = film.imageName;

                var ImagePath = downloader.LocalPathForFilename(posterlink);

                if (ImagePath != "")
                {
                    await downloader.DownloadImage(posterlink, ImagePath, CancellationToken.None);
                }

                film.imageName = ImagePath;
            }
            return movies;
        }
    }
}