using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Models
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
