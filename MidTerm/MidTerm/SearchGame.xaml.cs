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
    /// Interaction logic for SearchGame.xaml
    /// </summary>
    public partial class SearchGame : Window
    {
        private List<Game> gameCollection;
        public ObservableCollection<Game> DBQueries { get; set; } = new ObservableCollection<Game>();
        List<string> PlatformCollection { get; set; } = new List<string>();

        public UserAccount UserInUse { get; set; }
        public List<UserAccount> Users { get; set; }
        XmlSerializer xmler = new XmlSerializer(typeof(List<UserAccount>));

        Dictionary<string, string> dict = new Dictionary<string, string>
        {
            { "GameEUSaleRange", "EU_Sales" },
            { "GameGenre", "Genre" },
            { "GameGlobalSaleRange", "Global_Sales" },
            { "GameJPSaleRange","JP_Sales" },
            { "GameName", "Name" },
            { "GameUSSaleRange", "NA_Sales" },
            { "GameOtherSaleRange", "Other_Sales" },
            { "GamePlat", "Platform" },
            { "GamePublisher", "Publisher" },
            { "GameRank", "Rank" },
            { "GameYear", "Year" },
        };

        public SearchGame(List<Game> collection, UserAccount user, List<UserAccount> users)
        {
            InitializeComponent();
            DataContext = this;
            gameCollection = collection;
            SetAllComboBoxes();
            UserInUse = user;
            Users = users;
        }

        private void SetAllComboBoxes()
        {
            List<string> PlatformCollection = gameCollection.Select(x => x.Platform).Distinct().ToList();
            PlatformCollection.Sort();
            GamePlat.ItemsSource = PlatformCollection;

            List<string> PublisherCollection = gameCollection.Select(x => x.Publisher).Distinct().ToList();
            PublisherCollection.Sort();
            GamePublisher.ItemsSource = PublisherCollection;

            List<string> GenreCollection = gameCollection.Select(x => x.Genre).Distinct().ToList();
            GenreCollection.Sort();
            GameGenre.ItemsSource = GenreCollection;

            List<string> YearCollection = gameCollection.Select(x => x.Year).Distinct().ToList();
            YearCollection.Sort();
            GameYear.ItemsSource = YearCollection;
        }


        private async void SearchGameButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            //collection of all strings extracted from the comboboxes and textboxes
            List<Tuple<string, string>> queryFields = new List<Tuple<string, string>>();

            //get textboxes with entries
            List<TextBox> tb = TextBoxParent.Children.OfType<TextBox>().Where(x => !string.IsNullOrWhiteSpace(x.Text)).ToList();
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
            ScriptOptions options = ScriptOptions.Default.AddReferences(typeof(Game).Assembly);
            List<Game> queriedGames = new List<Game>(gameCollection);
            foreach (string lambdaExpression in FilterCollection)
            {
                Func<Game, bool> gameFilterExpression = await CSharpScript.EvaluateAsync<Func<Game, bool>>(lambdaExpression, options);
                queriedGames = queriedGames.Where(gameFilterExpression).ToList();
            }

            DBQueries = new ObservableCollection<Game>( queriedGames);
            Games.ItemsSource = DBQueries;
            if(DBQueries.Count() == 0) { MessageBox.Show("No games match the query", "Ooops"); }

        }

        private void WishlistCommand(object sender, RoutedEventArgs e)
        {
            if (Games.SelectedItem != null)
            {
                //UserInUse.WishListGames.Add(Games.SelectedItem as Game);
                var s = Games.SelectedItems.Cast<Object>().ToArray();
                foreach(var i in s)
                {
                    UserInUse.WishListGames.Add(i as Game);
                }
                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                MessageBox.Show("Your selected game(s) has been added to your Games Wish List", "Success!!!");
            }
        }

        private void OwnedListCommand(object sender, RoutedEventArgs e)
        {
            if (Games.SelectedItem != null)
            {
                //UserInUse.OwnedGames.Add(Games.SelectedItem as Game);
                var s = Games.SelectedItems.Cast<Object>().ToArray();
                foreach (var i in s)
                {
                    UserInUse.OwnedGames.Add(i as Game);
                }
                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                MessageBox.Show("Your selected game has been added to your Games Owned List", "Success!!!");
            }        
        }

        private void Clear_Search(object sender, RoutedEventArgs e)
        {
            GameName.Text = "";
            GameRank.Text = "";
            GameGenre.SelectedItem = null;
            GamePublisher.SelectedItem = null;
            GamePlat.SelectedItem = null;
            GameYear.SelectedItem = null;
        }
    }
}
