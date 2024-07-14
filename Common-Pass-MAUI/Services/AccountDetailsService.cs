using Common_Pass_MAUI.Helpers;
using Common_Pass_MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common_Pass_MAUI.Services
{
    public interface IAccountDetailsService
    {
        Task<AccountDetailModel> GetAccountDetails();
    }
    public class AccountDetailsService : IAccountDetailsService
    {
        private readonly HttpClient _client;

        public AccountDetailsService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Pass_Client");
        }
        public async Task<AccountDetailModel> GetAccountDetails()
        {

            //var returnModel = new AccountDetailModel();

            //returnModel.Details.Add(new AccountDetailsDto()
            //{
            //    Account = "Acc",
            //    Id = 1,
            //    Pass = "qwertyu",
            //    UserName = "user"
            //});
            //returnModel.Details.Add(new AccountDetailsDto()
            //{
            //    Account = "My Acc",
            //    Id = 2,
            //    Pass = "IamKM",
            //    UserName = "yo user"
            //});
            //return returnModel;


            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = await TokenHelper.GetJWTTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync("AccountDetails/");
            var returnModel = new AccountDetailModel();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync());
                if (result.Data is JsonElement dataElement && dataElement.ValueKind == JsonValueKind.Object)
                {
                    var dataSeralized = JsonSerializer.Serialize(result.Data);
                    if (!String.IsNullOrEmpty(dataSeralized))
                        returnModel = JsonSerializer.Deserialize<AccountDetailModel>(dataSeralized) ?? new AccountDetailModel();

                }
            }
            return returnModel;
        }
    }
}
