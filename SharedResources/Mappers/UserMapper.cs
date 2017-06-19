using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;

namespace SharedResources.Mappers
{
    public class UserMapper : IUserMapper
    {
        //Fields from the Users table:
        public int Id { get; set; }
        public string Name { get; set; }
        public string password_hash { get; set; }

        //The following properties are not reflected in the Users table (They come from Roles), but are relevant for the business objects that model a user:
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
