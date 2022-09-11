using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MidTerm
{
    [XmlRoot(ElementName = "UserAccount")]
    public class UserAccount
    {
        [XmlAttribute(DataType = "string")]
        public string Login { get; set; }

        [XmlAttribute(DataType = "string")]
        public string Password { get; set; }

        [XmlAttribute(DataType = "string")]
        public string FirstName { get; set; }

        [XmlAttribute(DataType = "string")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "GameWishList")]
        public ObservableCollection<Game> WishListGames = new ObservableCollection<Game>();

        [XmlElement(ElementName = "GameOwnedList")]
        public ObservableCollection<Game> OwnedGames = new ObservableCollection<Game>();

        [XmlElement(ElementName = "MovieWishList")]
        public ObservableCollection<Movies> WishListMovies = new ObservableCollection<Movies>();

        [XmlElement(ElementName = "MovieOwnedList")]
        public ObservableCollection<Movies> OwnedMovies = new ObservableCollection<Movies>();

        public UserAccount() { }

        public UserAccount(string login, string password, string firstname, string lastname)
        {
            Login = login;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
