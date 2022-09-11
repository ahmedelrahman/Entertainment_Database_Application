using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MidTerm
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        

        List<Game> MainGamesCollection = ProcessGamesFile("vgsalesGlobale.csv");
        List<Movies> MainMoviesCollection = ProcessMoviesFile("movies.csv");

        public UserAccount CurrentUser { get; set; }
        public List<UserAccount> ListOfUsers { get; set; }

        public MainWindowVM(UserAccount currentUser, List<UserAccount> listOfUsers)
        {
            //InitializeComponent();
            CurrentUser = currentUser;
            ListOfUsers = listOfUsers;
        }

        private static List<Game> ProcessGamesFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Game.ParseFromFile).ToList();
        }

        private static List<Movies> ProcessMoviesFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Movies.ParseFromFile).ToList();
        }

        private void ViewGameListCommand(object obj)
        {
            MainDataBase mainData = new MainDataBase(MainGamesCollection);
            mainData.ShowDialog();
        }

        public ICommand ViewGameList
        {
            get
            {
                if (_openGameList == null)
                {
                    _openGameList = new DelegateCommand(ViewGameListCommand);
                }

                return _openGameList;
            }
        }
        DelegateCommand _openGameList;

        private void ViewMovieListCommand(object obj)
        {
            MovieDataBaseVM mDBVM = new MovieDataBaseVM(MainMoviesCollection);
            MovieDataBase MDB = new MovieDataBase();
            MDB.DataContext = mDBVM;
            MDB.ShowDialog();
        }

        public ICommand ViewMovieCommand
        {
            get
            {
                if (_openMovieList == null)
                {
                    _openMovieList = new DelegateCommand(ViewMovieListCommand);
                }

                return _openMovieList;
            }
        }
        DelegateCommand _openMovieList;


        private void SearchGameCommand(object obj)
        {
            SearchGame searchData = new SearchGame(MainGamesCollection, CurrentUser, ListOfUsers);
            searchData.ShowDialog();
        }

        public ICommand SearchGame
        {
            get
            {
                if (_search == null)
                {
                    _search = new DelegateCommand(SearchGameCommand);
                }

                return _search;
            }
        }
        DelegateCommand _search;

        private void searchMovieCommand(object obj)
        {
            SearchMovie searchMovieData = new SearchMovie(MainMoviesCollection, CurrentUser, ListOfUsers);
            searchMovieData.ShowDialog();
        }

        public ICommand SearchMovieCommand
        {
            get
            {
                if (_searchMovie == null)
                {
                    _searchMovie = new DelegateCommand(searchMovieCommand);
                }

                return _searchMovie;
            }
        }
        DelegateCommand _searchMovie;

        private void EditPersonalDataCommand(object obj)
        {
            UserDataDisplay userDataDisplay = new UserDataDisplay();
            UserDataDisplayVM userDataVM = new UserDataDisplayVM(userDataDisplay, CurrentUser, ListOfUsers);
            userDataDisplay.DataContext = userDataVM;
            userDataDisplay.ShowDialog();
        }

        public ICommand EditPersonalData
        {
            get
            {
                if (_edit == null)
                {
                    _edit = new DelegateCommand(EditPersonalDataCommand);
                }

                return _edit;
            }
        }
        DelegateCommand _edit;

        private void EditMovieDataCommand(object obj)
        {
            UserMovieDataDisplay userDataDisplay = new UserMovieDataDisplay();
            UserMovieDataDisplayVM userDataVM = new UserMovieDataDisplayVM(userDataDisplay, CurrentUser, ListOfUsers);
            userDataDisplay.DataContext = userDataVM;
            userDataDisplay.ShowDialog();
        }

        public ICommand EditMovieData
        {
            get
            {
                if (_editMovie == null)
                {
                    _editMovie = new DelegateCommand(EditMovieDataCommand);
                }

                return _editMovie;
            }
        }
        DelegateCommand _editMovie;

        private void updateSettingsClick(object obj)
        {
            CreateAccount editAccount = new CreateAccount();
            CreateAccountVM editAccountVM = new CreateAccountVM(editAccount, CurrentUser, ListOfUsers, true);
            editAccount.DataContext = editAccountVM;
            editAccount.ShowDialog();
        }

        public ICommand UpdateSettingsCommand
        {
            get
            {
                if (_updateSetting == null)
                {
                    _updateSetting = new DelegateCommand(updateSettingsClick);
                }

                return _updateSetting;
            }
        }
        DelegateCommand _updateSetting;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        
    }
}

