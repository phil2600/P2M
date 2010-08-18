using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDabaze.Scraper
{

    public class NodeImdb
    {
        public String title;
    }

    class Imdb : Scraper
    {
        public override void load()
        {
        }

        public override MoviesManager.Movies FindMoviesTitles(String moviename)
        {
            return null;
        }

        public override MoviesManager.Movie FindMovie(int code)
        {
            return null;
        }
    }
}
