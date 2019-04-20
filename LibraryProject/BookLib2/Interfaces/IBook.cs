using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib;
using BookLib.Models;

namespace BookLib
{
    public interface IBook : IItem
    {
        string Author { get; set; }
        string Edition { get; set; }
        BookCategory Genre { get; set;}
    }
}
