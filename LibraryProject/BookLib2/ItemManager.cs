using BookLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLib
{

    public class ItemManager
    {
        #region Data Members
        ItemCollection _collection;
        #endregion

        #region Properties
        public static IJournal EmptyJournal
        { //Create an empty template to fill in the user interface
            get { return new Journal(); }
        }

        public static IBook EmptyBook
        { //Create an empty template to fill in the user interface
            get { return new Book(); }
        }
        #endregion

        #region Ctor
        private static ItemManager _instance;
        public static ItemManager GetInstance()
        {
            if (_instance == null)
                _instance = new ItemManager();
            return _instance;
        }

        private ItemManager()
        {
            _collection = new ItemCollection(); 
        }
        #endregion

        #region Public Methods
       
        public void RegisterNewBook(IBook newBook)
        {  //Accept the template after filling and creating a new item
            newBook = new Book(newBook.Author, newBook.Genre, newBook.Edition, newBook.Name,
                               newBook.CopyNumber, newBook.PrintDate, newBook.Price);
            _collection.AddItem(newBook);
        }

        public void RegisterNewJournal(IJournal newJournal)
        { //Accept the template after filling and creating a new item
            newJournal = new Journal(newJournal.Genre, newJournal.Editor, newJournal.Name,
                                     newJournal.CopyNumber, newJournal.PrintDate, newJournal.Price);
            _collection.AddItem(newJournal);
        }

        public bool RemoveItem(IItem itemToRemove)
        {
            return _collection.RemoveItemByISBN(itemToRemove.ISBN.ToString());
        }

        public List<IItem> GetAllItems(ItemType type = ItemType.All)
        { //Send all existing information according to the selected type
            switch (type)
            {
                case ItemType.All:
                    return _collection.Items;
                case ItemType.Book:
                    return _collection.Items.Where(item => item is IBook)?.ToList();
                case ItemType.Journal:
                    return _collection.Items.Where(item => item is IJournal)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }

        }

        public List<IItem> GetItemsByName(string name, ItemType type)
        {
            switch (type)
            {
                case ItemType.All:
                    return _collection.Items.Where(item => item.Name == name)?.ToList();
                case ItemType.Book:
                    return _collection.Items.Where(item => item is IBook && item.Name == name)?.ToList();
                case ItemType.Journal:
                    return _collection.Items.Where(item => item is IJournal && item.Name == name)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }
        }

        public List<IItem> GetItemsByCopyNumber(int copyNumber, ItemType type)
        {
            switch (type)
            {
                case ItemType.All:
                    return _collection.Items.Where(item => item.CopyNumber == copyNumber)?.ToList();
                case ItemType.Book:
                    return _collection.Items.Where(item => item is IBook && item.CopyNumber == copyNumber)?.ToList();
                case ItemType.Journal:
                    return _collection.Items.Where(item => item is IJournal && item.CopyNumber == copyNumber)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }
        }

        public List<IItem> GetItemsByPrice(double price, ItemType type)
        {
            switch (type)
            {
                case ItemType.All:
                    return _collection.Items.Where(item => item.Price == price)?.ToList();
                case ItemType.Book:
                    return _collection.Items.Where(item => item is IBook && item.Price == price)?.ToList();
                case ItemType.Journal:
                    return _collection.Items.Where(item => item is IJournal && item.Price == price)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }
        }

        public List<IItem> GetBooksByAuthor(string Author)
        {
            return _collection.Items.Select(item => item as IBook).Where(item => item?.Author == Author).ToList<IItem>();
        }

        public List<IItem> GetJournalsByEditor(string Editor)
        {
            return _collection.Items.Select(item => item as IJournal).Where(item => item?.Editor == Editor).ToList<IItem>();

        }

        public void RentNewItem(Guid userId,IItem itemToRent)
        { 
            bool isRent = true;
            if (_collection.SearchUser(userId))
            {
                _collection.UpdateCopyNumber(itemToRent, isRent);
                _collection._rentCollection[userId].Add(itemToRent);
            }
            else
            {
                List<IItem> tmp = new List<IItem>();
                tmp.Add(itemToRent);
                _collection.UpdateCopyNumber(itemToRent, isRent);
                _collection._rentCollection.Add(userId,tmp);
            }
        }

        public List<IItem> ShowRentedItems(Guid _userID)
        {
                return _collection._rentCollection[_userID].ToList();
        }

        public void ReturnItem(Guid userId, IItem itemToReturn)
        { //Returns an item that is in rental mode
            bool isRent = false;
            _collection.UpdateCopyNumber(itemToReturn, isRent);
            Guid ISBN = itemToReturn.ISBN;
            int tmp =_collection._rentCollection[userId].FindIndex(item => item.ISBN == ISBN);
            _collection._rentCollection[userId].RemoveAt(tmp);
        }
        #endregion
    }
}
