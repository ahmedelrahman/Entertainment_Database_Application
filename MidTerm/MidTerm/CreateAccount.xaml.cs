using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace MidTerm
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public List<UserAccount> Users { get; set; }
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserAccount>));
        public bool IsEdit { get; set; }
        public UserAccount EditingUser { get; set; }

        public CreateAccount()
        {
            InitializeComponent();
        }

        //public CreateAccount(List<UserAccount> users, bool isEdit = false)
        //{
        //    InitializeComponent();
        //    Users = users;
        //    IsEdit = isEdit;
        //}

        //public CreateAccount(UserAccount user, List<UserAccount> users, bool isEdit = true)
        //{
        //    InitializeComponent();
        //    DeleteAccount.Visibility = Visibility.Visible;
        //    Users = users;
        //    EditingUser = user;
        //    IsEdit = isEdit;
        //}

        //private void addAccount(object sender, RoutedEventArgs e)
        //{
        //    if (ValidateAllEntries())
        //    {
        //        if (IsEdit)
        //        {
        //            UserAccount userToModify = Users.FirstOrDefault(x => x == EditingUser);
        //            userToModify.Login = UserName.Text;
        //            userToModify.Password = UserPassword.Password;
        //            userToModify.FirstName = FirstName.Text;
        //            userToModify.LastName = LastName.Text;

        //            string path = "Users.xml";
        //            if (File.Exists(path))
        //            {
        //                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
        //                {
        //                    serializer.Serialize(fs, Users);
        //                }
        //            }

        //            MessageBox.Show("Account has been Updated", "Success!");
        //            this.Close();
        //        }
        //        else
        //        {
        //            UserAccount act = new UserAccount(UserName.Text, UserPassword.Password, FirstName.Text, LastName.Text);
        //            Users.Add(act);

        //            string path = "Users.xml";
        //            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        //            {
        //                serializer.Serialize(fs, Users);
        //            }
        //            MessageBox.Show("Account has been created", "Success!");
        //            this.Close();
        //        }
        //    }
        //}

        //private void cancelButton(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private bool ValidateAllEntries()
        //{
        //    bool FirstNamevalid = string.IsNullOrWhiteSpace(FirstName.Text) ? false : ValidateForLetters(FirstName.Text);
        //    FirstName.Background = FirstNamevalid ? Brushes.White : Brushes.Coral;
        //    FirstNameWarning.Visibility = FirstNamevalid ? Visibility.Hidden : Visibility.Visible;

        //    if(FirstNamevalid == false)
        //    {
        //        MessageBox.Show("Your First Name can not include any space or any special characters", "Warning!");
        //    }

        //    bool LastNamevalid = string.IsNullOrWhiteSpace(LastName.Text) ? false : ValidateForLetters(LastName.Text);
        //    LastName.Background = LastNamevalid ? Brushes.White : Brushes.Coral;
        //    LastNameWarning.Visibility = LastNamevalid ? Visibility.Hidden : Visibility.Visible;

        //    if (LastNamevalid == false)
        //    {
        //        MessageBox.Show("Your Last Name can not include any space or any special characters", "Warning!");
        //    }

        //    bool UserNamevalid = string.IsNullOrWhiteSpace(UserName.Text) ? false : ValidateForLetters(UserName.Text);
        //    UserName.Background = UserNamevalid ? Brushes.White : Brushes.Coral;
        //    UserNameWarning.Visibility = UserNamevalid ? Visibility.Hidden : Visibility.Visible;

        //    if (UserNamevalid == false)
        //    {
        //        MessageBox.Show("Your Username can not include any space or any special characters", "Warning!");
        //    }

        //    if (string.IsNullOrWhiteSpace(UserPassword.Password)) { return false; }

        //    return (FirstNamevalid && UserNamevalid && LastNamevalid);
        //}

        //// function validates for letters
        //private bool ValidateForLetters(string tester)
        //{
        //    return tester.Where(x => char.IsLetter(x)).Count() == tester.Length;
        //}

        //private void DeleteAccountCommand(object sender, RoutedEventArgs e)
        //{
        //    if (IsEdit)
        //    {
        //        MessageBoxResult result = MessageBox.Show("Do you want to delete you account?", "Account Deletion", MessageBoxButton.YesNoCancel);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            MessageBox.Show("Deleting your account now!", "Deletion!!!");
        //            UserAccount userToModify = Users.FirstOrDefault(x => x == EditingUser);
        //            Users.Remove(userToModify);

        //            string path = "Users.xml";
        //            if (File.Exists(path))
        //            {
        //                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        //                {
        //                    serializer.Serialize(fs, Users);
        //                }
        //            }
        //            MessageBox.Show("You're account has been deleted.", "Goodbye!!!");
        //            Environment.Exit(1);

        //        }
        //        else if (result == MessageBoxResult.No)
        //        {
        //            MessageBox.Show("Good to have you back", "Hey There!!!");
        //        }
        //    }
        //}
    }
}
