using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    public interface IUser
    {
        string Name { get; set; }
        string Password { get; set; }
        Guid UserId { get; }
        UserType Type { get; set; }
    }
}
