using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieDabaze.DB
{
    class DBFactory
    {
        public static DB createInstance(String type)
        {
            if (type == "XML")
                return new dbXML();
            if (type == "MySQL")
                return new dbMySQL();
            return new dbXML();
        }
    }
}