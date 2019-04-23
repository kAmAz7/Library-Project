using BookLib;
using BookLib.Models;
using LibraryProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Users;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LibraryProject
{
    public sealed partial class EmployeeMainPage : Page
    {
        #region Data Members
        ItemManager itemManager = ItemManager.GetInstance();
        UserManager userManager = UserManager.GetInstance();
        List<IItem> _items = new List<IItem>();
        List<IBook> _books = new List<IBook>();
        List<IJournal> _journals = new List<IJournal>();
        List<IUser> _users = new List<IUser>();
        SearchEngine _itemSearch = new SearchEngine();
        #endregion

        #region Ctor
        public EmployeeMainPage()
        {
            this.InitializeComponent();
            chooseItemCbox.ItemsSource = Enum.GetValues(typeof(ItemType)).Cast<ItemType>();
            chooseItemCbox.SelectedItem = ItemType.All;
            ShowAllItems();
        }
        #endregion

        #region Events

        private void addBookBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddBook));
        }

        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddUser));
        }

        private async void removeBookBtn_Click(object sender, RoutedEventArgs e)
        {  
            try
                {
                    IItem itemToRemove = (IItem)showResultLbox.SelectedItem;
                    itemManager.RemoveItem(itemToRemove);
                    ShowAllItems();
                }
                catch (ArgumentNullException)
                {
                    await new MessageDialog("You must select an Item to remove it").ShowAsync();
                }
                catch (NullReferenceException)
                {
                    await new MessageDialog("You must select an Item to remove it").ShowAsync();
                }
                catch (ArgumentException)
                {
                    await new MessageDialog("Failed to remove requested Item \nCheck that you aren't trying to remove a 'User' by mistake!!").ShowAsync();
                }  
                catch (InvalidCastException)
                {
                    await new MessageDialog("Failed to remove requested Item \nCheck that you are'nt trying to remove a 'User' by mistake!!").ShowAsync();
                }
        }

        private async void removeUserBtn_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                IUser userToRemove = (IUser)showResultLbox.SelectedItem;
                userManager.RemoveUser(userToRemove);
                ShowAllUsers();
            }
            catch (ArgumentNullException)
            {
                await new MessageDialog("Please select a 'User' to remove").ShowAsync();
            }
            catch (NullReferenceException)
            {
                await new MessageDialog("Please select a 'User' to remove").ShowAsync();
            }
            catch (ArgumentException)
            {
                await new MessageDialog("Failed to remove requested User \nCheck that you are not trying to remove a 'Reading Item' by mistake!!").ShowAsync();
            }
            catch (InvalidCastException)
            {
                await new MessageDialog("Failed to remove requested User \nCheck that you are not trying to remove a 'Reading Item' by mistake!!").ShowAsync();
            }
        }

        private async void searchItemBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            string type = chooseItemCbox.SelectedItem.ToString();
            ItemType enumType;
            Enum.TryParse(type, out enumType);
            string category = ((ListBoxItem)searchByCbox.SelectedItem).Content.ToString();
            string inputText = searchItemBox.QueryText;

            try
            { 
                _items = _itemSearch.SearchExecuter(enumType, category, inputText);
                showResultLbox.ItemsSource = _items;
            }
            catch(ArgumentException)
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

        private async void searchUserBox_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        { 
            string type = ((ListBoxItem)chooseUserTypeCbox.SelectedItem).Content.ToString();
            if (type == "All") type = "None";
            UserType enumType;
            Enum.TryParse(type, out enumType);
            string searchBy = ((ListBoxItem)searchUserByCbox.SelectedItem).Content.ToString();
            string typedWord = searchUserBox.QueryText;

            try
            {
                switch (searchBy)
                {
                    case ("All"):
                        {
                            _users = userManager.GetAllUsers(enumType);
                            showResultLbox.ItemsSource = _users;
                            break;
                        }
                    case ("Name"):
                        {
                            _users = userManager.GetUsersByName(typedWord, enumType);
                            showResultLbox.ItemsSource = _users;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (InvalidOperationException)
            {
                await new MessageDialog("User type is invalid").ShowAsync();
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
            _items = itemManager.GetAllItems();
            showResultLbox.ItemsSource = _items;
        }

        private void ShowAllUsers()
        {
            _users = userManager.GetAllUsers();
            showResultLbox.ItemsSource = _users;
        }
        #endregion
    }
}
