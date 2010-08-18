using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDabaze.Scraper
{
    abstract class Scraper
    {
        public MoviesManager.Movies movies;
        public Scraper()
        {
            movies = MoviesManager.Movies.Instance;
        }

        public abstract void load();
        public abstract MoviesManager.Movies FindMoviesTitles(String moviename);
        public abstract MoviesManager.Movie FindMovie(int code);
    }
}
