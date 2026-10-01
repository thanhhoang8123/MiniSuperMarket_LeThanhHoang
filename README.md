 MiniSupermarket System

📚 Thông tin
- Môn học: Lập trình Ứng dụng .NET Core
- Buổi: 3
- Đề tài: Hệ thống quản lý siêu thị mini
- Kiến trúc: Client - Server

---

🎯 Mục tiêu Buổi 3

Chuyển hệ thống từ cơ chế lưu trữ dữ liệu tạm thời trên RAM sang
**Microsoft SQL Server** thông qua **Entity Framework Core (EF Core)**.

Các nội dung chính:
- SQL Server
- Entity Framework Core
- Code-First
- DbContext
- EF Core Migrations
- LINQ và async/await
- CRUD với dữ liệu thực tế
- Kiểm tra dữ liệu bằng SSMS

---
🗄️ 1. Tích hợp SQL Server

Ở Buổi 1 và Buổi 2, dữ liệu được lưu tạm trong bộ nhớ RAM nên
sẽ mất khi Web API khởi động lại.

Buổi 3 chuyển sang lưu trữ lâu dài bằng SQL Server:

```text
MiniSupermarketDb
├── Categories
└── Products

Dữ liệu được lưu trực tiếp trong cơ sở dữ liệu và vẫn tồn tại
sau khi tắt hoặc khởi động lại ứng dụng.

🔧 2. Entity Framework Core

Sử dụng EF Core theo phương pháp Code-First.

Các package chính:

Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design

EF Core đóng vai trò kết nối giữa các Model C# và SQL Server.

📦 3. DbContext

Xây dựng:

SupermarketDbContext

DbContext quản lý các Entity và kết nối đến SQL Server.

Các DbSet chính:

DbSet<Category>
DbSet<Product>

Connection String được cấu hình trong:

appsettings.json

Sau đó đăng ký SupermarketDbContext vào Program.cs
thông qua Dependency Injection.

🔄 4. EF Core Migrations

Sử dụng Migration để tạo và cập nhật cơ sở dữ liệu.

Các lệnh chính:

Add-Migration InitialCreateDatabase
Update-Database

Add-Migration tạo mã Migration dựa trên các Model.

Update-Database áp dụng Migration vào SQL Server và tạo
cấu trúc cơ sở dữ liệu.

🌐 5. Web API với EF Core

CategoriesController được chuyển từ dữ liệu In-Memory
sang truy vấn trực tiếp SQL Server thông qua DbContext.

Các chức năng CRUD:

Method	Endpoint	Chức năng
GET	/api/categories	Lấy danh sách
GET	/api/categories/{id}	Lấy theo ID
POST	/api/categories	Thêm
PUT	/api/categories/{id}	Cập nhật
DELETE	/api/categories/{id}	Xóa

Sử dụng các phương thức bất đồng bộ như:

ToListAsync()
FindAsync()
SaveChangesAsync()

Đối với các truy vấn chỉ đọc, sử dụng AsNoTracking() để không
cần theo dõi Entity trong DbContext.

🖥️ 6. WinForms Client

WinForms tiếp tục sử dụng:

HttpClient
System.Net.Http.Json
JWT Bearer Token từ Buổi 2

Các thao tác trên FormCategoryManagement được thực hiện
thông qua Web API.

WinForms
    ↓
HttpClient
    ↓
ASP.NET Core Web API
    ↓
Entity Framework Core
    ↓
SQL Server

Logic CRUD phía WinForms tiếp tục sử dụng API, chỉ thay đổi
nguồn dữ liệu phía Backend từ RAM sang SQL Server.

👥 7. Bài tập mở rộng - Quản lý khách hàng

Xây dựng thêm module Customers.

Customer gồm:
CustomerId
CustomerName
PhoneNumber
Address
RewardPoints
MembershipRank

Các nội dung thực hiện:

Tạo Customer.cs.
Thêm DbSet<Customer> vào SupermarketDbContext.
Tạo Migration cập nhật CSDL.
Xây dựng CustomersController.
Thực hiện CRUD khách hàng.
Xây dựng FormCustomerManagement trên WinForms.
Tìm kiếm khách hàng theo tên hoặc số điện thoại.
🧪 8. Kiểm thử
Swagger

Kiểm tra:

GET    /api/categories
POST   /api/categories
PUT    /api/categories/{id}
DELETE /api/categories/{id}
SQL Server

Sử dụng SQL Server Management Studio (SSMS) để kiểm tra
dữ liệu trong các bảng.

Ví dụ:

SELECT * FROM dbo.Categories;
WinForms

Thực hiện:

Thêm dữ liệu
Cập nhật dữ liệu
Xóa dữ liệu
Tải lại dữ liệu

Sau đó tắt Web API và WinForms rồi chạy lại để kiểm tra dữ liệu
vẫn còn trong SQL Server.

🛠️ Công nghệ sử dụng
.NET 8
ASP.NET Core Web API
WinForms
Entity Framework Core
SQL Server
LINQ
JWT Authentication
Swagger
SSMS
C#
Visual Studio
📁 Cấu trúc chính
MiniSupermarket
│
├── MiniSupermarket.API
│   ├── Controllers
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   └── CustomersController.cs
│   │
│   ├── Data
│   │   └── SupermarketDbContext.cs
│   │
│   ├── Models
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   └── Customer.cs
│   │
│   ├── Migrations
│   ├── Program.cs
│   └── appsettings.json
│
└── MiniSupermarket.WinForms
    ├── FormLogin.cs
    ├── FormCategoryManagement.cs
    ├── FormCustomerManagement.cs
    └── SessionManager.cs
✅ Kết quả Buổi 3

Hoàn thành:

Kết nối ASP.NET Core Web API với SQL Server.
Sử dụng EF Core Code-First.
Tạo và quản lý Database bằng Migrations.
Chuyển CRUD từ In-Memory sang SQL Server.
Sử dụng LINQ kết hợp async/await.
Kiểm tra dữ liệu bằng Swagger và SSMS.
Xây dựng module quản lý khách hàng.
Đảm bảo dữ liệu vẫn tồn tại sau khi khởi động lại ứng dụng.
