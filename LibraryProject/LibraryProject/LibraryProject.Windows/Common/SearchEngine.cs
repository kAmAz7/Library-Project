using BookLib;
using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Common
{
    internal class SearchEngine
    {
        #region Data Members
        ItemManager itemManager = ItemManager.GetInstance();
        List<IItem> _items = new List<IItem>();
        #endregion

        #region Public Methods
        public List<IItem> SearchExecuter(ItemType enumType, string category, string inputText)
        { 
            switch (category)
            {
                case ("All"):
                    return _items = itemManager.GetAllItems(enumType);
                case ("Name"):
                    return _items = itemManager.GetItemsByName(inputText, enumType);
                case ("Author"):
                    {
                        if (enumType == ItemType.Book)
                            return _items = itemManager.GetBooksByAuthor(inputText);
                        else
                            throw new ArgumentException();
                    }
                case ("Editor"):
                    {
                        if (enumType == ItemType.Journal)
                            return _items = itemManager.GetJournalsByEditor(inputText);
                        else
                            throw new ArgumentException();
                    }
                case ("Copy Number"):
                    {
                        int copyNumber;
                        if (int.TryParse(inputText, out copyNumber) && copyNumber > 0)
                            return _items = itemManager.GetItemsByCopyNumber(copyNumber, enumType);
                        else
                            return _items = null;
                    }
                case ("Price"):
                    {
                        double price;
                        if (double.TryParse(inputText, out price) && price > 0)
                            return _items = itemManager.GetItemsByPrice(price, enumType);
                        else
                            return _items = null;
                    }
                default:
                    throw new ArgumentException();
            }
        }
        #endregion
    }
}
