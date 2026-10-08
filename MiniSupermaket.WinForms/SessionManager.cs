using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;
    }

    public static class ApiClientService
    {
        public static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7207/api/")
        };

        public static async Task<bool> LoginAsync(string username, string password)
        {
            var loginObj = new
            {
                Username = username,
                Password = password
            };

            var response = await Client.PostAsJsonAsync(
                "auth/login",
                loginObj
            );

            if (response.IsSuccessStatusCode)
            {
                var jsonString =
                    await response.Content.ReadAsStringAsync();

                using var doc =
                    JsonDocument.Parse(jsonString);

                SessionManager.JwtToken =
                    doc.RootElement
                        .GetProperty("token")
                        .GetString() ?? string.Empty;

                SessionManager.CurrentRole =
                    doc.RootElement
                        .GetProperty("role")
                        .GetString() ?? string.Empty;

                SessionManager.CurrentUsername = username;

                // QUAN TRỌNG
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken
                    );

                return true;
            }

            return false;
        }

        public static async Task<string> GetDataWithTokenAsync(string endpoint)
        {
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    SessionManager.JwtToken
                );

            var response = await Client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            if (response.StatusCode ==
                System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception(
                    "Phiên làm việc hết hạn hoặc chưa đăng nhập!"
                );
            }

            throw new Exception(
                "Lỗi khi gọi dữ liệu từ Server."
            );
        }
    }
}