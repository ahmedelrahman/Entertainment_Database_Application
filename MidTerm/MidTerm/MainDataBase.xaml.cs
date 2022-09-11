using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MidTerm
{
    /// <summary>
    /// Interaction logic for MainDataBase.xaml
    /// </summary>
    public partial class MainDataBase : Window
    {
        public ObservableCollection<Game> DBSource { get; set; } = new ObservableCollection<Game>();

        public MainDataBase(List<Game> collection)
        {
            InitializeComponent();
            DataContext = this;
            DBSource = new ObservableCollection<Game>(collection);
        }
    }
}
