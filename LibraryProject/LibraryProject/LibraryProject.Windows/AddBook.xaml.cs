using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BookLib;
using BookLib.Models;
using Windows.UI.Popups;

namespace LibraryProject
{
    public sealed partial class AddBook : Page
    {
        #region Data Members
        ItemManager _manager = ItemManager.GetInstance();
        #endregion

        #region Ctor
        public AddBook()
        {
            this.InitializeComponent();
            typeCbox.ItemsSource = Enum.GetValues(typeof(ItemType)).Cast<ItemType>(); 
            typeCbox.SelectedItem = ItemType.All; 
            editionTbox.IsEnabled = false;
        }
        #endregion

        #region Events
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeeMainPage));
        }

        private void typeCbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            switch (typeCbox.SelectedItem.ToString())
            {
                case ("Book"):
                    {
                        editionTbox.IsEnabled = true;
                        categoryCbox.ItemsSource = Enum.GetValues(typeof(BookCategory)).Cast<BookCategory>();
                        categoryCbox.SelectedItem = BookCategory.biography;

                        break;
                    }
                case ("Journal"):
                    {
                        editionTbox.IsEnabled = false;
                        categoryCbox.ItemsSource = Enum.GetValues(typeof(JournalCategory)).Cast<JournalCategory>();
                        categoryCbox.SelectedItem = JournalCategory.Fashion;

                        break;
                    }
                case ("All"):
                    {
                        editionTbox.IsEnabled = false;
                        categoryCbox.ItemsSource = Enum.GetValues(typeof(BookCategory)).Cast<BookCategory>();
                        categoryCbox.SelectedItem = BookCategory.None;

                        break;
                    }
                default:
                    break;
            }

        }

        private async void createBtn_Click(object sender, RoutedEventArgs e)
        {
            string itemType = typeCbox.SelectedItem.ToString();
            ItemType enumItemType;
            Enum.TryParse(itemType, out enumItemType);
            string Category = categoryCbox.SelectedItem.ToString();
            string name = nameTbox.Text;
            double price;
            double.TryParse(priceTbox.Text, out price);
            int copyNumber;
            int.TryParse(copyTbox.Text, out copyNumber);
            string author = authorTbox.Text;
            string edition = editionTbox.Text;
            DateTime printDate = printDateDpick.Date.DateTime;

            if (enumItemType == ItemType.All)
                erorMessageTbl.Text = "Please select Item's type";
            else if (Category == "None")
                erorMessageTbl.Text = "Please select a category";
            else if (string.IsNullOrWhiteSpace(name))
                erorMessageTbl.Text = "Please enter Item's name";
            else if (price < 20)
                erorMessageTbl.Text = "Price must be at least '20'";
            else if (copyNumber <= 0)
                erorMessageTbl.Text = "Copy Number must be at least '1'";
            else if (printDate > DateTime.Now)
                erorMessageTbl.Text = "Date can't be greater than the current date";
            else if (string.IsNullOrWhiteSpace(author))
                erorMessageTbl.Text = "Please enter Author's name";
            else if (itemType == "Book" && string.IsNullOrWhiteSpace(edition))
                erorMessageTbl.Text = "Please enter an Edition";

            else try
            {
                if (enumItemType == ItemType.Book)
                {
                    BookCategory enumCategory;
                    Enum.TryParse(Category, out enumCategory);

                    IBook newBook = CreateNewBookData(enumCategory, name, price,
                                    copyNumber, author, printDate, edition);
                    _manager.RegisterNewBook(newBook);

                    await new MessageDialog("Successfully added a new Book!!").ShowAsync();
                    Frame.Navigate(typeof(EmployeeMainPage));
                }
                else
                {
                    JournalCategory enumCategory;
                    Enum.TryParse(Category, out enumCategory);

                    IJournal newJournal = CreateNewJournalData(enumCategory, name, price,
                                          copyNumber, printDate, edition, author);
                    _manager.RegisterNewJournal(newJournal);

                    await new MessageDialog("Successfully added a new Journal!!").ShowAsync();
                    Frame.Navigate(typeof(EmployeeMainPage));
                }
            }
            catch (NullReferenceException)
            {
                await new MessageDialog("Requested Item couldn't be created!! \nAll data must be filled in!!").ShowAsync();
            }
            catch (AbstractInitializationExaption ex)
            {
                await new MessageDialog(ex.ToString()).ShowAsync();
            }
        }
        #endregion

        #region Private Methods
        private IBook CreateNewBookData(BookCategory BookChosenCategory, string name,double price,
            int copyNumber,string author,DateTime printDate,string edition)
        { 
            IBook newBook = ItemManager.EmptyBook;
            newBook.Genre = BookChosenCategory;
            newBook.Name = name;
            newBook.Price = price;
            newBook.CopyNumber = copyNumber;
            newBook.Author = author;
            newBook.Edition = edition;
            newBook.PrintDate = printDate;

            return newBook;
        }

        private IJournal CreateNewJournalData(JournalCategory JournalchosenCategory, string name, double price,
           int copyNumber, DateTime printDate, string edition,string editor)
        { 
            IJournal newJournal = ItemManager.EmptyJournal;
            newJournal.Genre = JournalchosenCategory;
            newJournal.Name = name;
            newJournal.Price = price;
            newJournal.CopyNumber = copyNumber;
            newJournal.PrintDate = printDate;
            newJournal.Editor = editor;
            newJournal.PrintDate = printDate;

            return newJournal;
        }
        #endregion
    }
}
