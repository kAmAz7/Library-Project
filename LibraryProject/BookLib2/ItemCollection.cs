using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLib
{
    public class ItemCollection
    {
        #region Data Members
        private readonly Dictionary<Guid, IItem> _items = new Dictionary<Guid, IItem>();
        internal Dictionary<Guid, List<IItem>> _rentCollection = new Dictionary<Guid, List<IItem>>();  
        #endregion

        #region Ctor
        public ItemCollection()
        {
            IBook test = new Book("Ayn Rand", Models.BookCategory.biography, "A2", "The Fountainhead", 3, DateTime.Now, 100);
            AddItem(test);
            
        }
        #endregion

        #region Properties
        public List<IItem> Items
        {
            get { return _items.Values.ToList();}
        }
        #endregion

        #region Public Methods

        
        internal void AddItem(IItem item)
        {//Validates the item and adds it to the collection
            Exception ex;
            if (ValidateItem(item, out ex))
                _items.Add(item.ISBN,item);
            else
                throw ex;
        }

        internal bool RemoveItemByISBN(string ISBN)
        {
            Guid temp = parseISBN(ISBN); //Checks that ISBN exists and correct
            return _items.Remove(temp);
        }

        internal bool SearchUser(Guid userId)
        { //Checks whether the user already exists in the list
            return (_rentCollection.ContainsKey(userId));
        }

        internal void UpdateCopyNumber(IItem itemToRent, bool isRent)
        { //Updates the number of copies of the book by rental or return action
            Guid ISBN = itemToRent.ISBN;
            if (isRent)
                _items[ISBN].CopyNumber--;
            else
                _items[ISBN].CopyNumber++;
        }
        #endregion

        #region Private Methods
        private Guid parseISBN(string ISBN)
        {
            if (string.IsNullOrWhiteSpace(ISBN))
                throw new ArgumentNullException("ISBN is null or empty");
            Guid temp;
            if (!Guid.TryParse(ISBN, out temp))
                throw new ArgumentException("ISBN is not valid");
            return temp;
        }

        private bool ValidateItem(IItem item, out Exception ex)
        {
            if (item == null)
            {
                ex = new NullReferenceException("Can't add null item");
                return false;
            }

            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(item.Name)) sb.Append(nameof(item.Name) + " ");
            if (item.CopyNumber <= 0) sb.Append(nameof(item.CopyNumber) + " ");
            if (item.ISBN == Guid.Empty) sb.Append(nameof(item.ISBN) + " ");
            if (item.Price < 0) sb.Append(nameof(item.Price) + " ");
            if (item.PrintDate == default(DateTime)) sb.Append(nameof(item.PrintDate));
            if (sb.Length == 0)
            {
                ex = null;
                return true;
            }
            else
                throw new AbstractInitializationExaption("Must initialize properties: " + sb.ToString());
        }
        #endregion
    }
}
