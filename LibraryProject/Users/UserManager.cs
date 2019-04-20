using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    class UserManager
    {
        UserCollection collection;
        IEnumerable<User> userList;

        public UserManager()
        {
            collection = new UserCollection();
            userList = collection.Items.Select(item => item as User).Where(user => user != null);
        }

        public void RegisterNewUser(IUser user)
        {
            if (!collection.IsUserExist(user.UserId))
            {
                User tmpUser = new User(user.Name, user.Password, user.Type);
                collection.AddUser(tmpUser);
            }
            else throw new Exception("user allredy exist");
        }


        public List<User> GetAllUsers()
        {
            return userList.ToList();
        }

        public List<User> GetAllCustomers()
        {
            return userList.Where(user => user.Type == UserType.Customer).ToList();
        }

        public List<User> GetAllEmployees()
        {
            return userList.Where(user => user.Type == UserType.Employee).ToList();
        }
    }
}
