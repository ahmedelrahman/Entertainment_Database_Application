using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTerm
{
    interface IEntertain
    {
        string Name { get; set; }
        string Genre { get; set; }
        string Year { get; set; }
        string Publisher { get; set; }
        string Global_Sales { get; set; }
    }
}
