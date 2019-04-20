using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    internal abstract class AbstractItem :IItem
    {
        #region Data Members
        private int _copyNumber;
        private DateTime _printDate;
        private double _price;
        #endregion

        #region Properties
        public string Name { get; set; }
        public Guid ISBN { get; }
        public int CopyNumber
        {
            get { return _copyNumber; }
            set
            {
                if (value < 0) _copyNumber = 0;
                else _copyNumber = value;
            }
        }

        public DateTime PrintDate
        {
            get { return _printDate; }
            set
            {
                if (value > DateTime.Now) _printDate = DateTime.Now;
                else _printDate = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 20) _price = 20;
                else _price = value;
            }
        }

        #endregion

        #region Ctor
        public AbstractItem()
        {

        }
        public AbstractItem(string Name, int CopyNumber, DateTime PrintDate, double Price)
        {
            this.Price = Price;
            this.Name = Name;
            this.CopyNumber = CopyNumber;
            ISBN = Guid.NewGuid();
            _printDate = PrintDate;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"Item Name: {Name} Price: {Price} Number of Copies: {CopyNumber} Printed Date: {PrintDate} ";
        }
        #endregion
    }
}
