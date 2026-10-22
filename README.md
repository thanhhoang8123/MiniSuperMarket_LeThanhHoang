🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)
Buổi thực hành: Buổi 5 - ADO.NET nâng cao, Stored Procedure và giao dịch POS Checkout

🏗️ 1. Mục tiêu buổi thực hành
- Sử dụng ADO.NET để truy vấn và cập nhật SQL Server.
- Chọn đúng phương thức thực thi của SqlCommand.
- Tham số hóa câu lệnh SQL để giảm nguy cơ SQL Injection.
- Xây dựng API tra cứu sản phẩm theo barcode.
- Dùng Stored Procedure để tổng hợp báo cáo doanh thu.
- Dùng SqlTransaction để bảo đảm quy trình checkout nhất quán.
- Kiểm thử trường hợp thanh toán thành công và rollback khi dữ liệu không hợp lệ.
🛠️ 2. Công nghệ sử dụng
- C# / .NET 8.0
- Microsoft.Data.SqlClient
- ADO.NET: SqlConnection, SqlCommand, SqlParameter, SqlDataReader, DataTable
- SQL Server / SSMS
- Stored Procedure và T-SQL
- SqlTransaction
- ASP.NET Core Web API, JWT Bearer
- WinForms Client và Swagger UI
🧠 3. Các thành phần ADO.NET cốt lõi
Thành phần	Vai trò
SqlConnection	Mở kết nối tới SQL Server.
SqlCommand	Thực thi câu lệnh SQL hoặc Stored Procedure.
SqlParameter	Truyền dữ liệu bằng tham số, tránh ghép trực tiếp đầu vào vào chuỗi SQL.
SqlDataReader	Đọc dữ liệu theo luồng, một chiều và chỉ đọc.
DataTable	Lưu dữ liệu trong bộ nhớ để sử dụng sau khi truy vấn.


Luôn quản lý tài nguyên kết nối bằng using hoặc await using để giải phóng tài nguyên khi hoàn thành hoặc khi xảy ra ngoại lệ.
3.1. Ba phương thức thực thi
- ExecuteScalar(): lấy một giá trị đơn, thường dùng cho COUNT, SUM hoặc ID mới.
- ExecuteNonQuery(): thực thi lệnh ghi và nhận số dòng bị ảnh hưởng.
- ExecuteReader(): đọc tập kết quả gồm nhiều dòng/cột.
🔒 4. An toàn truy vấn SQL
Không ghép trực tiếp dữ liệu người dùng vào câu SQL. Sử dụng tham số:
string sql = "SELECT ProductId, ProductName FROM Products WHERE Barcode = @Barcode";

using var command = new SqlCommand(sql, connection);
command.Parameters.AddWithValue("@Barcode", barcode);
Các câu lệnh ghi dữ liệu cũng cần tham số hóa. Kết nối và command phải được giải phóng đúng cách.
🏷️ 5. API tra cứu Barcode
Controller ADO.NET cung cấp endpoint tra cứu sản phẩm theo mã vạch:

GET /api/ado-products/barcode/{barcode}

Quy trình xử lý:
1. Nhận barcode từ route.
2. Mở SqlConnection.
3. Tạo SqlCommand với tham số.
4. Dùng SqlDataReader đọc thông tin sản phẩm.
5. Trả JSON khi tìm thấy; trả phản hồi phù hợp khi không có sản phẩm.
Dự án cũng có thể sử dụng DataTable/SqlDataAdapter cho trường hợp cần tải dữ liệu theo mô hình ngắt kết nối.
📊 6. Stored Procedure và báo cáo doanh thu
Stored Procedure được triển khai trong SQL Server:

dbo.sp_GetRevenueByCashier

API báo cáo:

GET /api/reports/revenue-by-cashier

API gọi Stored Procedure bằng SqlCommand với CommandType.StoredProcedure, đọc kết quả bằng SqlDataReader và trả JSON chứa các trường tổng hợp theo nhân viên, gồm tên nhân viên, số hóa đơn và doanh thu.

API báo cáo được giới hạn vai trò Admin. WinForms gọi API để hiển thị tổng số hóa đơn, tổng doanh thu và bảng chi tiết doanh thu theo nhân viên.
Phạm vi báo cáo hiện tại tổng hợp theo nhân viên; API chưa lọc theo ngày và chưa trả về mặt hàng bán chạy nhất. Giao diện không nên hiển thị những chỉ số mà API chưa cung cấp.

💳 7. POS Checkout và SqlTransaction
Checkout thực hiện nhiều thao tác liên quan trong cùng một giao dịch:
1. Kiểm tra giỏ hàng và tồn kho.
2. Lấy thông tin sản phẩm và giá từ cơ sở dữ liệu.
3. Tạo hóa đơn trong Orders.
4. Ghi các dòng hàng vào OrderDetails.
5. Trừ số lượng tồn kho.
6. Cập nhật điểm thưởng khách hàng nếu có.
7. Commit() khi tất cả thao tác thành công; Rollback() nếu có lỗi.
Các command tham gia giao dịch phải gắn cùng một SqlTransaction. Giá và tồn kho cần được kiểm tra từ cơ sở dữ liệu, không tin cậy dữ liệu giá do Client tự gửi.
7.1. ACID
- Atomicity: tất cả thao tác cùng thành công hoặc cùng bị hoàn tác.
- Consistency: dữ liệu vẫn thỏa mãn các ràng buộc nghiệp vụ.
- Isolation: giao dịch đồng thời không được làm sai trạng thái dữ liệu.
- Durability: dữ liệu đã commit được lưu bền vững.
🧪 8. Kiểm thử
Kịch bản 1 — Tra cứu barcode
- Barcode tồn tại: API trả HTTP 200 OK và thông tin sản phẩm.
- Barcode không tồn tại: API trả phản hồi không tìm thấy phù hợp.
Kịch bản 2 — Checkout thành công
- Gửi giỏ hàng có số lượng không vượt tồn kho.
- Kiểm tra hóa đơn, chi tiết hóa đơn và số lượng tồn kho sau thanh toán.
- Kiểm tra tên nhân viên lấy từ claim của JWT.
Kịch bản 3 — Checkout thất bại và rollback
- Gửi giỏ hàng có ít nhất một sản phẩm có số lượng vượt tồn kho.
- Xác nhận checkout thất bại.
- Kiểm tra tồn kho không bị trừ một phần và không tạo hóa đơn/chi tiết dở dang.
Kịch bản 4 — Báo cáo doanh thu
- Chạy Stored Procedure trong SSMS.
- Gọi GET /api/reports/revenue-by-cashier bằng token Admin trên Swagger.
- Mở WinForms, vào Báo cáo và xác nhận dữ liệu được lấy từ API.
🚀 9. Chạy dự án
1. Kiểm tra connection string của API trỏ tới đúng database.
2. Chạy MiniSupermarket.API.
3. Đăng nhập để lấy JWT hợp lệ.
4. Kiểm thử các endpoint bằng Swagger hoặc WinForms.
5. Chạy WinForms Client và thử checkout/báo cáo với dữ liệu phù hợp.
6. Đối soát kết quả bằng SQL Server khi cần.
👨‍💻 10. Thông tin sinh viên
- Họ tên sinh viên: [Lê Thanh Hoàng]
- Mã sinh viên: [2124110130]
- Lớp học phần: [Lập trình ứng dụng]
