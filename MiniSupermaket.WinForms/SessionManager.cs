
namespace MiniSupermarket.WinForms
{
    // Lớp quản lý thông tin phiên đăng nhập hiện tại
    public static class SessionManager
    {
        // JWT Token dùng để xác thực với Web API
        public static string JwtToken { get; set; } = string.Empty;

        // Tên tài khoản đang đăng nhập
        public static string CurrentUsername { get; set; } = string.Empty;

        // Vai trò của tài khoản: Admin, Cashier hoặc Warehouse
        public static string CurrentRole { get; set; } = string.Empty;

        // Xóa thông tin khi đăng xuất
        public static void ClearSession()
        {
            // Xóa thông tin phiên đăng nhập
            JwtToken = string.Empty;
            CurrentUsername = string.Empty;
            CurrentRole = string.Empty;

            // Gỡ JWT Token khỏi Header của HttpClient
            MiniSupermarket.WinForms.Services.ApiClientService
                .AttachToken(string.Empty);
        }
    }
}
