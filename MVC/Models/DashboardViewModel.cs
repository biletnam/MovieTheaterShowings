using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharedResources.Interfaces;

namespace MVC.Models
{
    public class DashboardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //The following properties are not reflected in the Users table (They come from Roles), but are relevant for the business objects that model a user:
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IUserMapper user { get; set; }
    }
}