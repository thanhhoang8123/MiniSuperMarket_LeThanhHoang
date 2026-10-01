# MiniSupermarket System

## 📚 Thông tin
- Môn học: Lập trình Ứng dụng .NET Core
- Buổi: 2
- Đề tài: Hệ thống quản lý siêu thị mini
- Kiến trúc: Client - Server

---

## 🎯 Mục tiêu Buổi 2

Xây dựng cơ chế **xác thực và phân quyền người dùng** cho hệ thống
thông qua **JWT (JSON Web Token)**.

Các nội dung chính:
- Authentication (Xác thực)
- Authorization (Phân quyền)
- JWT Stateless
- Phân quyền Admin / Cashier
- Đăng nhập trên WinForms
- Gửi Bearer Token khi gọi Web API

---

## 🔐 1. Authentication & Authorization

### Authentication
Kiểm tra danh tính người dùng thông qua tài khoản và mật khẩu.

Sau khi đăng nhập thành công, Web API trả về:
- JWT Token
- Role của người dùng

### Authorization
Kiểm tra quyền của người dùng khi truy cập API.

Hệ thống sử dụng:
- `[Authorize]`: yêu cầu người dùng đã đăng nhập.
- `[Authorize(Roles = "Admin")]`: chỉ cho phép Admin.
- `[Authorize(Roles = "Admin,Cashier")]`: cho phép Admin hoặc Cashier.

---

## 🔑 2. JWT Authentication

JWT được sử dụng theo mô hình **Stateless**.

Server không lưu Session của người dùng trên RAM. 
Thông tin xác thực được chứa trong JWT Token và Client gửi Token
trong các request tiếp theo.

JWT gồm 3 phần:
- Header
- Payload (Claims)
- Signature

Các thông tin như Username, Role và thời hạn Token được sử dụng
để xác định người dùng và quyền truy cập. :contentReference[oaicite:2]{index=2}

---

## ⚙️ 3. Backend - ASP.NET Core Web API

### AuthController

Xây dựng API đăng nhập:

```text
POST /api/auth/login

Client gửi:

{
    "username": "admin",
    "password": "123456"
}

Sau khi xác thực thành công, API trả về JWT Token và Role.

Cấu hình JWT

JWT được cấu hình trong:

Program.cs

Sử dụng:

JwtBearer
Secret Key
Authentication Middleware
Authorization Middleware

Các API cần bảo vệ được đánh dấu bằng [Authorize].

🖥️ 4. WinForms Client

Xây dựng màn hình:

FormLogin

Các chức năng:

Nhập Username và Password.
Gửi yêu cầu đăng nhập đến Web API.
Nhận JWT Token.
Lưu Token và Role.
Mở FormCategoryManagement sau khi đăng nhập thành công.
SessionManager

Lưu thông tin phiên làm việc:

JwtToken
CurrentRole

SessionManager giúp các Form khác sử dụng Token khi gọi API.

🛡️ 5. Gửi Bearer Token

Các request từ WinForms đến API được đính kèm:

Authorization: Bearer <JWT_TOKEN>

Ví dụ:

client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue(
        "Bearer",
        SessionManager.JwtToken);

Nhờ đó WinForms có thể gọi các API đã được bảo vệ bằng
[Authorize].

🧪 6. Kiểm thử phân quyền
Trường hợp	Kết quả
Chưa đăng nhập gọi API bảo vệ	401 Unauthorized
Đăng nhập thành công	Nhận JWT Token
Cashier gọi API được phép	200 OK
Cashier gọi API chỉ dành cho Admin	403 Forbidden
Admin gọi API Admin	Được phép
🔄 7. Luồng hoạt động
┌──────────────┐
│   FormLogin  │
└──────┬───────┘
       │ Username + Password
       ▼
┌────────────────────┐
│  POST /auth/login  │
└─────────┬──────────┘
          │
          ▼
┌────────────────────┐
│      JWT Token     │
│   + Role           │
└─────────┬──────────┘
          │
          ▼
┌────────────────────┐
│   SessionManager   │
└─────────┬──────────┘
          │ Bearer Token
          ▼
┌────────────────────┐
│      Web API       │
│ [Authorize]        │
└─────────┬──────────┘
          │
          ▼
    Admin / Cashier
🛠️ Công nghệ sử dụng
.NET 8
ASP.NET Core Web API
Windows Forms
JWT Authentication
REST API
Swagger
C#
Visual Studio
📁 Cấu trúc chính
MiniSupermarket
│
├── MiniSupermarket.API
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   └── CategoriesController.cs
│   ├── Models
│   └── Program.cs
│
└── MiniSupermarket.WinForms
    ├── FormLogin.cs
    ├── FormCategoryManagement.cs
    └── SessionManager.cs
