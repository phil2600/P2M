using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieDabaze.DB
{
    abstract class DB
    {
        MoviesManager.Movies movies;
        public String db_path { get; set; }

        public DB()
        {
            movies = MoviesManager.Movies.Instance;
        }
        
        public abstract void test();
        
        public abstract void load();
        public abstract void write_on_file(String filename);
    }
}
