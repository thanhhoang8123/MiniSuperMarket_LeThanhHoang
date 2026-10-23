
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
        // Khởi tạo HttpClient dùng chung cho toàn bộ ứng dụng
        public static readonly HttpClient Client = new HttpClient
        {
            // Địa chỉ gốc của Web API
            BaseAddress = new Uri("https://localhost:7207/api/")
        };

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

            // Gửi yêu cầu POST đến API đăng nhập
            using var response = await Client.PostAsJsonAsync(
                "auth/login", loginObj);

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

            // Lưu Token, vai trò và tên tài khoản vào phiên làm việc
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

            // Gắn Token để sử dụng cho các yêu cầu API tiếp theo
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
                $"{response.ReasonPhrase}.\n{body}");
        }
    }
}
