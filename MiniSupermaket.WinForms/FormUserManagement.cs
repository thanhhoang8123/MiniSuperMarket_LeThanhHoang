using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        public FormUserManagement()
        {
            InitializeComponent();

            Load += FormUserManagement_Load;
            dgvUsers.CellClick += dgvUsers_CellClick;
            btnAddUser.Click += btnAddUser_Click;
            btnResetPassword.Click += btnResetPassword_Click;
            btnToggleLock.Click += btnToggleLock_Click;
        }

        private async void btnToggleLock_Click(
    object? sender,
    EventArgs e)
        {
            if (this.Tag == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần khóa hoặc mở khóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int userId = Convert.ToInt32(this.Tag);

            try
            {
                ApiClientService.Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);

                var confirm = MessageBox.Show(
                    "Bạn có chắc muốn thay đổi trạng thái tài khoản này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                    return;

                var response =
                    await ApiClientService.Client.PutAsync(
                        $"users/{userId}/toggle-lock",
                        null);

                if (response.IsSuccessStatusCode)
                {
                    var result =
                        await response.Content.ReadFromJsonAsync<ToggleLockResponse>();

                    MessageBox.Show(
                        result?.Message ?? "Đã thay đổi trạng thái tài khoản!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();
                    ClearInputs();
                }
                else
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Không thể thay đổi trạng thái!\n\n" +
                        $"HTTP: {(int)response.StatusCode}\n\n" +
                        message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private async void btnResetPassword_Click(
    object? sender,
    EventArgs e)
        {
            if (this.Tag == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn tài khoản cần đặt lại mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu mới!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            int userId = Convert.ToInt32(this.Tag);

            var request = new
            {
                NewPassword = txtPassword.Text.Trim()
            };

            try
            {
                ApiClientService.Client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);

                var response =
                    await ApiClientService.Client.PutAsJsonAsync(
                        $"users/{userId}/reset-password",
                        request);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtPassword.Clear();
                }
                else
                {
                    var message =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Đặt lại mật khẩu thất bại!\n\n" +
                        $"HTTP: {(int)response.StatusCode}\n\n" +
                        message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private async void FormUserManagement_Load(
            object? sender,
            EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                // Đảm bảo request có JWT của tài khoản đang đăng nhập
                ApiClientService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);

                var users =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<UserDto>>("users");

                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvUsers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvUsers.Rows[e.RowIndex];

            txtUsername.Text =
                row.Cells["Username"].Value?.ToString() ?? "";

            txtFullName.Text =
                row.Cells["FullName"].Value?.ToString() ?? "";

            cboRole.Text =
                row.Cells["Role"].Value?.ToString() ?? "";

            txtPassword.Clear();

            this.Tag =
                row.Cells["UserId"].Value;
        }

        private async void btnAddUser_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Tên đăng nhập và mật khẩu không được trống!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var newUser = new
            {
                Username = txtUsername.Text.Trim(),

                // Model User hiện tại của bạn dùng PasswordHash
                PasswordHash = txtPassword.Text.Trim(),

                FullName = txtFullName.Text.Trim(),

                Role = cboRole.SelectedItem?.ToString()
                       ?? "Cashier",

                IsActive = true
            };

            try
            {
                var res =
                    await ApiClientService.Client
                        .PostAsJsonAsync("users", newUser);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Tạo tài khoản mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadUsersAsync();

                    ClearInputs();
                }
                else
                {
                    var message =
                        await res.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        $"Không thể tạo tài khoản!\n\n" +
                        $"HTTP: {(int)res.StatusCode} - {res.StatusCode}\n\n" +
                        message,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtPassword.Clear();

            cboRole.SelectedIndex = 1;

            this.Tag = null;

            dgvUsers.ClearSelection();
        }
    }

    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
    public class ToggleLockResponse
    {
        public string Message { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}