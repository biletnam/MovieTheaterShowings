using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Interfaces
{
    public interface IUsersDAL
    {
        IUserMapper Insert(IUserMapper user);
        IUserMapper Get_User_by_User_Name(IUserMapper user);
        Task<List<IUserMapper>> Get_All_Users();
        IUserMapper Get_User_by_Id(IUserMapper user);
    }
}
