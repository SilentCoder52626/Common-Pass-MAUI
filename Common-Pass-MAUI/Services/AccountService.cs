using Common_Pass_MAUI.Helpers;
using Common_Pass_MAUI.Models;
using Common_Pass_MAUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Services
{
    public interface IAccountService
    {
        Task<bool> IsUserValidated();
        Task<bool> Login(LoginModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;

        public AccountService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Pass_Client");
        }

        public async Task<bool> IsUserValidated()
        {
            var token = await TokenHelper.GetJWTTokenAsync();
            if(token != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Login(LoginModel model)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("account/login", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                await SecureStorage.SetAsync(UIConstants.UserTokenKey, result);
                return true;
            }
            return false;
        }
    }
}
