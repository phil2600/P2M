using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieDabaze.MoviesManager
{
    public class Movie
    {
        /*
        ** Attributs : Getter & Setter
        */ 
        public String _filename { get; set; }
        public String[] _filename_not_ext { get; set; }

        // Visible information
        public String _title { get; set; }
        public String _realisator { get; set; }
        public String _actor1 { get; set; }
        public String _actor2 { get; set; }
        public String _genre1 { get; set; }
        public String _genre2 { get; set; }
        public String _synopsis { get; set; }
        public String _note { get; set; }
        public String _runtime { get; set; }
        public String _year { get; set; }
        public String _link_picture { get; set; }
        public String _link { get; set; }
        public String _already_seen { get; set; }

        //message;

        /*
        ** Public Methods
        */
        public Movie(String filename)
        {
            _filename = filename;
            _filename_not_ext = _filename.Split('.');
        }
        public Movie()
        {
        }

        public bool is_right_ext()
        {
            return ((_filename_not_ext[1] == "avi") || (_filename_not_ext[1] == "mkv") || (_filename_not_ext[1] == "mpg") || (_filename_not_ext[1] == "MPG") || (_filename_not_ext[1] == "AVI") || (_filename_not_ext[1] == "MKV"));
        }

        //public String filename_cleaner()
        //{
//            return this.filename_cleaner(this._filename_not_ext);
        //}
        public String filename_cleaner(String filename)
        {
            String cleaned = String.Empty;
            Regex regexp = new Regex(@" |, ");


            return cleaned;
        }
    }
}
