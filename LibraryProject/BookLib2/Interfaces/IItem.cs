using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public interface IItem
    {
         string Name { get; set; }
         int CopyNumber { get; set; } 
         Guid ISBN { get;} 
         DateTime PrintDate { get; set; } 
         double Price { get; set; }
    }
}
