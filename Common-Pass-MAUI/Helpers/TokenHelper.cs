using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Helpers
{
    public static class TokenHelper
    {
        public static async Task<string?> GetJWTTokenAsync()
        {
            return await SecureStorage.GetAsync(UIConstants.UserTokenKey);
        }
    }
}
