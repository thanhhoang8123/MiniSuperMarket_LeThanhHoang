
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniSupermarket.WinForms
{
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;

        public static void ClearSession()
        {
            JwtToken = string.Empty;
            CurrentUsername = string.Empty;
            CurrentRole = string.Empty;

            ApiClientService.Client.DefaultRequestHeaders.Authorization = null;
        }
    }

    public static class ApiClientService
    {
        public static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7207/api/")
        };

        private static void ApplyToken()
        {
            Client.DefaultRequestHeaders.Authorization =
                string.IsNullOrWhiteSpace(SessionManager.JwtToken)
                    ? null
                    : new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);
        }

        public static async Task<bool> LoginAsync(
            string username,
            string password)
        {
            var loginObj = new
            {
                Username = username,
                Password = password
            };

            using var response = await Client.PostAsJsonAsync(
                "auth/login",
                loginObj);

            if (!response.IsSuccessStatusCode)
                return false;

            using var doc = JsonDocument.Parse(
                await response.Content.ReadAsStringAsync());

            var root = doc.RootElement;

            if (!root.TryGetProperty("token", out var tokenElement) ||
                !root.TryGetProperty("role", out var roleElement))
            {
                throw new Exception(
                    "Phản hồi đăng nhập không có token hoặc role.");
            }

            SessionManager.JwtToken =
                tokenElement.GetString() ?? string.Empty;

            SessionManager.CurrentRole =
                roleElement.GetString() ?? string.Empty;

            SessionManager.CurrentUsername = username;

            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                SessionManager.ClearSession();
                throw new Exception("Server trả về token rỗng.");
            }

            ApplyToken();
            return true;
        }

        public static async Task<string> GetDataWithTokenAsync(
            string endpoint)
        {
            ApplyToken();

            using var response = await Client.GetAsync(endpoint);

            return await ReadResponseAsync(response);
        }

        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            ApplyToken();

            using var response = await Client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                await ReadResponseAsync(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(
            string endpoint,
            T data)
        {
            ApplyToken();

            return await Client.PostAsJsonAsync(endpoint, data);
        }

        public static async Task<HttpResponseMessage> PutAsync<T>(
            string endpoint,
            T data)
        {
            ApplyToken();

            return await Client.PutAsJsonAsync(endpoint, data);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(
            string endpoint)
        {
            ApplyToken();

            return await Client.DeleteAsync(endpoint);
        }

        private static async Task<string> ReadResponseAsync(
            HttpResponseMessage response)
        {
            string body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return body;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception(
                    "Phiên làm việc hết hạn hoặc chưa đăng nhập (401).");

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception(
                    "Bạn không có quyền thực hiện thao tác này (403).");

            throw new HttpRequestException(
                $"API trả về lỗi {(int)response.StatusCode} " +
                $"{response.ReasonPhrase}.\n{body}");
        }
    }
}
