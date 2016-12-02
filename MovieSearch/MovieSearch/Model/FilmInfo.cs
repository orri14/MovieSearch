using System.Collections.Generic;

namespace MovieSearch.Model
{
    public class FilmInfo
    {
        public string title { get; set; }
        public string year { get; set; }
        public string imageName { get; set; }
        public List<string> cast { get; set; } 
        public string description { get; set; }
        public List<string> genres { get; set; }
        public string duration { get; set; }
        public string rating { get; set; }
    }
}
