using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

using SharedResources.Interfaces;
using CompositionRoot;
using SharedResources.Exceptions.BLL;

namespace API.Controllers
{
    public class UsersController : ApiController
    {
        //Properties:
        private IUsersBLL users_bll { get; set; }

        public UsersController()
        {
            CRoot CompositionRoot = new CRoot("prod");
            users_bll = CompositionRoot.UsersBLL;
        }

        public List<IUserMapper> Get()
        {
            List<IUserMapper> output = new List<IUserMapper>();
            try
            {
                output = users_bll.Get_All_Users();
            }
            catch (SqlBLLException)
            {
                //Put error message here.
            }

            return output;
        } 
    }
}
