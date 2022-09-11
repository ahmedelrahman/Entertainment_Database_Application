using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTerm
{
    public class MovieDataBaseVM : INotifyPropertyChanged
    {
        public ObservableCollection<Movies> MainMoviesCollection { get; set; } = new ObservableCollection<Movies>();
        //List<Movies> MainMoviesCollection = ProcessMoviesFile("movies.csv");
        private Movies selectedMovie;
        public Movies SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                selectedMovie = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedMovie"));
            }
        }

        public MovieDataBaseVM(List<Movies> collection)
        {
            MainMoviesCollection = new ObservableCollection<Movies>(collection);
            //ReadInDataFromXML();
        }
        private static List<Movies> ProcessMoviesFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Movies.ParseFromFile).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}