using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Interfaces
{
    public interface IUserMapper
    {
        //Fields from the Users table:
        int Id { get; set; }
        string Name { get; set; }
        string password_hash { get; set; }

        //The following properties are not reflected in the Users table (They come from Roles), but are relevant for the business objects that model a user:
        int RoleId { get; set; }
        string RoleName { get; set; }
    }
}
