using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Services
{
    public interface IAccountService
    {
        Task<bool> IsUserValidated();
    }
    public class AccountService : IAccountService
    {
        public async Task<bool> IsUserValidated()
        {
            return false;
            //await SecureStorage.GetAsync(UIConstants.UserTokenKey);
        }
    }
}
