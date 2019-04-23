using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    public class UserManager
    {
        #region Data Members
        UserCollection _collection;
        #endregion

        #region Properties
        public static IUser Empty
        {
            get { return new User(); }
        }
        #endregion

        #region Ctor
        private static UserManager _instance;
        public static UserManager GetInstance()
        {
            if (_instance == null)
                _instance = new UserManager();
            return _instance;
        }

        private UserManager()
        {
            _collection = new UserCollection();
        }
        #endregion

        #region Public Methods

        public void RegisterNewUser(IUser user)
        { 
            if (!_collection.IsUserExist(user.Name))
            {
                User tmpUser = new User(user.Name, user.Password, user.Type);
                _collection.AddUser(tmpUser);
            }
            else throw new Exception("user allredy exist");
        }

        public UserType CheckUserDetails(string Name, string Password,out Guid userId)
        { 
            IUser tmp = null;
            try
            {
                tmp = _collection.Items.FirstOrDefault(user => user.Name == Name && user.Password == Password);
                if (tmp != null)
                {
                    userId = tmp.UserId;
                    return tmp.Type;
                }
                    
                else
                    return UserType.None;
            }
            catch (InvalidOperationException)
            {
                return UserType.None;
            }
        }

        public bool RemoveUser(IUser user)
        { 
            string id = user.UserId.ToString();
            return _collection.RemoveUserByUserId(id);
        }

        public List<IUser> GetAllUsers(UserType type = UserType.None)
        { 
            switch (type)
            {
                case UserType.None:
                    return _collection.Items?.ToList();
                case UserType.Employee:
                    return _collection.Items.Where(user => user.Type == UserType.Employee)?.ToList();
                case UserType.Customer:
                    return _collection.Items.Where(user => user.Type == UserType.Customer)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }
        }

        public List<IUser> GetUsersByName(string name, UserType type)
        { 
            switch (type)
            {
                case UserType.None:
                    return _collection.Items.Where(item => item.Name == name)?.ToList();
                case UserType.Employee:
                    return _collection.Items.Where(item => item.Type == UserType.Employee && item.Name == name)?.ToList();
                case UserType.Customer:
                    return _collection.Items.Where(item => item.Type == UserType.Customer && item.Name == name)?.ToList();
                default:
                    throw new InvalidOperationException("Invalid type");
            }
        }
        #endregion
    }
}
