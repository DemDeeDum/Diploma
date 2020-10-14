using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Admin
{
    public class UserManagmentViewModel
    {
        public List<UserViewModel> Admins { get; set; }
        public List<UserViewModel> Users { get; set; }
        public UserManagmentViewModel()
        {
            Admins = new List<UserViewModel>();
            Users = new List<UserViewModel>();
        }
    }
}