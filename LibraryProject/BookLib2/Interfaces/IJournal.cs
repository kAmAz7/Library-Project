using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLib;
using BookLib.Models;

namespace BookLib
{
    public interface IJournal : IItem
    {
        string Editor { get; set; }
        JournalCategory Genre { get; set; }
    }
}
