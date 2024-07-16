using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Models
{
    public class UserModel
    {
        public int SN { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
    }
    public class RegisterUserResponseDto
    {
        public string EmailConfirmationLink { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
