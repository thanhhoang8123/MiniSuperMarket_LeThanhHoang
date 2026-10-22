🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)
Buổi thực hành: Buổi 2 - Bảo mật & phân quyền JWT cho Web API

🏗️ 1. Mục tiêu buổi thực hành
- Phân biệt Authentication (xác thực) và Authorization (phân quyền).
- Tích hợp JWT (JSON Web Token) vào ASP.NET Core Web API.
- Bảo vệ API bằng [Authorize] và [Authorize(Roles = "...")].
- Xây dựng giao diện đăng nhập WinForms.
- Lưu thông tin phiên đăng nhập và gửi Bearer Token khi gọi API.
🛠️ 2. Công nghệ sử dụng
- C# / .NET 8.0
- ASP.NET Core Web API
- JWT Bearer Authentication
- System.IdentityModel.Tokens.Jwt
- Microsoft.AspNetCore.Authentication.JwtBearer
- Windows Forms
- HttpClient và System.Net.Http.Json
- Swagger UI
🔐 3. Kiến thức chính
3.1. Authentication và Authorization
- Authentication: xác minh danh tính tài khoản đăng nhập.
- Authorization: xác định tài khoản có quyền thực hiện chức năng hay không.
3.2. Cấu trúc JWT
JWT gồm ba phần:
1. Header: thuật toán và loại token.
2. Payload: các claims như tên tài khoản, vai trò và thời hạn.
3. Signature: chữ ký dùng để phát hiện token bị chỉnh sửa.
JWT hỗ trợ xác thực không trạng thái (stateless): Client gửi token kèm request; API xác minh token thay vì lưu một phiên đăng nhập phía Server.
3.3. Data Annotations và ModelState
Các thuộc tính như [Required], [StringLength], [Range] và [EmailAddress] giúp kiểm tra dữ liệu đầu vào. Với Controller dùng [ApiController], dữ liệu không hợp lệ thường được phản hồi bằng HTTP 400 Bad Request.
🧩 4. Kiến trúc và chức năng triển khai
Backend — MiniSupermarket.API
- AuthController: tiếp nhận thông tin đăng nhập và cấp JWT.
- Cấu hình AddAuthentication() và AddJwtBearer() trong Program.cs.
- Gọi app.UseAuthentication() trước app.UseAuthorization().
- Bảo vệ các API cần đăng nhập bằng [Authorize].
- Dùng claim Name và Role để nhận diện tài khoản và vai trò.
WinForms Client — MiniSupermarket.WinForms
- FormLogin: nhập tài khoản và mật khẩu.
- SessionManager: lưu thông tin người dùng và token trong phiên chạy ứng dụng.
- ApiClientService: sử dụng HttpClient để gọi API và gắn header Authorization: Bearer <token>.
- Sau khi đăng nhập, mở giao diện chính theo thông tin phiên đăng nhập.
🚀 5. Hướng dẫn chạy và kiểm thử
Bước 1. Chạy Web API
1. Mở Solution bằng Visual Studio 2022.
2. Chọn MiniSupermarket.API làm Startup Project.
3. Nhấn F5 và mở Swagger UI.
Bước 2. Kiểm thử đăng nhập
1. Gọi POST /api/auth/login với thông tin đăng nhập hợp lệ.
2. Kiểm tra phản hồi có success, token và role.
3. Thử tài khoản hoặc mật khẩu không hợp lệ; API phải từ chối đăng nhập.
Bước 3. Kiểm thử API được bảo vệ
1. Gọi API có [Authorize] khi chưa có token: mong đợi HTTP 401 Unauthorized.
2. Gửi token hợp lệ trong Swagger hoặc Client.
3. Với API giới hạn vai trò, thử token có vai trò không phù hợp: mong đợi HTTP 403 Forbidden.
Bước 4. Kiểm thử WinForms
1. Chạy MiniSupermarket.WinForms.
2. Đăng nhập qua FormLogin.
3. Kiểm tra API được gọi kèm Bearer Token.
4. Thử đăng nhập sai và kiểm tra thông báo lỗi.

👨‍💻 6. Thông tin sinh viên
- Họ tên sinh viên: [Lê Thanh Hoàng]
- Mã sinh viên: [2124110130]
- Lớp học phần: [Lập trình ứng dụng]
