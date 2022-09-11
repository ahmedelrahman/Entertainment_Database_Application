using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MidTerm
{
    public class LoginVM : INotifyPropertyChanged
    {
        private Login tempLog;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public List<UserAccount> UserList = new List<UserAccount>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserAccount>));

        public LoginVM(Login log)
        {
            ReadInUsers();
            tempLog = log;
        }

        private void ReadInUsers()
        {
            string path = "Users.xml";

            if (File.Exists(path))
            {
                using (FileStream rs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    UserList = serializer.Deserialize(rs) as List<UserAccount>;
                }
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }
        }

        private void loginClicked(object obj)
        {
            UserAccount tempAct = UserList.FirstOrDefault(x => x.Login == tempLog.login.Text);
            if (tempAct?.Password == tempLog.txtPassword.Password) // is that null if not don't do this
            {
                MainWindow mw = new MainWindow(tempAct, UserList);
                tempLog.Close();
                mw.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Login credentials", "Login Error");
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginEvent == null)
                {
                    _loginEvent = new DelegateCommand(loginClicked);
                }

                return _loginEvent;
            }
        }
        DelegateCommand _loginEvent;

        private void newAccountClicked(object obj)
        {
            CreateAccount newAccount = new CreateAccount();
            CreateAccountVM newAccountVM = new CreateAccountVM(newAccount, UserList, false);
            newAccount.DataContext = newAccountVM;
            newAccount.ShowDialog();
        }

        public ICommand NewAcnCommand
        {
            get
            {
                if (_nacEvent == null)
                {
                    _nacEvent = new DelegateCommand(newAccountClicked);
                }

                return _nacEvent;
            }
        }
        DelegateCommand _nacEvent;

        private void cancelClicked(object obj)
        {
            Application.Current.Shutdown();
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelEvent == null)
                {
                    _cancelEvent = new DelegateCommand(cancelClicked);
                }

                return _cancelEvent;
            }
        }
        DelegateCommand _cancelEvent;

        private bool ValidateAllEntries()
        {
            bool loginvalid = string.IsNullOrWhiteSpace(tempLog.login.Text) ? false : ValidateForLetters(tempLog.login.Text);
            tempLog.login.Background = loginvalid ? Brushes.White : Brushes.Coral;
            tempLog.loginWarning.Visibility = loginvalid ? Visibility.Hidden : Visibility.Visible;

            if (string.IsNullOrWhiteSpace(tempLog.txtPassword.Password)) { return false; }

            return loginvalid;
        }

        // function validates for letters
        private bool ValidateForLetters(string tester)
        {
            return tester.Where(x => char.IsLetter(x)).Count() == tester.Length;
        }
    }
}
