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

            database.load("c:/movie.xml");

            Scraper.Scraper scraper = Scraper.ScraperFactory.createInstance("Allocine");
            scraper.load();
            scraper.FindMoviesTitles("Alien");
            MoviesManager.Movie test_movie = scraper.FindMovie(62);

            // Tests de lecture
            checkedListBox1.Items.Clear();
            foreach (MoviesManager.Movie movie in database.movies_get().movies)
                checkedListBox1.Items.Add(movie._filename);
            checkedListBox1.Sorted.CompareTo(true);
            if (checkedListBox1.Items.Count > 0)
                checkedListBox1.SelectedIndex = 0;

            MoviesManager.Movies movies = scraper.movies;
            movies.add(test_movie);

            checkedListBox2.Items.Clear();
            foreach (MoviesManager.Movie movie in database.movies_get().movies)
                checkedListBox2.Items.Add(movie._title);
            checkedListBox2.Sorted.CompareTo(true);
            if (checkedListBox2.Items.Count > 0)
                checkedListBox2.SelectedIndex = 0;


            //foreach (String item in checkedListBox1.CheckedItems)
            //    checkedListBox2.Items.Add(item);
            // \Tests de lecture
            
            database.save("c:/movie2.xml");
        }
    }
}
