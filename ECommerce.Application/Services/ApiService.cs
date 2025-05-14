using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using System.Text;

namespace ECommerce.Application.Services
{
    public class ApiService<T> : IApiService<T> where T : class
    {
        //private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient client;

        public ApiService(System.Net.Http.IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            //_httpClientFactory = httpClientFactory;
            client = httpClientFactory.CreateClient("ECommerceAPI");
            _httpContextAccessor = httpContextAccessor;
        }

        //private async Task<HttpClient> CreateClientWithAuthAsync()
        //{
        //    //var client = _httpClientFactory.CreateClient("ECommerceAPI");

        //    // Get access token from the current user session
        //    //var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        //    //if (!string.IsNullOrEmpty(accessToken))
        //    //{
        //    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    //}



        //    return client;
        //}
        public async Task<bool> DeleteAsync(string endpoint, int id)
        {
            //var client = await CreateClientWithAuthAsync();
            var response = await client.DeleteAsync($"{endpoint}/{id}");

            return response.IsSuccessStatusCode;
        }
       
        public async Task<IEnumerable<T>> GetAllAsync(string endpoint)
        {
            //var client = await CreateClientWithAuthAsync();
            var response = await client.GetAsync(endpoint);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> GetByIdAsync(string endpoint, int id)
        {
            //var client = await CreateClientWithAuthAsync();
            var response = await client.GetAsync($"{endpoint}/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<T> UpdateAsync(string endpoint, int id, T entity)
        {
            //var client = await CreateClientWithAuthAsync();
            var content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{endpoint}/{id}", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> CreateAsync(string endpoint, T entity)
        {
            //var client = await CreateClientWithAuthAsync();
            var content = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}

