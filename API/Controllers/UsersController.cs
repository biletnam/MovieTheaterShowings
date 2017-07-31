using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

using SharedResources.Interfaces;
using SharedResources.Mappers;
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

        [HttpGet]
        [Route("users/")]
        public HttpResponseMessage Get()
        {
            HttpResponseMessage output;
            List<IUserMapper> data = new List<IUserMapper>();
            try
            {
                data = users_bll.Get_All_Users();
                ResponseWrapper wrapper = new ResponseWrapper(data);
                output = Request.CreateResponse(HttpStatusCode.OK, wrapper);
            }
            catch (BLLException)
            {
                output = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return output;
        }

        [HttpGet]
        [Route("users/{id}")]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage output;
            IUserMapper data;
            try
            {
                //Create an IUserMapper
                IUserMapper user = new UserMapper();
                user.Id = id;
                data = users_bll.Get_User_by_Id(user);
                ResponseWrapper wrapper = new ResponseWrapper(data);
                output = Request.CreateResponse(HttpStatusCode.OK, wrapper);
            }
            catch (BLLException)
            {
                output = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return output;
        }

        [HttpGet]
        [Route("users/{id}/{field}")]
        public HttpResponseMessage Get(int id, string field)
        {
            HttpResponseMessage output;
            IUserMapper data;
            try
            {
                //Create an IUserMapper
                IUserMapper user = new UserMapper();
                user.Id = id;
                data = users_bll.Get_User_by_Id(user);
                var outputField = new Object();
                switch(field.ToLower()){
                    case "id": outputField = data.Id; break;
                    case "name": outputField = data.Name; break;
                    case "password_hash": outputField = data.password_hash; break;
                    case "role": outputField = data.RoleId; break;
                    case "rolename": outputField = data.RoleName; break;
                    default: outputField = data; break;
                }
                ResponseWrapper wrapper = new ResponseWrapper(outputField);
                output = Request.CreateResponse(HttpStatusCode.OK, wrapper);
            }
            catch (BLLException)
            {
                output = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            return output;
        }
    }
}
