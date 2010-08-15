using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieDabaze.DB
{
    abstract class DB
    {
        public MoviesManager.Movies movies;
        public String db_path { get; set; }

        public DB()
        {
            movies = MoviesManager.Movies.Instance;
        }

        public abstract void test();

        public abstract void load(String filename);
        public abstract void save(String filename);
        public MoviesManager.Movies movies_get()
        {
            return movies;
        }
        public void movies_set(MoviesManager.Movies movies)
        {
            this.movies = movies;
        }
    }
}
