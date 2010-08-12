using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieDabaze.DB
{
    class dbMySQL : DB
    {
        public override void test()
        {
            MessageBox.Show("mySQL DB");
        }

        public override void load()
        {

        }

        public override void write_on_file(String filename)
        {

        }
    }
}
