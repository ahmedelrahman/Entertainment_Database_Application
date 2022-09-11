using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public List<UserAccount> UserList = new List<UserAccount>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<UserAccount>));

        public Login()
        {
            InitializeComponent();
            LoginVM logVM = new LoginVM(this);
            DataContext = logVM;
        }
        
    }
}
