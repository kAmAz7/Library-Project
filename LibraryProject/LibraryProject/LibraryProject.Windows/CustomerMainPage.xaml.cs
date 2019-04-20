using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using BookLib;
using BookLib.Models;
using System;
using System.Linq;
using LibraryProject.Common;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace LibraryProject
{
    public sealed partial class CustomerMainPage : Page 
    {
        #region Data Members
        ItemManager manager = ItemManager.GetInstance();
        List<IItem> _items = new List<IItem>();
        SearchEngine _itemSearch = new SearchEngine();
        Guid _userID;
        #endregion

        #region Ctor
        public CustomerMainPage()
        {
            this.InitializeComponent();
            chooseItemCbox.ItemsSource = Enum.GetValues(typeof(ItemType)).Cast<ItemType>();
            chooseItemCbox.SelectedItem = ItemType.All;
            ShowAllItems();
        }
        #endregion

        #region Events
        protected override void OnNavigatedTo(NavigationEventArgs e)
        { //Transfer the user ID from the registration screen
            _userID = (Guid)e.Parameter;
        }

        private async void searchItemBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            //Data members
            string type = chooseItemCbox.SelectedItem.ToString();
            ItemType enumType;
            Enum.TryParse(type, out enumType);
            string category = ((ListBoxItem)searchByCbox.SelectedItem).Content.ToString();
            string inputText = searchItemBox.QueryText;

            try
            { //Send search information to the shared search class (common folder/search engine class)
                _items = _itemSearch.SearchExecuter(enumType, category, inputText);
                showResultLbox.ItemsSource = _items;
            }
            catch (ArgumentException)
            {
                if (category == "Author")
                    await new MessageDialog("Please check that you have selected a 'Book'-type item").ShowAsync();
                if (category == "Editor")
                    await new MessageDialog("Please check that you have selected a 'Journal'-type item").ShowAsync();
            }
            catch (NullReferenceException)
            {
                await new MessageDialog("One or more Data are invalid").ShowAsync();
            }
        }

        private async void rentBookBtn_Click(object sender, RoutedEventArgs e)
        {
            //input validation
            IItem itemToRent = (IItem)showResultLbox.SelectedItem;
            if (itemToRent == null)
                await new MessageDialog("Please select an Item to borrow").ShowAsync();  
            else if (itemToRent.CopyNumber == 0)
                await new MessageDialog("This Item is not available in the stock at the moment").ShowAsync();
            else
            {
                //Transfer Book Rental
                manager.RentNewItem(_userID, itemToRent);
                await new MessageDialog("Thank you for borrowing this Item!! \nHave fun!!!").ShowAsync();
                //await new MessageDialog("Thank you for borrowing!! \nHave fun!!!", itemToRent.Name).ShowAsync();

            }
        }

        private async void showRentedBtn_Click(object sender, RoutedEventArgs e)
        { //View the books I am currently renting
            try
            {
                _items = manager.ShowRentedItems(_userID);
                if (_items.Count == 0)
                    await new MessageDialog("You don't have any borrowed Items to return").ShowAsync();
                else
                    showResultLbox.ItemsSource = _items;
            }
            catch (KeyNotFoundException)
            {
                await new MessageDialog("You don't have any borrowed Items to return").ShowAsync();
            }

        }

        private async void returnBookBtn_Click(object sender, RoutedEventArgs e)
        {
            //Input validation
            IItem itemToReturn = (IItem)showResultLbox.SelectedItem;
            if (itemToReturn == null)
                await new MessageDialog("Please select an Item to return").ShowAsync();
            else try
                { //Transfer information to return a book
                    manager.ReturnItem(_userID, itemToReturn);
                    _items = manager.ShowRentedItems(_userID);
                    showResultLbox.ItemsSource = _items;
                }
                catch (ArgumentNullException)
                {
                    await new MessageDialog("You can't return an Item that you did'nt borrow").ShowAsync();
                }
                catch (KeyNotFoundException)
                {
                    await new MessageDialog("You don't have any borrowed Items to return").ShowAsync();
                }
                catch (ArgumentOutOfRangeException)
                {
                    await new MessageDialog("You can't return an Item that you did'nt borrow").ShowAsync();
                }

        }

        private void logOutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        #endregion

        #region Private Methods
        private void ShowAllItems()
        {
            _items = manager.GetAllItems();
            showResultLbox.ItemsSource = _items;
        }
        #endregion
    }
}
