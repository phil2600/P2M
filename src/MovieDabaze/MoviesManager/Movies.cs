using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MovieDabaze.MoviesManager
{
    class Movies
    {
        static Movies instance = null;
        static readonly object padlock = new object();

        public List<Movie> movies = new List<Movie>();
        private bool loaded = false;

        public Movies()
        {
        }

        public static Movies Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Movies();
                    return instance;
                }
            }
        }

        public Movies(String filename)
        {
            this.load(filename);
        }

        public void add(Movie movie)
        {
            movies.Add(movie);
        }

        public bool find(Movie movie)
        {
            return movies.Contains(movie);
        }

        public void remove(Movie movie)
        {
            movies.Remove(movie);
        }

        public String find_by_title(String title)
        {
            try
            {
                return movies.First(a => a._title.Contains(title))._title;
                //return true;
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
        
        public bool exists(String link)
        {
            try
            {
                movies.First(a => a._link.Contains(link));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void load(String filename)
        {
            if (loaded)
                return ;
            XmlTextReader reader = new XmlTextReader(filename);
            reader.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument details = new XmlDocument();
            details.Load(reader);

            foreach (XmlNode node in details.DocumentElement.ChildNodes)
            {
                if (node.Name == "film")
                {
                    Movie movie = new Movie(node.Attributes["link"].Value);
                    movie._title = node.Attributes["titre"].Value;
                    movie._realisator = node.Attributes["realisateur"].Value;
                    movie._actor1 = node.Attributes["acteur1"].Value;
                    movie._actor2 = node.Attributes["acteur2"].Value;
                    movie._genre1 = node.Attributes["genre1"].Value;
                    movie._genre2 = node.Attributes["genre2"].Value;
                    movie._synopsis = node.Attributes["synopsis"].Value;
                    movie._note = node.Attributes["note"].Value;
                    movie._runtime = node.Attributes["duree"].Value;
                    //movie._already_seen = node.Attributes["dejavu"].;
                    movie._year = node.Attributes["annee"].Value;
                    movie._link = node.Attributes["link"].Value;
                    movie._link_picture = node.Attributes["link_affiche"].Value;
                    this.add(movie);
                }
            }
            loaded = true;
            reader.Close();
        }
    }
}
