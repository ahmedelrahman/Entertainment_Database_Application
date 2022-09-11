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
    public class UserDataDisplayVM : INotifyPropertyChanged
    {
        public UserAccount UserInUse { get; set; }
        public List<UserAccount> Users { get; set; }
        XmlSerializer xmler = new XmlSerializer(typeof(List<UserAccount>));
        UserDataDisplay temp = new UserDataDisplay();
        public UserDataDisplayVM(UserDataDisplay userDataDisplay, UserAccount user, List<UserAccount> users)
        {
            temp = userDataDisplay;
            temp.OwnedList.ItemsSource = user.OwnedGames;
            temp.WishList.ItemsSource = user.WishListGames;
            UserInUse = user;
            Users = users;
        }



        private void swapToOwnedClicked(object obj)
        {
            if (temp.WishList.SelectedItem != null)
            {
                Game tempOwnGame = UserInUse.WishListGames.FirstOrDefault(x => x == temp.WishList.SelectedItem);
                UserInUse.WishListGames.Remove(tempOwnGame);
                UserInUse.OwnedGames.Add(tempOwnGame);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedGames;
                temp.WishList.ItemsSource = UserInUse.WishListGames;
                MessageBox.Show("Selected game has swapped from Wish list to Owned List", "Success!!!");
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
                Game tempOwnGame = UserInUse.OwnedGames.FirstOrDefault(x => x == temp.OwnedList.SelectedItem);
                UserInUse.OwnedGames.Remove(tempOwnGame);
                UserInUse.WishListGames.Add(tempOwnGame);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedGames;
                temp.WishList.ItemsSource = UserInUse.WishListGames;
                MessageBox.Show("Selected game has swapped from Owned list to Wish List", "Success!!!");
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
            if (temp.Platform.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.WishListGames.Where(x => x.Platform == temp.Platform.SelectionBoxItem.ToString()).ToList();
                temp.WishList.ItemsSource = query1;
            }

            if (temp.Genre.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.WishListGames.Where(x => x.Genre == temp.Genre.SelectionBoxItem.ToString()).ToList();
                temp.WishList.ItemsSource = query1;
            }

            if (temp.Year.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.WishListGames.Where(x => x.Year == temp.Year.SelectionBoxItem.ToString()).ToList();
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
            if (temp.Platform.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.OwnedGames.Where(x => x.Platform == temp.Platform.SelectionBoxItem.ToString()).ToList();
                temp.OwnedList.ItemsSource = query1;
            }

            if (temp.Genre.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.OwnedGames.Where(x => x.Genre == temp.Genre.SelectionBoxItem.ToString()).ToList();
                temp.OwnedList.ItemsSource = query1;
            }

            if (temp.Year.SelectedIndex != -1)
            {
                List<Game> query1;
                query1 = UserInUse.OwnedGames.Where(x => x.Year == temp.Year.SelectionBoxItem.ToString()).ToList();
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
            temp.OwnedList.ItemsSource = UserInUse.OwnedGames;
            temp.WishList.ItemsSource = UserInUse.WishListGames;
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
                Game tempWishGame = UserInUse.WishListGames.FirstOrDefault(x => x == temp.WishList.SelectedItem);
                UserInUse.WishListGames.Remove(tempWishGame);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.WishList.ItemsSource = UserInUse.WishListGames;
                MessageBox.Show("Selected game has been deleted", "Success!!!");
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
                Game tempOwnGame = UserInUse.OwnedGames.FirstOrDefault(x => x == temp.OwnedList.SelectedItem);
                UserInUse.OwnedGames.Remove(tempOwnGame);

                string path = "Users.xml";
                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        xmler.Serialize(fs, Users);
                    }
                }
                temp.OwnedList.ItemsSource = UserInUse.OwnedGames;
                MessageBox.Show("Selected game has been deleted", "Success!!!");
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
