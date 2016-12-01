using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Þarf mögulega ekki að hafa þenna klasa
 * Bua i staðinn til listann í SearchController
 */

namespace MovieSearch.Model
{
    public class FilmInfo
    {
        public string title { get; set; }
        public string year { get; set; }
        public string imageName { get; set; }
        public string cast { get; set; } 
        public string description { get; set; }
        public List<string> genres { get; set; }
        public string duration { get; set; }
        public string rating { get; set; }
    }
}
