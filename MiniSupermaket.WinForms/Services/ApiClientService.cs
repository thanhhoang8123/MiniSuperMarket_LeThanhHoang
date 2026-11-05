using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

using MiniSupermarket.WinForms;

namespace MiniSupermarket.WinForms.Services
{
    // Lớp dùng chung để giao tiếp với Web API
    public static class ApiClientService
    {
        // Dùng kiểm tra chứng chỉ mặc định của Windows/.NET. Không bỏ qua lỗi chứng chỉ,
        // vì đăng nhập và JWT sẽ bị lộ nếu kết nối bị chuyển hướng qua máy chủ giả mạo.
        private static readonly HttpClientHandler _handler = new();

        // Kết nối tới API phát triển đang bind loopback ở cổng 5055.
        // Khi triển khai ngoài máy phát triển, hãy cấu hình HTTPS với chứng chỉ hợp lệ.
        private static string _baseUri = "http://localhost:5055/api/";

        private static HttpClient? _client = CreateHttpClient(_baseUri);

        // Khởi tạo HttpClient dùng chung cho toàn bộ ứng dụng (tự động phục hồi nếu bị dispose)
        public static HttpClient Client
        {
            get
            {
                if (_client != null)
                {
                    try
                    {
                        // DefaultRequestHeaders getter trong .NET Runtime luôn gọi CheckDisposed()
                        _ = _client.DefaultRequestHeaders;
                    }
                    catch (ObjectDisposedException)
                    {
                        _client = null;
                    }
                }
                return _client ??= CreateHttpClient(_baseUri);
            }
            private set => _client = value;
        }

        public static void EnsureClientActive()
        {
            if (_client != null)
            {
                try
                {
                    _ = _client.DefaultRequestHeaders;
                }
                catch (ObjectDisposedException)
                {
                    _client = CreateHttpClient(_baseUri);
                }
            }
            else
            {
                _client = CreateHttpClient(_baseUri);
            }
        }

        private static HttpClient CreateHttpClient(string baseAddress)
        {
            var client = new HttpClient(_handler, disposeHandler: false)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(15)
            };

            if (!string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            return client;
        }

        public static void SwitchBaseAddress(string newBaseAddress)
        {
            _baseUri = newBaseAddress;
            Client = CreateHttpClient(newBaseAddress);
        }

        // Gắn JWT Token vào Header để xác thực khi gọi API
        public static void AttachToken(string token)
        {
            // Xóa Token cũ trước khi gắn Token mới
            Client.DefaultRequestHeaders.Authorization = null;

            // Nếu Token hợp lệ thì thêm vào Header theo chuẩn Bearer
            if (!string.IsNullOrWhiteSpace(token))
            {
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Đăng nhập và lưu thông tin phiên làm việc
        public static async Task<bool> LoginAsync(
            string username, string password)
        {
            // Tạo đối tượng chứa thông tin đăng nhập
            var loginObj = new
            {
                Username = username,
                Password = password
            };

            // Không tự chuyển sang HTTP khi HTTPS lỗi: thao tác đó sẽ gửi mật khẩu/JWT
            // qua kết nối không mã hóa. Hãy cấu hình và tin cậy chứng chỉ HTTPS của API.
            using var response = await Client.PostAsJsonAsync("auth/login", loginObj);

            // Đăng nhập thất bại nếu API trả về mã lỗi
            if (!response.IsSuccessStatusCode)
                return false;

            // Đọc dữ liệu JSON do API trả về
            using var doc = JsonDocument.Parse(
                await response.Content.ReadAsStringAsync());

            var root = doc.RootElement;

            // Kiểm tra phản hồi có Token và Role hay không
            if (!root.TryGetProperty("token", out var tokenElement) ||
                !root.TryGetProperty("role", out var roleElement))
            {
                throw new Exception(
                    "Phản hồi đăng nhập không có token hoặc role.");
            }

            // Lưu Token, vai trò và tên tài khoản vào biến tĩnh của phiên làm việc
            SessionManager.JwtToken =
                tokenElement.GetString() ?? string.Empty;

            SessionManager.CurrentRole =
                roleElement.GetString() ?? string.Empty;

            SessionManager.CurrentUsername = username;

            // Kiểm tra Token nhận được từ Server
            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                SessionManager.ClearSession();
                throw new Exception("Server trả về token rỗng.");
            }

            // Gắn Token vào Header Authorization: Bearer {token} để sử dụng cho tất cả các Form sau
            AttachToken(SessionManager.JwtToken);

            return true;
        }

        // Gửi yêu cầu GET và nhận dữ liệu JSON dạng chuỗi
        public static async Task<string> GetDataWithTokenAsync(
            string endpoint)
        {
            using var response = await Client.GetAsync(endpoint);

            // Kiểm tra phản hồi và trả về dữ liệu
            return await ReadResponseAsync(response);
        }

        // Gửi yêu cầu GET và chuyển JSON thành đối tượng T
        public static async Task<T?> GetAsync<T>(string endpoint)
        {
            using var response = await Client.GetAsync(endpoint);

            // Nếu API báo lỗi thì xử lý và thông báo
            if (!response.IsSuccessStatusCode)
                await ReadResponseAsync(response);

            // Chuyển nội dung JSON thành kiểu dữ liệu T
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Gửi yêu cầu POST để thêm dữ liệu mới
        public static Task<HttpResponseMessage> PostAsync<T>(
            string endpoint, T data)
        {
            return Client.PostAsJsonAsync(endpoint, data);
        }

        // Gửi yêu cầu PUT để cập nhật dữ liệu
        public static Task<HttpResponseMessage> PutAsync<T>(
            string endpoint, T data)
        {
            return Client.PutAsJsonAsync(endpoint, data);
        }

        // Gửi yêu cầu DELETE để xóa dữ liệu
        public static Task<HttpResponseMessage> DeleteAsync(
            string endpoint)
        {
            return Client.DeleteAsync(endpoint);
        }

        // Đọc phản hồi API và xử lý các mã lỗi HTTP
        private static async Task<string> ReadResponseAsync(
            HttpResponseMessage response)
        {
            // Đọc nội dung phản hồi từ Server
            string body = await response.Content.ReadAsStringAsync();

            // Nếu thành công thì trả về dữ liệu
            if (response.IsSuccessStatusCode)
                return body;

            // Lỗi 401: Chưa đăng nhập hoặc phiên làm việc hết hạn
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception(
                    "Phiên làm việc hết hạn hoặc chưa đăng nhập (401).");

            // Lỗi 403: Không có quyền thực hiện thao tác
            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exception(
                    "Bạn không có quyền thực hiện thao tác này (403).");

            // Các lỗi HTTP khác
            throw new HttpRequestException(
                $"API trả về lỗi {(int)response.StatusCode} " +
                $"{response.ReasonPhrase}.\n{body}",
                inner: null,
                statusCode: response.StatusCode);
        }
    }
}
