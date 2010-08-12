using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using MovieDabaze.MoviesManager;
using System.IO;

namespace MovieDabaze.DB
{
    class dbXML : DB
    {
        public override void test()
        {
            MessageBox.Show("XML DB");
        }
        
        public override void load()
        {
        }

        public override void write_on_file(String filename)
        {
            MoviesManager.Movies movies = MoviesManager.Movies.Instance;

            XmlSerializer serializer = new XmlSerializer(typeof(MoviesManager.Movie));
            TextWriter textWriter = new StreamWriter(@"C:\movie.xml");

            foreach (MoviesManager.Movie movie in movies.movies)
            {
                serializer.Serialize(textWriter, movie);
            }

            textWriter.Close();
        }
    }
}
