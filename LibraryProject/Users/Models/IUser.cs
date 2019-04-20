using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    interface IUser
    {
        string Name { get; }
        string Password { get; }
        Guid UserId { get; }
        UserType Type { get; }
    }
}
