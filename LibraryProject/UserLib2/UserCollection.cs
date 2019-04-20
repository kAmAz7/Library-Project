using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Users
{
    class UserCollection
    {
        #region Data Members

        private readonly Dictionary<Guid, IUser> _users = new Dictionary<Guid, IUser>();
        #endregion

        #region Properties
        internal List<IUser> Items
        {
            get { return _users.Values.ToList(); }
        }
        #endregion

        #region Ctor
        public UserCollection()
        {
            User admin = new User("admin","admin",UserType.Employee);
            AddUser(admin);
            User customer = new User("customer","customer", UserType.Customer);
            AddUser(customer);
        }
        #endregion

        #region Public Methods
        internal void AddUser(IUser newUser)
        { //Add a new user to the list of users
            if (newUser != null
                && !string.IsNullOrEmpty(newUser.Name)
                && !string.IsNullOrEmpty(newUser.Password))
            {
                _users.Add(newUser.UserId, newUser);
            }
            else throw new NullReferenceException("Data is null");
        }

        internal bool RemoveUserByUserId(string userId)
        {
            Guid temp = ParseUserId(userId);
            return _users.Remove(temp);
        }

        internal bool IsUserExist(string userName)
        {
            var name = _users.FirstOrDefault(user => user.Value.Name == userName);
            if (name.Value == default(User))
                return false;
            return true;
        }
        #endregion

        #region Private Methods
        private Guid ParseUserId(string userId)
        { //Convert the user ID to characters
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException("UserId is null or empty");
            Guid temp;
            if (!Guid.TryParse(userId, out temp))
                throw new ArgumentException("UserId is not valid");
            return temp;
        }
        #endregion
    }
}
