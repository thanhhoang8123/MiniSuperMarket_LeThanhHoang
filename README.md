# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 6 – EF Core nâng cao, phân trang LINQ và tích hợp Client–Server

---

## 🏗️ 1. Mục tiêu buổi thực hành

- Tối ưu truy vấn dữ liệu quan hệ với Entity Framework Core.
- Sử dụng `Include()` và `ThenInclude()` để nạp dữ liệu quan hệ.
- Hạn chế truy vấn dư thừa và lỗi N+1 Query.
- Sử dụng DTO để định dạng dữ liệu trả về từ Web API.
- Xây dựng phân trang phía máy chủ bằng `Skip()` và `Take()`.
- Kết hợp tìm kiếm với phân trang và tính lại tổng số trang sau khi lọc.
- Tích hợp WinForms Client với ASP.NET Core Web API bằng `HttpClient` và JSON.
- Hiển thị dữ liệu phân trang trên `DataGridView`, hỗ trợ tìm kiếm, lọc danh mục và CRUD.

## 🛠️ 2. Công nghệ sử dụng

- C# / .NET 8
- ASP.NET Core Web API
- Entity Framework Core (EF Core) và LINQ
- SQL Server / SQL Server Management Studio (SSMS)
- Windows Forms (WinForms)
- `HttpClient`, `System.Net.Http.Json`
- DTO và JSON
- JWT Bearer Authentication và phân quyền API

## 🧠 3. Tối ưu truy vấn quan hệ trong EF Core

### 3.1. Các chiến lược nạp dữ liệu

| Chiến lược | Đặc điểm |
|---|---|
| Lazy Loading | Nạp dữ liệu liên quan khi thuộc tính được truy cập; có thể gây nhiều truy vấn ngoài ý muốn. |
| Explicit Loading | Chủ động yêu cầu nạp dữ liệu liên quan sau truy vấn ban đầu. |
| Eager Loading | Nạp dữ liệu quan hệ ngay trong truy vấn bằng `Include()` và `ThenInclude()`. |

Ví dụ nạp danh mục của sản phẩm:

```csharp
var products = await _context.Products
    .Include(p => p.Category)
    .AsNoTracking()
    .ToListAsync();
```

Ví dụ nạp nhiều cấp quan hệ từ đơn hàng đến sản phẩm:

```csharp
var orders = await _context.Orders
    .Include(o => o.OrderDetails)
    .ThenInclude(od => od.Product)
    .AsNoTracking()
    .ToListAsync();
```

### 3.2. Vấn đề N+1 Query

N+1 Query xảy ra khi ứng dụng truy vấn danh sách chính rồi tiếp tục phát sinh thêm truy vấn cho từng bản ghi để lấy dữ liệu liên quan. Với danh sách lớn, cách làm này tăng số lần truy cập cơ sở dữ liệu và giảm hiệu năng.

**Cách hạn chế:** dùng truy vấn quan hệ phù hợp với `Include()` hoặc chiếu thẳng dữ liệu cần thiết sang DTO bằng `Select()`.

### 3.3. `AsNoTracking()`

Dùng `AsNoTracking()` cho các truy vấn chỉ đọc để EF Core không theo dõi thay đổi của các entity, giúp giảm chi phí bộ nhớ và xử lý trong các màn hình danh sách.

```csharp
var customers = await _context.Customers
    .AsNoTracking()
    .ToListAsync();
```

### 3.4. DTO và vòng lặp tham chiếu JSON

Các entity có quan hệ hai chiều có thể tạo vòng lặp tham chiếu khi tuần tự hóa JSON. DTO giúp định nghĩa rõ dữ liệu cần truyền qua mạng, giảm dữ liệu thừa và tránh để lộ cấu trúc entity không cần thiết.

Ví dụ DTO sản phẩm có thể gồm `ProductId`, `Barcode`, `ProductName`, `Price`, `StockQuantity`, `CategoryId` và `CategoryName`.

Chiếu dữ liệu sang DTO:

```csharp
var items = await _context.Products
    .AsNoTracking()
    .Select(p => new ProductItemDto
    {
        ProductId = p.ProductId,
        Barcode = p.Barcode,
        ProductName = p.ProductName,
        Price = p.Price,
        StockQuantity = p.StockQuantity,
        CategoryId = p.CategoryId,
        CategoryName = p.Category != null
            ? p.Category.CategoryName
            : string.Empty
    })
    .ToListAsync();
```

## 📄 4. Phân trang phía máy chủ bằng LINQ

### 4.1. Vì sao cần server-side pagination?

Không nên tải toàn bộ dữ liệu lớn về WinForms rồi mới chia trang tại máy khách. Phân trang phía máy chủ giúp giảm lượng dữ liệu truyền qua mạng và bộ nhớ cần dùng ở Client.

### 4.2. Công thức phân trang

- `pageNumber`: số trang, bắt đầu từ 1.
- `pageSize`: số bản ghi mỗi trang.
- Số bản ghi cần bỏ qua: `(pageNumber - 1) * pageSize`.
- Tổng số trang: `Math.Ceiling((double)totalItems / pageSize)`.

Ví dụ trang 3, mỗi trang 5 bản ghi thì cần bỏ qua `(3 - 1) * 5 = 10` bản ghi đầu tiên.

### 4.3. Kết hợp `OrderBy()`, `Skip()` và `Take()`

Luôn sắp xếp trước khi phân trang để kết quả có thứ tự ổn định.

```csharp
var items = await _context.Products
    .AsNoTracking()
    .OrderBy(p => p.ProductId)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

EF Core có thể chuyển cách truy vấn này thành SQL Server `ORDER BY ... OFFSET ... FETCH`.

### 4.4. Tìm kiếm kết hợp phân trang

Thứ tự xử lý thông thường:

1. Tạo `IQueryable` ban đầu.
2. Áp dụng điều kiện tìm kiếm nếu có từ khóa.
3. Đếm tổng số bản ghi **sau khi lọc**.
4. Tính `TotalPages`.
5. Sắp xếp dữ liệu.
6. Áp dụng `Skip()` và `Take()`.
7. Trả kết quả gồm trang hiện tại, kích thước trang, tổng bản ghi, tổng trang và danh sách dữ liệu.

Ví dụ lọc khách hàng theo tên hoặc số điện thoại:

```csharp
if (!string.IsNullOrWhiteSpace(keyword))
{
    string search = keyword.Trim();
    query = query.Where(c =>
        c.CustomerName.Contains(search) ||
        c.PhoneNumber.Contains(search));
}

int totalItems = await query.CountAsync();
```

Cần kiểm tra `pageNumber >= 1` và giới hạn `pageSize` hợp lý trước khi truy vấn.

### 4.5. Cấu trúc JSON phân trang

Ví dụ cấu trúc phản hồi API:

```json
{
  "currentPage": 1,
  "pageSize": 5,
  "totalItems": 12,
  "totalPages": 3,
  "items": []
}
```

`items` chứa các bản ghi thuộc trang hiện tại; các trường tổng hợp dùng để điều khiển giao diện phân trang.

## 🔌 5. Tích hợp WinForms Client với Web API

### 5.1. Gọi API bằng `HttpClient`

WinForms gửi HTTP request đến Web API và nhận dữ liệu JSON. Nên dùng `GetFromJsonAsync<T>()`, `PostAsJsonAsync()`, `PutAsJsonAsync()` hoặc `DeleteAsync()` tùy nghiệp vụ. Trong dự án, cần dùng đúng `BaseAddress` và gắn JWT Bearer Token khi endpoint yêu cầu xác thực.

### 5.2. Hiển thị dữ liệu phân trang

Giao diện quản lý thường cần:

- `DataGridView` để hiển thị danh sách của trang hiện tại.
- Nút **Trang trước** và **Trang sau**.
- Nhãn thông tin, ví dụ `Trang 1 / 3`.
- Từ khóa tìm kiếm.
- Các biến trạng thái như `_currentPage`, `_pageSize`, `_totalPages` và `_keyword`.

Khi người dùng tìm kiếm hoặc thay đổi bộ lọc, nên đưa `_currentPage` về 1 rồi tải lại dữ liệu. Nút Trang trước chỉ hoạt động khi trang hiện tại lớn hơn 1; nút Trang sau chỉ hoạt động khi trang hiện tại nhỏ hơn tổng số trang.

### 5.3. Lọc danh mục sản phẩm

ComboBox lọc danh mục cần gửi `categoryId` đến endpoint phân trang. Khi không chọn danh mục cụ thể, có thể bỏ tham số hoặc dùng giá trị quy ước `0` để biểu thị tất cả danh mục; API phải xử lý nhất quán với quy ước của Client.

Ví dụ URL:

```text
GET /api/products/paged?pageNumber=1&pageSize=5&keyword=coca&categoryId=2
```

API áp dụng điều kiện tìm kiếm và lọc danh mục trước khi đếm tổng số bản ghi, sau đó mới thực hiện phân trang.

## 🧩 6. Nội dung đã áp dụng trong MiniSupermarket

### 6.1. Quản lý sản phẩm

- Gọi API lấy danh sách sản phẩm theo trang.
- Tìm kiếm theo tên sản phẩm hoặc Barcode.
- Lọc sản phẩm theo danh mục bằng ComboBox.
- Hiển thị trạng thái trang và điều khiển nút chuyển trang.
- Giữ các thao tác thêm, sửa, xóa sản phẩm thông qua Web API.

### 6.2. Quản lý khách hàng

- API `GET /api/customers/paged` trả dữ liệu theo trang.
- Sắp xếp khách hàng theo `RewardPoints` giảm dần, sau đó theo `CustomerId` tăng dần.
- Hỗ trợ tham số `pageNumber` và `pageSize`.
- Bổ sung tham số `keyword` cho tìm kiếm theo tên hoặc số điện thoại khi dùng phiên bản API đã cập nhật.
- WinForms cần gọi endpoint phân trang và đọc `CurrentPage`, `PageSize`, `TotalItems`, `TotalPages`, `Items` để hiển thị đúng dữ liệu.

> **Lưu ý nghiệm thu:** Chỉ đánh dấu hoàn thành sau khi đã chạy thử trong môi trường dự án. Việc có mã nguồn không tự động chứng minh rằng tất cả kịch bản đã chạy thành công.

## 🧪 7. Kịch bản kiểm thử

| STT | Kịch bản | Kết quả mong đợi |
|---|---|---|
| 1 | Mở màn hình sản phẩm | Hiển thị dữ liệu trang đầu và thông tin phân trang. |
| 2 | Chuyển sang trang sau | Danh sách thay đổi đúng trang; không lặp dữ liệu do thiếu sắp xếp. |
| 3 | Tìm kiếm sản phẩm | Danh sách chỉ chứa kết quả khớp từ khóa; tổng số trang được tính lại. |
| 4 | Chọn danh mục | Danh sách chỉ chứa sản phẩm thuộc danh mục đã chọn. |
| 5 | Kết hợp tìm kiếm và lọc danh mục | Hai điều kiện được áp dụng đồng thời trước khi phân trang. |
| 6 | Tìm khách hàng theo tên | Trả về các khách hàng khớp từ khóa, phân trang đúng. |
| 7 | Tìm khách hàng theo số điện thoại | Trả về kết quả phù hợp và tổng số trang chính xác. |
| 8 | Từ khóa không có kết quả | Danh sách rỗng; thông tin trang không gây lỗi. |
| 9 | Thêm/sửa/xóa dữ liệu | API phản hồi phù hợp và danh sách được tải lại. |
| 10 | API yêu cầu xác thực | Request có JWT hợp lệ; xử lý rõ lỗi 401/403. |

## 🚀 8. Chạy và kiểm tra dự án

1. Kiểm tra connection string của API trỏ đúng SQL Server/database.
2. Chạy `MiniSupermarket.API` và xác nhận API khởi động thành công.
3. Đăng nhập để lấy JWT hợp lệ nếu endpoint yêu cầu xác thực.
4. Kiểm tra endpoint phân trang bằng Swagger hoặc công cụ HTTP.
5. Chạy WinForms Client và kiểm thử tìm kiếm, lọc, chuyển trang, CRUD.
6. Đối chiếu dữ liệu với SQL Server khi cần.
7. Chạy **Build → Rebuild Solution** và xử lý hết lỗi trước khi commit.


## 👨‍💻 10. Thông tin sinh viên

- **Họ tên sinh viên:** [Lê Thanh Hoàng]
- **Mã sinh viên:** [2124110130]
- **Lớp học phần:** [Lập trình ứng dụng]

---
