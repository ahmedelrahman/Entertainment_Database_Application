using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace MidTerm
{
    /// <summary>
    /// Interaction logic for SearchMovie.xaml
    /// </summary>
    public partial class SearchMovie : Window
    {
        private List<Movies> movieCollection;
        public ObservableCollection<Movies> DBQueries { get; set; } = new ObservableCollection<Movies>();
        List<string> PlatformCollection { get; set; } = new List<string>();

        public UserAccount UserInUse { get; set; }
        public List<UserAccount> Users { get; set; }
        XmlSerializer xmler = new XmlSerializer(typeof(List<UserAccount>));

        Dictionary<string, string> dict = new Dictionary<string, string>
        {
            { "MovieGenre", "Genre" },
            { "MovieGlobalSaleRange", "Global_Sales" },
            { "MovieName", "Name" },
            { "MovieAudienceScore", "AudienceScore"},
            { "MovieRottenTomatoesScore", "RottenTomatoes" },
            { "MovieOtherSaleRange", "Other_Sales" },
            { "MoviePublisher", "Publisher" },
            { "MovieYear", "Year" },
        };

        public SearchMovie(List<Movies> collection, UserAccount user, List<UserAccount> users)
        {
            InitializeComponent();
            DataContext = this;
            movieCollection = collection;
            SetAllComboBoxes();
            UserInUse = user;
            Users = users;
        }

        private void SetAllComboBoxes()
        {
            List<string> PublisherCollection = movieCollection.Select(x => x.Publisher).Distinct().ToList();
            PublisherCollection.Sort();
            MoviePublisher.ItemsSource = PublisherCollection;

            List<string> GenreCollection = movieCollection.Select(x => x.Genre).Distinct().ToList();
            GenreCollection.Sort();
            MovieGenre.ItemsSource = GenreCollection;

            List<string> YearCollection = movieCollection.Select(x => x.Year).Distinct().ToList();
            YearCollection.Sort();
            MovieYear.ItemsSource = YearCollection;
        }

        private async void SearchMovieButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            //collection of all strings extracted from the comboboxes and textboxes
            List<Tuple<string, string>> queryFields = new List<Tuple<string, string>>();

            //get textboxes with entries
            List<TextBox> tb = CBParent1.Children.OfType<TextBox>().Where(x => !string.IsNullOrWhiteSpace(x.Text)).ToList();
            foreach (TextBox t in tb) { queryFields.Add(Tuple.Create(t.Name, t.Text)); }

            //get comboboxes with selections
            List<ComboBox> cb = new List<ComboBox>();
            cb.AddRange(CBParent1.Children.OfType<ComboBox>().Where(x => x.SelectedIndex != -1).ToList());
            //cb.AddRange(CBParent2.Children.OfType<ComboBox>().Where(x => x.SelectedIndex != -1).ToList());
            foreach (ComboBox c in cb) { queryFields.Add(Tuple.Create(c.Name.ToString(), c.SelectedItem.ToString())); }

            //collection of lambda expressions
            List<string> FilterCollection = new List<string>();
            for (int i = queryFields.Count(); i != 0; i--) { FilterCollection.Add($"g => g.{dict[queryFields[i - 1].Item1]} == \"{queryFields[i - 1].Item2}\""); }

            //container that holds results from all linqs
            ScriptOptions options = ScriptOptions.Default.AddReferences(typeof(Movies).Assembly);
            List<Movies> queriedMovies = new List<Movies>(movieCollection);
            foreach (string lambdaExpression in FilterCollection)
            {
                Func<Movies, bool> movieFilterExpression = await CSharpScript.EvaluateAsync<Func<Movies, bool>>(lambdaExpression, options);
                queriedMovies = queriedMovies.Where(movieFilterExpression).ToList();
            }

            DBQueries = new ObservableCollection<Movies>(queriedMovies);
            Movies.ItemsSource = DBQueries;
            if (DBQueries.Count() == 0) { MessageBox.Show("No movies match the query", "Ooops"); }
        }

        private void WishlistCommand(object sender, RoutedEventArgs e)
        {
            if (Movies.SelectedItem != null)
            {
                //UserInUse.WishListGames.Add(Games.SelectedItem as Game);
                var s = Movies.SelectedItems.Cast<Object>().ToArray();
                foreach (var i in s)
                {
                    UserInUse.WishListMovies.Add(i as Movies);
                }
                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                MessageBox.Show("Your selected movie(s) has been added to your Movies Wish List", "Success!!!");
            }
        }

        private void OwnedListCommand(object sender, RoutedEventArgs e)
        {
            if (Movies.SelectedItem != null)
            {
                //UserInUse.OwnedGames.Add(Games.SelectedItem as Game);
                var s = Movies.SelectedItems.Cast<Object>().ToArray();
                foreach (var i in s)
                {
                    UserInUse.OwnedMovies.Add(i as Movies);
                }
                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                MessageBox.Show("Your selected movie(s) has been added to your Movies Owned List", "Success!!!");
            }
        }

        private void Clear_Search(object sender, RoutedEventArgs e)
        {
            MovieName.Text = "";
            MovieGenre.SelectedItem = null;
            MoviePublisher.SelectedItem = null;
            MovieYear.SelectedItem = null;
        }
    }
}
