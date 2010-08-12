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
        //public override void CreateDB()
        //{
        //    MessageBox.Show("MySQL created");
        //}
        //public override void WriteOnFile(String filename)
        //{
        //    MessageBox.Show("MySQL Write on " + filename);
        //}
    }
}
