using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDabaze.Scraper
{
    class ScraperFactory
    {
        public static Scraper createInstance(String type)
        {
            if (type == "Allocine")
                return new Allocine();
            if (type == "IMDB")
                return new Imdb();
            return new Allocine();
        }

    }
}
