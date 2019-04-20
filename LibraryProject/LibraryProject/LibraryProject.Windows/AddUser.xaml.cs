using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Users;
using Windows.UI.Popups;


namespace LibraryProject
{
    public sealed partial class AddUser : Page
    {
        #region Data Members
        UserManager _manager = UserManager.GetInstance();
        #endregion

        #region Ctor
        public AddUser()
        {
            this.InitializeComponent();
            userTypeCbox.ItemsSource = Enum.GetValues(typeof(UserType)).Cast<UserType>();
            userTypeCbox.SelectedItem = UserType.None;
        }
        #endregion

        #region Events
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeeMainPage));
        }

        private async void createBtn_Click(object sender, RoutedEventArgs e)
        {
            //Data members and input Check 
            string Type = userTypeCbox.SelectedItem.ToString();
            string userName = usernameTbox.Text;
            string password = passwordTbox.Text;
            string confirmPass = confirmPassTbox.Text;
            if (string.IsNullOrWhiteSpace(userName))
                erorMessageTbl.Text = "Please fill-in the 'Username' field";
            else if (string.IsNullOrEmpty(password))
                erorMessageTbl.Text = "Please fill-in the 'Password' field";
            else if (string.IsNullOrEmpty(confirmPass))
                erorMessageTbl.Text = "Please fill-in the 'Confirm Password' field";
            else if (password != confirmPass)
                erorMessageTbl.Text = "Passwords do not match each other!!";
            else if (Type == "None")
                erorMessageTbl.Text = "Please select 'User' type";

            else
            { //Transfer data to create a new user
                UserType chosenType;
                Enum.TryParse(Type, out chosenType);
                IUser newUser = CreateNewUserData(userName, password, chosenType);
                bool isSucceeded = true;

                try
                {
                    _manager.RegisterNewUser(newUser);
                }
                catch (Exception)
                {
                    await new MessageDialog("Current 'Username' already exists in the system!! \nPlease choose and enter different 'Username'!!").ShowAsync();
                    isSucceeded = false;
                }

                if (isSucceeded)
                {
                    await new MessageDialog("New 'User' created successfully!!").ShowAsync();
                    this.Frame.Navigate(typeof(EmployeeMainPage));
                }
            }
        }
        #endregion

        #region Private Methods
        private IUser CreateNewUserData(string name, string  password, UserType type)
        { //Fill in a template to create a new user
            IUser newUser = UserManager.Empty;
            newUser.Name = name;
            newUser.Password = password;
            newUser.Type = type;
            return newUser;
        }
        #endregion
    }
}
