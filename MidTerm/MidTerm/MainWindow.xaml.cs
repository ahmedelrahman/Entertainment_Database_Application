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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MidTerm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public UserAccount CurrentUser { get; set; }
        public List<UserAccount> ListOfUsers { get; set; }

        public MainWindow(UserAccount currentUser, List<UserAccount> listOfUsers)
        {
            InitializeComponent();            
            CurrentUser = currentUser;
            ListOfUsers = listOfUsers;
            MainWindowVM mainWindow = new MainWindowVM(CurrentUser, ListOfUsers);
            DataContext = mainWindow;
            UserCreds.Header = $"Logged in as: {currentUser.Login}";
        }

        //private void UpdateSettingsCommand(object sender, RoutedEventArgs e)
        //{
        //    //CreateAccount actEdit = new CreateAccount(CurrentUser, ListOfUsers);
        //    //actEdit.ParentContainer.Title = "Account Edit";
        //    //actEdit.ShowDialog();
        //    //actEdit.UserName.Text = CurrentUser.Login;
        //    //actEdit.UserPassword.Password = CurrentUser.Password;
        //    //actEdit.FirstName.Text = CurrentUser.FirstName;
        //    //actEdit.LastName.Text = CurrentUser.LastName;
        //}
    }
}
