using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeShopAPI_UI
{
    public class LoginModel
    {
        [Required]
        public string Input { get; set; }

        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginModel()
        {
            Description = "User Name";
        }

    }
}
