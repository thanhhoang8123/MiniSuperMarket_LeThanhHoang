using System;



using System.Drawing;



using System.Windows.Forms;



namespace MiniSupermarket.WinForms



{



    public partial class FormMainShell : Form



    {



        // Biến lưu trữ Form con đang được kích hoạt hiển thị



        private Form? _activeForm = null;
        private Button? _currentActiveButton = null;



        // btnOrders is in Designer



        public FormMainShell()



        {



            InitializeComponent();



            EnableDoubleBuffering();
            foreach (Control ctrl in panelNav.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.MouseEnter += (s, e) => {
                        if (s is Button b && b != _currentActiveButton)
                        {
                            b.BackColor = Color.FromArgb(30, 41, 59);
                            b.ForeColor = Color.White;
                        }
                    };
                    btn.MouseLeave += (s, e) => {
                        if (s is Button b && b != _currentActiveButton)
                        {
                            b.BackColor = Color.FromArgb(15, 23, 42);
                            b.ForeColor = Color.FromArgb(203, 213, 225);
                        }
                    };
                }
            }
            btnLogout.MouseEnter += (s, e) => btnLogout.BackColor = Color.FromArgb(30, 41, 59);
            btnLogout.MouseLeave += (s, e) => btnLogout.BackColor = Color.FromArgb(9, 13, 22);



            // btnOrders initialized in Designer



        }



        /// <summary>



        /// Kích hoạt cơ chế bộ đệm kép (Double Buffering) chống nhấp nháy màn hình



        /// </summary>



        private void EnableDoubleBuffering()



        {



            this.DoubleBuffered = true;



            try



            {



                typeof(Panel).InvokeMember(



                    "DoubleBuffered",



                    System.Reflection.BindingFlags.SetProperty |



                    System.Reflection.BindingFlags.Instance |



                    System.Reflection.BindingFlags.NonPublic,



                    null,



                    panelMainContent,



                    new object[] { true });



            }



            catch



            {



                // Bỏ qua nếu môi trường không hỗ trợ reflection



            }



        }







        private void FormMainShell_Load(object sender, EventArgs e)



        {



            // 1. Hiển thị thông tin phiên người dùng đăng nhập



            lblUserInfo.Text = $"👤  {SessionManager.CurrentUsername}   •   [{SessionManager.CurrentRole}]";



            // 2. Kích hoạt phân quyền giao diện theo vai trò (Role-Based Access)



            ApplyRolePermissions(SessionManager.CurrentRole);



            // 3. Mở màn hình mặc định tương ứng với vai trò



            OpenDefaultScreenByRole(SessionManager.CurrentRole);



        }



        /// <summary>



        /// Hàm nhúng động một Form con vào vùng panelMainContent



        /// </summary>



        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)



        {



            // 1. Đóng và giải phóng tài nguyên của form con trước đó để tiết kiệm bộ nhớ RAM



            if (_activeForm != null)



            {



                _activeForm.Close();



                _activeForm.Dispose();



            }



            HighlightActiveButton(senderButton);



            _activeForm = childForm;



            // 2. CẤU HÌNH NHÚNG FORM CON MƯỢT MÀ VÀO PANEL TRUNG TÂM (Chống giật lag)



            childForm.TopLevel = false;                          // Bỏ chế độ cửa sổ cấp cao độc lập



            childForm.FormBorderStyle = FormBorderStyle.None;     // Ẩn thanh viền và nút phóng to/thu nhỏ Windows



            childForm.Dock = DockStyle.Fill;                     // Tự động kéo dãn vừa khít toàn bộ panel trung tâm



            // 3. Tạm dừng layout để gom các thao tác vẽ lại (tránh hiện tượng chớp trắng giật màn hình)



            panelMainContent.SuspendLayout();



            panelMainContent.Controls.Clear();                   // Dọn dẹp điều khiển màn hình cũ



            panelMainContent.Controls.Add(childForm);            // Nhúng form mới vào vùng chứa



            panelMainContent.Tag = childForm;



            panelMainContent.ResumeLayout();                     // Tiếp tục vẽ sau khi nạp xong hoàn tất



            lblTitle.Text = screenTitle;                        // Đồng bộ tên chức năng trên Header



            childForm.BringToFront();



            childForm.Show();



        }



        /// <summary>



        /// Làm nổi bật nút menu bên Sidebar đang được chọn



        /// </summary>



        private void HighlightActiveButton(Button activeButton)



        {



            _currentActiveButton = activeButton;
            foreach (Control ctrl in panelNav.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(15, 23, 42);
                    btn.ForeColor = Color.FromArgb(203, 213, 225);
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }

            activeButton.BackColor = Color.FromArgb(37, 99, 235);
            activeButton.ForeColor = Color.White;
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);



        }



        /// <summary>



        /// Phân định quyền truy cập hiển thị theo vai trò người dùng



        /// </summary>



        private void ApplyRolePermissions(string role)



        {



            switch (role.ToUpper())



            {



                case "ADMIN":



                    // Quản trị viên: Có toàn quyền sử dụng tất cả các nút



                    btnPOS.Visible = true;



                    btnCategory.Visible = true;



                    btnProduct.Visible = true;



                    btnCustomer.Visible = true;



                    btnReports.Visible = true;



                    btnUserManage.Visible = true;



                    btnOrders.Visible = true;



                    break;
                case "CASHIER":
                    btnPOS.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnOrders.Visible = true;
                    btnCategory.Visible = false;
                    btnReports.Visible = false;
                    btnUserManage.Visible = false;
                    break;



                case "WAREHOUSE":



                    // Thủ kho: Chỉ quản lý Danh mục nhóm hàng và Sản phẩm tồn kho



                    btnPOS.Visible = false;



                    btnCustomer.Visible = false;



                    btnReports.Visible = false;



                    btnUserManage.Visible = false;



                    btnCategory.Visible = true;



                    btnProduct.Visible = true;



                    btnOrders.Visible = false;



                    break;



                default:



                    // Vai trò không xác định: Khóa toàn bộ



                    MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);



                    this.Close();



                    break;



            }



        }



        /// <summary>



        /// Điều hướng ngay vào màn hình đúng chuyên môn của từng vai trò



        /// </summary>



        private void OpenDefaultScreenByRole(string role)



        {



            switch (role.ToUpper())



            {



                case "ADMIN":



                case "WAREHOUSE":



                    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);



                    break;
                case "CASHIER":
                    OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
                    break;



            }



        }



        // ================= CÁC SỰ KIỆN CLICK NÚT TRÊN SIDEBAR =================



        private void btnCategory_Click(object sender, EventArgs e)



        {



            OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC SẢN PHẨM", btnCategory);



        }



        private void btnProduct_Click(object sender, EventArgs e)



        {



            OpenChildForm(



                new FormProductManagement(),



                "QUẢN LÝ SẢN PHẨM",



                btnProduct);



        }



        private void btnCustomer_Click(object sender, EventArgs e)



        {



            OpenChildForm(



                new FormCustomerManagement(),



                "QUẢN LÝ KHÁCH HÀNG THÂN THIẾT",



                btnCustomer);



        }



        private void btnPOS_Click(object sender, EventArgs e)



        {



            OpenChildForm(



                new FormPOS(),



                "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)",



                btnPOS);



        }



        private void btnLogout_Click(object sender, EventArgs e)



        {



            var confirm = MessageBox.Show(



                "Bạn có chắc chắn muốn đăng xuất phiên làm việc?",



                "Xác nhận",



                MessageBoxButtons.YesNo,



                MessageBoxIcon.Question);



            if (confirm == DialogResult.Yes)



            {



                SessionManager.JwtToken = string.Empty;



                SessionManager.CurrentUsername = string.Empty;



                SessionManager.CurrentRole = string.Empty;



                this.Hide();



                new FormLogin().ShowDialog();



                this.Close();



            }



        }



        private void btnReports_Click(object sender, EventArgs e)



        {



            if (SessionManager.CurrentRole != "Admin")



            {



                MessageBox.Show(



                    "Bạn không có quyền xem dữ liệu tài chính của cửa hàng!",



                    "Từ chối",



                    MessageBoxButtons.OK,



                    MessageBoxIcon.Stop);



                return;



            }



            OpenChildForm(



                new FormQuickReport(),



                "BÁO CÁO DOANH THU & HIỆU SUẤT CA BÁN",



                btnReports);



        }



        private void btnOrders_Click(object? sender, EventArgs e)



        {



            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase))



            {



                MessageBox.Show(



                    "Bạn không có quyền xem danh sách đơn hàng.",



                    "Từ chối truy cập",



                    MessageBoxButtons.OK,



                    MessageBoxIcon.Warning);



                return;



            }



            OpenChildForm(



                new FormOrderManagement(),



                "QUẢN LÝ ĐƠN HÀNG",



                btnOrders);



        }



        private void btnUserManage_Click(object sender, EventArgs e)



        {



            OpenChildForm(



                new FormUserManagement(),



                "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG",



                btnUserManage);



        }



    }



}