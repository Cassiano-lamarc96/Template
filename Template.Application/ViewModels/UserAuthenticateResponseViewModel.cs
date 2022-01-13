using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Application.ViewModels
{
    public class UserAuthenticateResponseViewModel
    {
        public UserViewModel user { get; set; }
        public string token { get; set; }

        public UserAuthenticateResponseViewModel(UserViewModel user, string token)
        {
            this.user = user;
            this.token = token;
        }
    }
}
