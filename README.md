# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 3 - Tích hợp SQL Server và Entity Framework Core Code-First

---

## 🏗️ 1. Mục tiêu buổi thực hành

- Kết nối ASP.NET Core Web API với SQL Server.
- Xây dựng Entity Model và `DbContext`.
- Sử dụng Entity Framework Core (EF Core) theo phương pháp Code-First.
- Tạo cơ sở dữ liệu bằng EF Core Migrations.
- Chuyển API quản lý dữ liệu sang EF Core và xử lý bất đồng bộ.
- Kết nối WinForms Client với API sử dụng dữ liệu thực tế.

## 🛠️ 2. Công nghệ sử dụng

- C# / .NET 8.0
- ASP.NET Core Web API
- SQL Server và SQL Server Management Studio (SSMS)
- Entity Framework Core
- EF Core SQL Server Provider
- EF Core Tools
- LINQ, `async` / `await`
- Swagger UI
- Windows Forms và `HttpClient`

## 🗄️ 3. Kiến trúc truy cập dữ liệu

Luồng xử lý chính:

`WinForms Client → HTTP Request → ASP.NET Core Web API → EF Core → SQL Server`

Web API chịu trách nhiệm xử lý nghiệp vụ và dữ liệu; WinForms chỉ gọi API, nhận JSON và hiển thị kết quả.

## 🧱 4. Entity Model và DbContext

### 4.1. Model `Category`

Model đại diện cho bảng danh mục, thường có các thuộc tính:

- `CategoryId`: khóa chính, tự tăng.
- `CategoryName`: tên danh mục, bắt buộc và giới hạn độ dài.
- `Description`: mô tả tùy chọn.
- `Products`: quan hệ một-nhiều với sản phẩm nếu được cấu hình trong Model.

### 4.2. Model `Product`

Model sản phẩm có các thông tin như mã vạch, tên sản phẩm, giá bán, số lượng tồn kho và khóa ngoại `CategoryId` liên kết danh mục.

### 4.3. `SupermarketDbContext`

`DbContext` đại diện cho phiên làm việc với cơ sở dữ liệu. Các `DbSet<T>` ánh xạ tới các bảng dữ liệu, ví dụ `Categories` và `Products`.

Có thể cấu hình dữ liệu khởi tạo (*data seeding*) bằng `OnModelCreating()` và `HasData()`.

## ⚙️ 5. Cấu hình SQL Server và EF Core

### Bước 1. Cài NuGet

Trong project API, cài các gói EF Core tương thích với phiên bản .NET đang dùng, gồm SQL Server Provider và EF Core Tools.

### Bước 2. Khai báo Connection String

Trong `appsettings.json`, cấu hình `ConnectionStrings:DefaultConnection` trỏ tới SQL Server và database của dự án.

> Không commit mật khẩu hoặc thông tin bí mật của môi trường thật lên Git. Nếu dùng SQL Authentication, nên cấu hình bằng User Secrets hoặc biến môi trường.

### Bước 3. Đăng ký DbContext

Trong `Program.cs`, đăng ký `SupermarketDbContext` qua Dependency Injection và cấu hình `UseSqlServer(connectionString)`.

### Bước 4. Tạo database bằng Migration

Mở Package Manager Console hoặc Terminal tại đúng project, tạo migration và cập nhật database bằng EF Core Tools. Tên migration phụ thuộc vào nội dung thay đổi của Model.

Ví dụ lệnh:

```powershell
Add-Migration InitialCreate
Update-Database
```

Nếu dùng CLI:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Chỉ chạy migration đầu tiên khi dự án chưa có migration tương ứng; nếu đã có migration, hãy dùng tên mới phù hợp với thay đổi.

## 🔄 6. Sử dụng EF Core trong Controller

Các thao tác cơ bản thường dùng:

- `ToListAsync()` để lấy danh sách.
- `FindAsync()` để tìm theo khóa chính.
- `Add()` và `SaveChangesAsync()` để thêm mới.
- Cập nhật entity rồi gọi `SaveChangesAsync()`.
- `Remove()` và `SaveChangesAsync()` để xóa.
- `Where()` / `FirstOrDefaultAsync()` để lọc hoặc tìm dữ liệu.
- Trả `NotFound()` khi không có bản ghi tương ứng và `BadRequest()` khi dữ liệu không hợp lệ.

Sử dụng `async` / `await` cho thao tác I/O để tránh chặn luồng xử lý trong khi chờ SQL Server.

## 🧪 7. Chạy và kiểm thử

1. Kiểm tra `DefaultConnection`.
2. Cập nhật database bằng migration.
3. Chạy `MiniSupermarket.API`.
4. Dùng Swagger kiểm tra GET, POST, PUT và DELETE cho API tương ứng.
5. Chạy WinForms Client và kiểm tra dữ liệu hiển thị khớp với SQL Server.
6. Kiểm tra trường hợp ID không tồn tại và dữ liệu đầu vào không hợp lệ.

## 👨‍💻 9. Thông tin sinh viên

- **Họ tên sinh viên:** [Lê Thanh Hoàng]
- **Mã sinh viên:** [2124110130]
- **Lớp học phần:** [Lập trình ứng dụng]
