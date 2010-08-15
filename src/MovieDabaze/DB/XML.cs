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
        
        public override void load(String filename)
        {
            movies = new MoviesManager.Movies();

            XmlSerializer serializer = new XmlSerializer(typeof(MoviesManager.Movies));
            TextReader textReader = new StreamReader(@"C:\movie.xml");

            movies = (MoviesManager.Movies)serializer.Deserialize(textReader);

            MoviesManager.Movies.Instance.movies = movies.movies;
            textReader.Close();
        }

        public override void save(String filename)
        {
            movies = MoviesManager.Movies.Instance;

            XmlSerializer serializer = new XmlSerializer(typeof(MoviesManager.Movies));
            TextWriter textWriter = new StreamWriter(@filename);

            serializer.Serialize(textWriter, movies);

            textWriter.Close();
        }
    }
}
