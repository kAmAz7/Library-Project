using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib.Models
{
    public enum BookCategory
    {
        None,
        Drama,
        Comedy,
        Romance,
        Horror,
        SinceFiction,
        biography
    }

    public enum JournalCategory
    {
        None,
        Medical,
        HiTech,
        Since,
        Fashion
    }

    public enum ItemType
    {
        All,
        Book,
        Journal
    }
}
