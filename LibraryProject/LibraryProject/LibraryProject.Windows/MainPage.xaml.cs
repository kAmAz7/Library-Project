using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Users;
using System;

namespace LibraryProject
{
    public sealed partial class MainPage : Page
    {
        #region Data Members
        UserManager manager = UserManager.GetInstance();
        #endregion

        #region Ctor
        public MainPage()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Events
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string userName = userNameTbox.Text;
            string password = passwordTbox.Password.ToString();

            if (string.IsNullOrWhiteSpace(userName))
                erorMessageTbl.Text = "Please fill in the 'Username' field";
            else if (string.IsNullOrEmpty(password))
                erorMessageTbl.Text = "Please fill in the 'Password' field";
            else
            {
                UserType Type;
                Guid userID = default(Guid);
                Type = manager.CheckUserDetails(userName, password,out userID); 
                if (Type == UserType.None)
                    erorMessageTbl.Text = "'Username' or 'Password' incorrect!!";
                else if (Type == UserType.Customer)
                    this.Frame.Navigate(typeof(CustomerMainPage),userID);
                else if (Type == UserType.Employee)
                    this.Frame.Navigate(typeof(EmployeeMainPage));
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        #endregion
    }
}
