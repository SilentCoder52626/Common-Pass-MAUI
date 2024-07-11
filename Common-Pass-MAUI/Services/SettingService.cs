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
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync());
                if (result.Data is JsonElement dataElement && dataElement.ValueKind == JsonValueKind.Object)
                {
                    var appSettingsArray = dataElement.GetProperty("AppSettings").EnumerateArray();

                    // Create a list to hold AppSettingDto objects
                    var appSettings = new List<SettingModel>();

                    // Iterate through the array and deserialize each object into AppSettingDto
                    foreach (var setting in appSettingsArray)
                    {
                        var appSetting = new SettingModel
                        {
                            Key = setting.GetProperty("Key").GetString(),
                            Value = setting.GetProperty("Value").GetString()
                        };

                        appSettings.Add(appSetting);
                    }
                    return appSettings;
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
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync());

            }
            else
            {
                var result = JsonSerializer.Deserialize<ResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception(result.Message);
            }
        }
    }
}
