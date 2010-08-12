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
            MoviesManager.Movie movie = new MovieDabaze.MoviesManager.Movie("tetest");
            movie._actor1 = "actor1";
            movie._actor2 = "actor2";
            movie._filename = "filename";
            movie._genre1 = "genre1";
            movie._genre2 = "genre2";
            movie._realisator = "realisator";
            XmlSerializer serializer = new XmlSerializer(typeof(MoviesManager.Movie));
            TextWriter textWriter = new StreamWriter(@"C:\movie.xml");
            serializer.Serialize(textWriter, movie);
            textWriter.Close();
        }
    }
}
