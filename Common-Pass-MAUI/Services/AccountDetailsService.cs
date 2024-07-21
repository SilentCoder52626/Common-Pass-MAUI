using Common_Pass_MAUI.Helpers;
using Common_Pass_MAUI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Common_Pass_MAUI.Services
{
    public interface IAccountDetailsService
    {
        Task<AccountDetailModel> GetAccountDetails();
        Task AddOrUpdateAccounts(AccountDetailsDto model);
        Task<AccountDetailsDto> GetDecryptedDetails(int id);
        Task Delete(int id);

    }
    public class AccountDetailsService : IAccountDetailsService
    {
        private readonly HttpClient _client;

        public AccountDetailsService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Pass_Client");
        }

        public async Task AddOrUpdateAccounts(AccountDetailsDto model)
        {
            try
            {
                // Clear and set headers
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Get JWT token
                var token = await TokenHelper.GetJWTTokenAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Serialize the model to JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await _client.PostAsync("AccountDetails/AddOrUpdate", jsonContent);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ResponseModel<AccountDetailsDto>>(responseContent);

                if (result?.Data == null)
                {
                    throw new Exception("Failed to update account details.");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Log HTTP-specific exceptions
                throw new Exception("Error making HTTP request.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                // Log JSON-specific exceptions
                throw new Exception("Error parsing JSON response.", jsonEx);
            }
            catch (Exception ex)
            {
                // Log general exceptions
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = await TokenHelper.GetJWTTokenAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _client.GetAsync($"AccountDetails/RemoveAccount/{id}");


                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ResponseModel>(responseContent);
                    throw new Exception(result.Message);
                }
                
            }
            catch (JsonException jsonEx)
            {
                throw new Exception("Error parsing JSON response.", jsonEx);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AccountDetailModel> GetAccountDetails()
        {


            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = await TokenHelper.GetJWTTokenAsync();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync("AccountDetails/");
            var returnModel = new AccountDetailModel();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<ResponseModel<AccountDetailModel>>(await response.Content.ReadAsStringAsync());
                if (result?.Data != null)
                {
                    returnModel = result.Data;
                }
            }
            return returnModel;
        }

        public async Task<AccountDetailsDto> GetDecryptedDetails(int id)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var token = await TokenHelper.GetJWTTokenAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await _client.GetAsync($"AccountDetails/DecryptedDetails/{id}");

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ResponseModel<AccountDetailsDto>>(responseContent);

                if (result?.Data != null)
                {
                    return result.Data;
                }
                else
                {
                    throw new Exception("Failed to retrieve account details.");
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Error making HTTP request.", httpEx);
            }
            catch (JsonException jsonEx)
            {
                throw new Exception("Error parsing JSON response.", jsonEx);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
