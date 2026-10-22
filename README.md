🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)
Môn học: Lập trình Ứng dụng .NET Core (Mã môn: 229162)
Buổi thực hành: Buổi 4 - WinForms Shell, Sidebar, Main Workspace và phân quyền

🏗️ 1. Mục tiêu buổi thực hành
- Xây dựng giao diện khung chính (Shell) cho WinForms.
- Chia giao diện thành Sidebar, Header và vùng làm việc chính.
- Nhúng các Form nghiệp vụ vào vùng nội dung mà không mở nhiều cửa sổ rời.
- Điều hướng chức năng theo vai trò người dùng.
- Tổ chức giao diện theo mô hình RBAC (Role-Based Access Control).
🛠️ 2. Công nghệ sử dụng
- C# / .NET 8.0
- Windows Forms
- Panel, Button, Label, Form
- DockStyle, FormBorderStyle, TopLevel
- SessionManager và vai trò từ phiên đăng nhập
- JWT Authentication phía API
🧭 3. Cấu trúc giao diện Shell
Giao diện chính gồm ba vùng:
1. panelSidebar: chứa các nút điều hướng.
2. panelTopHeader: hiển thị tiêu đề màn hình và thông tin tài khoản.
3. panelMainContent: vùng chứa Form nghiệp vụ đang mở.
Cấu trúc này giúp giao diện nhất quán và cho phép chuyển đổi chức năng trong cùng cửa sổ chính.
🧩 4. Nhúng Form con động
Khi mở một màn hình nghiệp vụ, Form con được thiết lập:
childForm.TopLevel = false;
childForm.FormBorderStyle = FormBorderStyle.None;
childForm.Dock = DockStyle.Fill;
Sau đó, Form con được thêm vào panelMainContent và gọi Show().

Nên đóng hoặc giải phóng Form con đang hoạt động trước khi thay bằng Form mới để tránh giữ tài nguyên không cần thiết. Tiêu đề vùng Header cũng cần cập nhật theo màn hình đang mở.
🔐 5. Điều hướng theo vai trò
Hệ thống sử dụng thông tin vai trò đã lưu trong phiên đăng nhập để quyết định nút nào được hiển thị hoặc bật.

Các vai trò nghiệp vụ trong tài liệu:
- Admin: quản trị hệ thống và truy cập các chức năng quản lý được cấp quyền.
- Cashier: sử dụng các chức năng phục vụ quầy thu ngân.
- Warehouse: sử dụng các chức năng quản lý hàng hóa/kho được cấp quyền.
Việc ẩn nút trên WinForms chỉ giúp điều hướng giao diện. Backend vẫn cần kiểm tra [Authorize] và vai trò ở những API cần bảo vệ; không xem giao diện là lớp bảo mật duy nhất.
🧱 6. Các màn hình nghiệp vụ
Tùy phạm vi đã triển khai trong Solution, Shell có thể điều hướng đến các Form như:
- POS / thanh toán.
- Quản lý danh mục.
- Quản lý sản phẩm.
- Quản lý khách hàng.
- Quản lý hóa đơn.
- Báo cáo.
- Quản lý người dùng.
Chỉ những màn hình đã có trong Solution và được nối sự kiện điều hướng mới sử dụng được; danh sách trên không có nghĩa mọi chức năng đều đã hoàn thiện.
🚀 7. Hướng dẫn kiểm thử
1. Chạy API và WinForms.
2. Đăng nhập bằng tài khoản thuộc từng vai trò.
3. Kiểm tra tên tài khoản và vai trò ở Header.
4. Kiểm tra các nút Sidebar theo vai trò.
5. Chuyển qua lại giữa các Form; xác nhận Form con hiển thị trong panelMainContent.
6. Thử gọi trực tiếp API bằng token không đủ quyền để xác nhận Backend vẫn chặn truy cập.
👨‍💻 8. Thông tin sinh viên
- Họ tên sinh viên: [Lê Thanh Hoàng]
- Mã sinh viên: [2124110130]
- Lớp học phần: [Lập trình di động]
