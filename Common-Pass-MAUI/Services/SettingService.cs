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
    public interface ISettingService
    {
        Task<List<SettingModel>> GetSetting();
        Task SaveSetting(List<SettingModel> model);

    }
    public class SettingService : ISettingService
    {
        private readonly HttpClient _client;

        public SettingService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Pass_Client");
        }
        public async Task<List<SettingModel>> GetSetting()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = await TokenHelper.GetJWTTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync("setting/");
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel<AppSettingModel>>(await response.Content.ReadAsStringAsync());

                if (result?.Data != null)
                {
                    return result.Data.AppSettings;

                }


            }
            return new List<SettingModel>();
        }

        public async Task SaveSetting(List<SettingModel> model)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = await TokenHelper.GetJWTTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("setting/update", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception(result.Message);
            }
        }
    }
}
