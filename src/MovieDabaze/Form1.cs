using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieDabaze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DB.DB database = DB.DBFactory.createInstance("XML");
            database.test();
            database.write_on_file("toot");
        }
    }
}
