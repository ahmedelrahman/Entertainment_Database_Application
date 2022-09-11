using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MidTerm
{
    public class CreateAccountVM : INotifyPropertyChanged
    {
        public List<UserAccount> Users { get; set; }
        private CreateAccount tempAcc;
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserAccount>));
        public bool IsEdit { get; set; }
        public UserAccount EditingUser { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public CreateAccountVM(CreateAccount cA, List<UserAccount> users, bool isEdit = false)
        {
            tempAcc = cA;
            Users = users;
            IsEdit = isEdit;
        }

        public CreateAccountVM(CreateAccount cA, UserAccount user, List<UserAccount> users, bool isEdit = true)
        {
            tempAcc = cA;
            tempAcc.DeleteAccount.Visibility = Visibility.Visible; //****************
            Users = users;
            EditingUser = user;
            IsEdit = isEdit;
        }

        private void submitClicked(object obj)
        {
            if (ValidateAllEntries())
            {
                if (IsEdit)
                {
                    UserAccount userToModify = Users.FirstOrDefault(x => x == EditingUser);
                    userToModify.Login = tempAcc.UserName.Text;
                    userToModify.Password = tempAcc.UserPassword.Password;
                    userToModify.FirstName = tempAcc.FirstName.Text;
                    userToModify.LastName = tempAcc.LastName.Text;

                    string path = "Users.xml";
                    if (File.Exists(path))
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
                        {
                            serializer.Serialize(fs, Users);
                        }
                    }

                    MessageBox.Show("Account has been Updated", "Success!");
                    tempAcc.Close();
                }
                else
                {
                    UserAccount act = new UserAccount(tempAcc.UserName.Text, tempAcc.UserPassword.Password, tempAcc.FirstName.Text, tempAcc.LastName.Text);
                    Users.Add(act);

                    string path = "Users.xml";
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                    {
                        serializer.Serialize(fs, Users);
                    }
                    MessageBox.Show("Account has been created", "Success!");
                    tempAcc.Close();
                }
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if (_subEvent == null)
                {
                    _subEvent = new DelegateCommand(submitClicked);
                }

                return _subEvent;
            }
        }
        DelegateCommand _subEvent;

        private void cancelClicked(object obj)
        {
            tempAcc.Close();
        }

        public ICommand AccCancelCommand
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
            bool FirstNamevalid = string.IsNullOrWhiteSpace(tempAcc.FirstName.Text) ? false : ValidateForLetters(tempAcc.FirstName.Text);
            tempAcc.FirstName.Background = FirstNamevalid ? Brushes.White : Brushes.Coral;
            tempAcc.FirstNameWarning.Visibility = FirstNamevalid ? Visibility.Hidden : Visibility.Visible;

            if (FirstNamevalid == false)
            {
                MessageBox.Show("Your First Name can not include any space or any special characters", "Warning!");
            }

            bool LastNamevalid = string.IsNullOrWhiteSpace(tempAcc.LastName.Text) ? false : ValidateForLetters(tempAcc.LastName.Text);
            tempAcc.LastName.Background = LastNamevalid ? Brushes.White : Brushes.Coral;
            tempAcc.LastNameWarning.Visibility = LastNamevalid ? Visibility.Hidden : Visibility.Visible;

            if (LastNamevalid == false)
            {
                MessageBox.Show("Your Last Name can not include any space or any special characters", "Warning!");
            }

            bool UserNamevalid = string.IsNullOrWhiteSpace(tempAcc.UserName.Text) ? false : ValidateForLetters(tempAcc.UserName.Text);
            tempAcc.UserName.Background = UserNamevalid ? Brushes.White : Brushes.Coral;
            tempAcc.UserNameWarning.Visibility = UserNamevalid ? Visibility.Hidden : Visibility.Visible;

            if (UserNamevalid == false)
            {
                MessageBox.Show("Your Username can not include any space or any special characters", "Warning!");
            }

            if (string.IsNullOrWhiteSpace(tempAcc.UserPassword.Password)) { return false; }

            return (FirstNamevalid && UserNamevalid && LastNamevalid);
        }

        // function validates for letters
        private bool ValidateForLetters(string tester)
        {
            return tester.Where(x => char.IsLetter(x)).Count() == tester.Length;
        }
    }
}
