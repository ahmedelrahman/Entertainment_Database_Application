using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MidTerm
{
    class UserMovieDataDisplayVM : INotifyPropertyChanged
    {
        public UserAccount UserInUse { get; set; }
        public List<UserAccount> Users { get; set; }
        XmlSerializer xmler = new XmlSerializer(typeof(List<UserAccount>));
        UserMovieDataDisplay temp = new UserMovieDataDisplay();
        public UserMovieDataDisplayVM(UserMovieDataDisplay userMovieDataDisplay, UserAccount user, List<UserAccount> users)
        {
            temp = userMovieDataDisplay;
            temp.OwnedList.ItemsSource = user.OwnedMovies;
            temp.WishList.ItemsSource = user.WishListMovies;
            UserInUse = user;
            Users = users;
        }
        private void swapToOwnedClicked(object obj)
        {
            if (temp.WishList.SelectedItem != null)
            {
                Movies tempOwnMovie = UserInUse.WishListMovies.FirstOrDefault(x => x == temp.WishList.SelectedItem);
                UserInUse.WishListMovies.Remove(tempOwnMovie);
                UserInUse.OwnedMovies.Add(tempOwnMovie);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedMovies;
                temp.WishList.ItemsSource = UserInUse.WishListMovies;
                MessageBox.Show("Selected Movie has swapped from Wish list to Owned List", "Success!!!");
            }
        }

        public ICommand SwapToOwnedCommand
        {
            get
            {
                if (_swapToOwnedEvent == null)
                {
                    _swapToOwnedEvent = new DelegateCommand(swapToOwnedClicked);
                }

                return _swapToOwnedEvent;
            }
        }
        DelegateCommand _swapToOwnedEvent;

        private void swapToWishClicked(object obj)
        {
            if (temp.OwnedList.SelectedItem != null)
            {
                Movies tempOwnMovie = UserInUse.OwnedMovies.FirstOrDefault(x => x == temp.OwnedList.SelectedItem);
                UserInUse.OwnedMovies.Remove(tempOwnMovie);
                UserInUse.WishListMovies.Add(tempOwnMovie);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedMovies;
                temp.WishList.ItemsSource = UserInUse.WishListMovies;
                MessageBox.Show("Selected Movie has swapped from Owned list to Wish List", "Success!!!");
            }
        }

        public ICommand SwapToWishCommand
        {
            get
            {
                if (_swapToWishEvent == null)
                {
                    _swapToWishEvent = new DelegateCommand(swapToWishClicked);
                }

                return _swapToWishEvent;
            }
        }
        DelegateCommand _swapToWishEvent;

        private void UpdateWishClicked(object obj)
        {
            if (temp.Genre.SelectedIndex != -1)
            {
                List<Movies> query1;
                query1 = UserInUse.WishListMovies.Where(x => x.Genre == temp.Genre.SelectionBoxItem.ToString()).ToList();
                temp.WishList.ItemsSource = query1;
            }

            if (temp.Year.SelectedIndex != -1)
            {
                List<Movies> query1;
                query1 = UserInUse.WishListMovies.Where(x => x.Year == temp.Year.SelectionBoxItem.ToString()).ToList();
                temp.WishList.ItemsSource = query1;
            }
        }

        public ICommand UpdateWishCommand
        {
            get
            {
                if (_updateWishEvent == null)
                {
                    _updateWishEvent = new DelegateCommand(UpdateWishClicked);
                }

                return _updateWishEvent;
            }
        }
        DelegateCommand _updateWishEvent;

        private void UpdateOwnedClicked(object obj)
        {
            if (temp.Genre.SelectedIndex != -1)
            {
                List<Movies> query1;
                query1 = UserInUse.OwnedMovies.Where(x => x.Genre == temp.Genre.SelectionBoxItem.ToString()).ToList();
                temp.OwnedList.ItemsSource = query1;
            }

            if (temp.Year.SelectedIndex != -1)
            {
                List<Movies> query1;
                query1 = UserInUse.OwnedMovies.Where(x => x.Year == temp.Year.SelectionBoxItem.ToString()).ToList();
                temp.OwnedList.ItemsSource = query1;
            }
        }

        public ICommand UpdateOwnedCommand
        {
            get
            {
                if (_updateOwnedEvent == null)
                {
                    _updateOwnedEvent = new DelegateCommand(UpdateOwnedClicked);
                }

                return _updateOwnedEvent;
            }
        }
        DelegateCommand _updateOwnedEvent;

        private void clearClicked(object obj)
        {
            temp.OwnedList.ItemsSource = UserInUse.OwnedMovies;
            temp.WishList.ItemsSource = UserInUse.WishListMovies;
        }

        public ICommand ClearSearchCommand
        {
            get
            {
                if (_clearEvent == null)
                {
                    _clearEvent = new DelegateCommand(clearClicked);
                }

                return _clearEvent;
            }
        }
        DelegateCommand _clearEvent;

        private void wishDeleteClicked(object obj)
        {
            if (temp.WishList.SelectedItem != null)
            {
                Movies tempWishMovie = UserInUse.WishListMovies.FirstOrDefault(x => x == temp.WishList.SelectedItem);
                UserInUse.WishListMovies.Remove(tempWishMovie);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.WishList.ItemsSource = UserInUse.WishListMovies;
                MessageBox.Show("Selected Movie has been deleted", "Success!!!");
            }
        }

        public ICommand WishDeleteCommand
        {
            get
            {
                if (_wishDeleteEvent == null)
                {
                    _wishDeleteEvent = new DelegateCommand(wishDeleteClicked);
                }

                return _wishDeleteEvent;
            }
        }
        DelegateCommand _wishDeleteEvent;

        private void ownDeleteClicked(object obj)
        {
            if (temp.OwnedList.SelectedItem != null)
            {
                Movies tempOwnMovie = UserInUse.OwnedMovies.FirstOrDefault(x => x == temp.OwnedList.SelectedItem);
                UserInUse.OwnedMovies.Remove(tempOwnMovie);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedMovies;
                MessageBox.Show("Selected Movie has been deleted", "Success!!!");
            }
        }

        public ICommand OwnDeleteCommand
        {
            get
            {
                if (_ownDeleteEvent == null)
                {
                    _ownDeleteEvent = new DelegateCommand(ownDeleteClicked);
                }

                return _ownDeleteEvent;
            }
        }
        DelegateCommand _ownDeleteEvent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}