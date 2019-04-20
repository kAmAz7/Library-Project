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

        private Dictionary<Guid, User> _users = new Dictionary<Guid, User>();
        #endregion

        #region Properties
        internal List<User> Items
        {
            get { return _users.Values.ToList(); }
        }
        #endregion

        #region Public Methods
        internal void AddUser(User newUser)
        {
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
            Guid temp = parseUserId(userId);
            return _users.Remove(temp);
        }

        internal bool IsUserExist(Guid userId)
        {
            var a = _users.SingleOrDefault(user => user.Value.UserId == userId);
            if (a.Value == default(User))
                return false;
            return true;
        }

        internal List<User> SearchUserByName(string Name)
        {
            return _users.Select(item => item.Value).Where(value => value.Name == Name).ToList<User>();
        }
        #endregion

        #region Private Methods
        private Guid parseUserId(string userId)
        {
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
