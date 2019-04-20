using System;
using BookLib.Models;

namespace BookLib
{
    internal class Book : AbstractItem , IBook
    {
        #region Properties
        public string Edition { get; set; }
        public string Author { get; set; }
        public BookCategory Genre { get; set;}
        #endregion

        #region Ctor
        public Book()
        {

        }
        public Book(string Author, BookCategory Genre, string Edition, string Name, int CopyNumber,DateTime PrintDate, double Price) 
            :base(Name,CopyNumber,PrintDate, Price)
        {
            this.Edition = Edition;
            this.Genre = Genre;
            this.Author = Author;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"{base.ToString()} Genre: {Genre} Edition: {Edition} Author: {Author} ISBN: {ISBN}";
        }
        #endregion
    }
}
