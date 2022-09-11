using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MidTerm
{
    [XmlRoot(ElementName = "UserAccount")]
    public class Game : IEntertain
    {
        [XmlAttribute(DataType = "string")]
        public string Rank { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Name { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Platform { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Year { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Genre { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Publisher { get; set; }
        [XmlAttribute(DataType = "string")]
        public string NA_Sales { get; set; }
        [XmlAttribute(DataType = "string")]
        public string EU_Sales { get; set; }
        [XmlAttribute(DataType = "string")]
        public string JP_Sales { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Other_Sales { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Global_Sales { get; set; }

        internal static Game ParseFromFile(string line)
        {
            var columns = line.Split(',');

            return new Game
            {
                Rank = columns[0],
                Name = columns[1],
                Platform = columns[2],
                Year = columns[3],
                Genre = columns[4],
                Publisher = columns[5],
                NA_Sales = columns[6],
                EU_Sales = columns[7],
                JP_Sales = columns[8],
                Other_Sales = columns[9],
                Global_Sales = columns[10]
            };
        }

        public override string ToString()
        {
            return $"{Rank}, {Name}, {Platform}, {Year}, {Genre}, {Publisher}, {NA_Sales}, {EU_Sales}, {JP_Sales}, {Other_Sales}, {Global_Sales}";
        }
    }
}
