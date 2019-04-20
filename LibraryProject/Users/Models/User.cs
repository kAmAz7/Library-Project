using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    public class User : IUser
    {
        #region Data Members
        private string _name;
        private string _password;
        private Guid _userId;
        private UserType _type;

        #endregion

        #region Properties
        public string Name { get { return _name; } }
        public string Password { get { return _password; } }
        public UserType Type { get { return _type; } }
        public Guid UserId { get { return _userId; } }

        #endregion Properties

        #region Ctor
        public User(string Name, string Password, UserType Type)
        {
            _name = Name;
            _password = Password;
            _type = Type;
            _userId = Guid.NewGuid();
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"Name: {_name} ID: {_userId} Type: {_type}";
        }
        #endregion
    }
}
