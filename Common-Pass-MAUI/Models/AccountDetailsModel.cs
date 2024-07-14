using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Models
{
    public class AccountDetailModel
    {
        public List<AccountDetailsDto> Details { get; set; } = new List<AccountDetailsDto>();
    }
    public class AccountDetailsDto : AccountDetailsCreateDto
    {
        public int Id { get; set; }

    }
    public class AccountDetailsCreateDto
    {
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
    }
}
