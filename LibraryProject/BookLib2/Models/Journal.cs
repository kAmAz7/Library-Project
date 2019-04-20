using BookLib.Models;
using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    internal class Journal : AbstractItem, IJournal
    {
        #region Properties
        public JournalCategory Genre { get; set; }
        public string Editor{ get; set; }
        #endregion

        #region Ctor
        public Journal()
        {

        }
        public Journal(JournalCategory Genre,string Editor, string Name, int CopyNumber, DateTime PrintDate, double Price)
            : base(Name, CopyNumber, PrintDate,Price)
        {
            this.Genre = Genre;
            this.Editor = Editor;      
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"{base.ToString()} Genre: {Genre} Editior: {Editor} ISBN: {ISBN}";
        }
        #endregion
    }
}
