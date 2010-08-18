using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Collections;

namespace MovieDabaze.Scraper
{
    class Allocine : Scraper
    {

        public readonly static string URI = @"http://api.allocine.fr/xml";
        private string _searchString = string.Empty;
        private XmlDocument _XMLdoc = new XmlDocument();

        private String type_num(String type)
        {
            XmlNodeList NumNodes = null;

            NumNodes = _XMLdoc.DocumentElement.GetElementsByTagName("results");
            string num = "";
            foreach (XmlNode node in NumNodes)
            {
                if (node.Attributes["type"].InnerText == type)
                {
                    num = node.Attributes["code"].InnerText;
                    return num;
                }
            }
            return "";
        }

        public override MoviesManager.Movies FindMoviesTitles(String moviename)
        {
            String filename = (@"C:\Users\phil\Desktop\searchAlien.xml");
            StreamReader sr = new StreamReader(filename, System.Text.Encoding.GetEncoding("iso-8859-1"));
            String ans = sr.ReadToEnd();
            XmlDocument SearchDoc = new XmlDocument();

            //_XMLdoc.LoadXml(getAllocineInfo(link));
            _XMLdoc.LoadXml(Encoding.UTF8.GetString(Encoding.Default.GetBytes(ans)));

            //movieName = movieName.Replace("&", "et");
            //string link = "http://api.allocine.fr/xml/search?q=" + movieName + "&partner=1&count=" + type_num("movie");
            //_XMLdoc.LoadXml(getAllocineInfo(link));
            _XMLdoc.LoadXml(Encoding.UTF8.GetString(Encoding.Default.GetBytes(ans)));

            MoviesManager.Movies retmovies = new MovieDabaze.MoviesManager.Movies();
//            XmlElement SearchEle = null;
            XmlNodeList SearchNodes = null;
            String  originaltitle,
                    film;

            SearchNodes = _XMLdoc.DocumentElement.GetElementsByTagName("movie");

            foreach (XmlNode node in SearchNodes)
            {
                originaltitle = node["originalTitle"].InnerText;

                if (node.InnerXml.Contains("title"))
                    film = originaltitle + " (" + node["title"].InnerText + ")";
                else
                    film = originaltitle;

                MoviesManager.Movie movie = new MovieDabaze.MoviesManager.Movie();
                movie._movie_code = node.Attributes["code"].InnerText;
                movie._title = film;
                retmovies.add(movie);
            }

            return retmovies;
        }

        public override void load()
        {
        }

        private String get_title()
        {
            String originaltitle;
            String film = String.Empty;
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("originalTitle");
            foreach (XmlNode elt in node)
            {
                originaltitle = elt.InnerText;

                if (elt.InnerXml.Contains("title"))
                    film = originaltitle + " (" + elt["title"].InnerText + ")";
                else
                    film = originaltitle;
            }
            return film;
        }

        private List<String> get_genre()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("genre");
            List<String> genres = new List<String>();
            foreach (XmlNode elt in node)
                genres.Add(elt.InnerText.ToString());
            return genres;
        }

        private List<String> get_activity(String activity, String activity2)
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("castMember");
            List<String> activities = new List<String>();
            foreach (XmlNode elt in node)
                if (elt["activity"].InnerText == activity || elt["activity"].InnerText == activity2)
                    activities.Add(elt["person"].InnerText.ToString());
            return activities;
        }

        private String get_runtime()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("runtime");
            foreach (XmlNode elt in node)
            {
                int runtime_min = Convert.ToInt32(elt.InnerText.ToString());
                if (runtime_min != 0)
                {
                    String runtime_hour = String.Empty;
                    if (runtime_min < 3600)
                        runtime_hour = "0h" + runtime_min / 60;
                    if (runtime_min >= 3600 && runtime_min < 7200)
                        runtime_hour = "1h" + (runtime_min - 3600) / 60;
                    if (runtime_min >= 7200 && runtime_min < 10800)
                        runtime_hour = "2h" + (runtime_min - 7200) / 60;
                    if (runtime_min >= 10800 && runtime_min < 14400)
                        runtime_hour = "3h" + (runtime_min - 10800) / 60;

                    return runtime_hour.ToString();
                }
            }
            return null;
        }

        private String get_prodyear()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("productionYear");
            foreach (XmlNode elt in node) // FIXME [LOOP ?]
                return elt.InnerText.ToString();
            return null;
        }

        private String get_synopsis()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("synopsis");
            foreach (XmlNode elt in node) // FIXME [LOOP ?]
            {
                String sysnopsis = elt.InnerXml;
                sysnopsis = sysnopsis.Replace("&amp;quot;", "");
                sysnopsis = sysnopsis.Replace("&lt;i&gt;", "");
                sysnopsis = sysnopsis.Replace("&lt;/i&gt;", "");
                sysnopsis = sysnopsis.Replace("&lt;br /&gt;", "\n");

                return sysnopsis.Normalize();
            }
            return null;
        }

        private String get_ratings()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("statistics");
            String noteType = String.Empty;
            foreach (XmlNode elt in node) // FIXME [LOOP ?]
            {
                if (elt.InnerXml.Contains("userRating"))
                {
                    string note = elt["userRating"].InnerText;
                    char[] Separateur = new Char[] { '.' };
                    string[] splitNote = note.Split(Separateur);
                    return splitNote[0];
                }
                if (noteType == "")
                {
                    if (elt.InnerXml.Contains("pressRating"))
                    {
                        string note = elt["pressRating"].InnerText;
                        char[] Separateur = new Char[] { '.' };
                        string[] splitNote = note.Split(Separateur);
                        return splitNote[0];
                    }
                }
            }
            return null;
        }

        private String get_link_picture()
        {
            XmlNodeList node = _XMLdoc.DocumentElement.GetElementsByTagName("poster");
            foreach (XmlNode elt in node) // FIXME [LOOP ?]
                return elt.Attributes["href"].InnerText.ToString();
            return null;
        }

        public override MoviesManager.Movie FindMovie(int code)
        {
            MoviesManager.Movie movie = new MovieDabaze.MoviesManager.Movie();
            String filename = (@"C:\Users\phil\Desktop\movie62Alien.xml");
            StreamReader sr = new StreamReader(filename, System.Text.Encoding.GetEncoding("iso-8859-1"));
            String ans = sr.ReadToEnd();
            //string link = "http://api.allocine.fr/xml/movie?code=" + movieNum + "&partner=1";
            //_XMLdoc.LoadXml(getAllocineInfo(link));
            _XMLdoc.LoadXml(Encoding.UTF8.GetString(Encoding.Default.GetBytes(ans)));

            movie._title = get_title();

            movie._genres = get_genre();
            movie._genre1 = movie._genres[0];
            movie._genre2 = movie._genres[1];
            

            movie._realisator = get_activity("Réalisateur", "Réalisatrice")[0];
            movie._actors = get_activity("Acteur", "Actrice");
            movie._actor1 = movie._actors[0];
            movie._actor2 = movie._actors[1];

            movie._runtime = get_runtime();
            movie._year = get_prodyear();
            movie._synopsis = get_synopsis();
            movie._note = get_ratings();
            movie._link_picture = get_link_picture();

            return movie;
        }

        // To Improve
        private String string_formatting(String to_format)
        {
            String ret = String.Empty;
            String accent = "ÀÁÂÃÄÅàáâãäåÒÓÔÕÖØòóôõöøÈÉÊËèéêëÌÍÎÏìíîïÙÚÛÜùúûüÿÑñÇç-";
            String sansAccent = "AAAAAAaaaaaaOOOOOOooooooEEEEeeeeIIIIiiiiUUUUuuuuyNnCc ";
            char[] tableauSansAccent = sansAccent.ToCharArray();
            char[] tableauAccent = accent.ToCharArray();

            for (int i = 0; i < accent.Length; i++)
                ret = to_format.Replace(tableauAccent[i].ToString(), tableauSansAccent[i].ToString());
            return ret;
        }

        private string getAllocineInfo(string alloCineUrl)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(alloCineUrl);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream resStream = response.GetResponseStream();

            String tempString = null;
            int count = 0;

            do
            {
                count = resStream.Read(buf, 0, buf.Length);

                if (count != 0)
                { // translate from bytes to ASCII text
                    tempString = Encoding.UTF7.GetString(buf, 0, count);
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // print out page source
            byte[] l = Encoding.Default.GetBytes(sb.ToString());
            string f = Encoding.UTF8.GetString(l);

            return f;
        }
    }
}