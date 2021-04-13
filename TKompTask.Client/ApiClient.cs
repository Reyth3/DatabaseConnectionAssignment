using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TKompTask.Shared;

namespace TKompTask.Client
{
    public class ApiClient : IDisposable
    {
        private static string API_ROOT = "https://localhost:44351";

        private readonly HttpClient _http;

        public ApiClient(string username, string password)
        {
            _http = new HttpClient() { BaseAddress = new Uri(API_ROOT) };
            var credentials = $"{username}:{password}";
            credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
            _http.DefaultRequestHeaders.Add("X-APIKEY", "xxx-debug-123");
        }

        public async Task<bool> TestConnection()
        {
            using var res = await _http.GetAsync("column/test");
            return res.IsSuccessStatusCode;     
        }

        public async Task<List<ColumnInfoDto>> GetColumnInfoForInts()
        {
            using var res = await _http.GetAsync("column/ints");
            if (res.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<List<ColumnInfoDto>>(await res.Content.ReadAsStringAsync());
            return new List<ColumnInfoDto>();
        }

        public void Dispose()
        {
            _http.Dispose();
        }
    }
}
