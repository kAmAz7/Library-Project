using System;

namespace Users
{
    internal class User : IUser
    {
        #region Properties
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public Guid UserId { get; }
        #endregion Properties

        #region Ctor
        public User()
        {
            UserId = Guid.NewGuid();
        }
        public User(string Name, string Password, UserType Type)
        {
            this.Name = Name;
            this.Password = Password;
            this.Type = Type;
            this.UserId = Guid.NewGuid();
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"Name: {Name}  Type: {Type} ID: {UserId}";
        }
        #endregion
    }
}
