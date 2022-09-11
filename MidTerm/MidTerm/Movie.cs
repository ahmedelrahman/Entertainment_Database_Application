using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MidTerm
{
    [XmlRoot(ElementName = "UserAccount")]
    public class Movies : IEntertain
    {
        [XmlAttribute(DataType = "string")]
        public string Name { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Year { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Genre { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Publisher { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Other_Sales { get; set; }
        [XmlAttribute(DataType = "string")]
        public string AudienceScore { get; set; }
        [XmlAttribute(DataType = "string")]
        public string RottenTomatoes { get; set; }
        [XmlAttribute(DataType = "string")]
        public string Global_Sales { get; set; }

        internal static Movies ParseFromFile(string line)
        {
            var columns = line.Split(',');

            return new Movies
            {
                Name = columns[0],
                Genre = columns[1],
                Publisher = columns[2],
                AudienceScore = columns[3],
                Other_Sales = columns[4],
                RottenTomatoes = columns[5],
                Global_Sales = columns[6],
                Year = columns[7]
            };
        }

        public override string ToString()
        {
            return $"{Name}, {Year}, {Genre}, {Publisher}, {Other_Sales}, {Global_Sales}, {AudienceScore}%, {RottenTomatoes}%";
        }
    }
}
