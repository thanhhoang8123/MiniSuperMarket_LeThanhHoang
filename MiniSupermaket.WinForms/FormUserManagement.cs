using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Headers;
using MiniSupermarket.WinForms.DTOs;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages;
        private int _totalUsers;
        private readonly Button _btnPreviousPage = new() { Text = "◀ Trang trước" };
        private readonly Button _btnNextPage = new() { Text = "Trang sau ▶" };
        private readonly Label _lblPageInfo = new() { TextAlign = ContentAlignment.MiddleCenter };

        public FormUserManagement()
        {
            InitializeComponent();

            _btnPreviousPage.FlatStyle = FlatStyle.Flat;
            _btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            _btnPreviousPage.ForeColor = System.Drawing.Color.White;
            _btnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            _btnPreviousPage.Cursor = Cursors.Hand;
            _btnNextPage.FlatStyle = FlatStyle.Flat;
            _btnNextPage.BackColor = System.Drawing.Color.FromArgb(71, 85, 105);
            _btnNextPage.ForeColor = System.Drawing.Color.White;
            _btnNextPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            _btnNextPage.Cursor = Cursors.Hand;
            _lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            _lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            panelGrid.Controls.Add(_btnPreviousPage);
            panelGrid.Controls.Add(_lblPageInfo);
            panelGrid.Controls.Add(_btnNextPage);
            _btnPreviousPage.Click += async (s, e) =>
            {
                if (_currentPage > 1) { ClearInputs(); _currentPage--; await LoadUsersAsync(); }
            };
            _btnNextPage.Click += async (s, e) =>
            {
                if (_currentPage < _totalPages) { ClearInputs(); _currentPage++; await LoadUsersAsync(); }
            };

            Load += FormUserManagement_Load;
            ConfigureUsersGrid();
            dgvUsers.CellClick += dgvUsers_CellClick;
            btnAddUser.Click += btnAddUser_Click;
            btnResetPassword.Click += btnResetPassword_Click;
            btnToggleLock.Click += btnToggleLock_Click;
            btnClear.Click += btnClear_Click;

            Resize += (s, e) => ResizeUserLayout();
            Load += (s, e) => ResizeUserLayout();
        }

        private void ConfigureUsersGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UserId",
                DataPropertyName = "UserId",
                HeaderText = "Mã ID",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Tên đăng nhập",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold) }
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Họ và tên",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Role",
                DataPropertyName = "Role",
                HeaderText = "Vai trò",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Trạng thái",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvUsers.EnableHeadersVisualStyles = false;
            dgvUsers.ColumnHeadersHeight = 38;
            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            dgvUsers.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgvUsers.RowTemplate.Height = 35;
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvUsers.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dgvUsers.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            dgvUsers.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgvUsers.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            dgvUsers.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvUsers.Columns[e.ColumnIndex].DataPropertyName == "IsActive" && e.Value != null)
                {
                    bool active = Convert.ToBoolean(e.Value);
                    e.Value = active ? "🟢 Hoạt động" : "🔒 Đã khóa";
                    e.FormattingApplied = true;
                    if (e.CellStyle != null)
                    {
                        if (!active)
                        {
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
                            e.CellStyle.Font = new System.Drawing.Font(dgvUsers.Font, System.Drawing.FontStyle.Bold);
                        }
                        else
                        {
                            e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
                        }
                    }
                }
            };
        }

        private void ResizeUserLayout()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            panelTop.Visible = false;

            panelGrid.Anchor = AnchorStyles.None;
            panelDetail.Anchor = AnchorStyles.None;

            int margin = 15;
            int gap = 15;
            int detailWidth = 330;

            int availableW = ClientSize.Width - margin * 2 - gap;
            int gridW = Math.Max(350, availableW - detailWidth);
            int contentH = Math.Max(350, ClientSize.Height - margin * 2);

            panelGrid.Location = new System.Drawing.Point(margin, margin);
            panelGrid.Size = new System.Drawing.Size(gridW, contentH);

            dgvUsers.Location = new System.Drawing.Point(15, 45);
            int pagerY = panelGrid.ClientSize.Height - 42;
            dgvUsers.Size = new System.Drawing.Size(panelGrid.Width - 30, Math.Max(120, pagerY - dgvUsers.Top - 8));
            _btnPreviousPage.SetBounds(15, pagerY, 112, 28);
            _lblPageInfo.SetBounds((panelGrid.ClientSize.Width - 250) / 2, pagerY, 250, 28);
            _btnNextPage.SetBounds(panelGrid.ClientSize.Width - 127, pagerY, 112, 28);

            panelDetail.Location = new System.Drawing.Point(panelGrid.Right + gap, margin);
            panelDetail.Size = new System.Drawing.Size(detailWidth, contentH);

            // Compact detail controls so all action buttons fit on any screen!
            lblDetail.Location = new System.Drawing.Point(15, 12);

            lblUsername.Location = new System.Drawing.Point(15, 42);
            txtUsername.Location = new System.Drawing.Point(15, 64);
            txtUsername.Size = new System.Drawing.Size(295, 25);

            lblFullName.Location = new System.Drawing.Point(15, 95);
            txtFullName.Location = new System.Drawing.Point(15, 117);
            txtFullName.Size = new System.Drawing.Size(295, 25);

            lblPassword.Location = new System.Drawing.Point(15, 148);
            txtPassword.Location = new System.Drawing.Point(15, 170);
            txtPassword.Size = new System.Drawing.Size(295, 25);

            lblRole.Location = new System.Drawing.Point(15, 201);
            cboRole.Location = new System.Drawing.Point(15, 223);
            cboRole.Size = new System.Drawing.Size(295, 26);

            btnAddUser.Location = new System.Drawing.Point(15, 265);
            btnAddUser.Size = new System.Drawing.Size(295, 36);

            btnResetPassword.Location = new System.Drawing.Point(15, 309);
            btnResetPassword.Size = new System.Drawing.Size(295, 36);

            btnToggleLock.Location = new System.Drawing.Point(15, 353);
            btnToggleLock.Size = new System.Drawing.Size(142, 36);

            btnClear.Location = new System.Drawing.Point(168, 353);
            btnClear.Size = new System.Drawing.Size(142, 36);
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
                    MessageBox.Show(
                        MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyResponseMessage(response.StatusCode, "thay đổi trạng thái tài khoản"),
                        "Chưa thể cập nhật tài khoản",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
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
                    MessageBox.Show(
                        MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyResponseMessage(response.StatusCode, "đặt lại mật khẩu"),
                        "Chưa thể đặt lại mật khẩu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
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

                var result =
                    await ApiClientService.Client
                        .GetFromJsonAsync<PagedResultDto<UserDto>>(
                            $"users/paged?pageNumber={_currentPage}&pageSize={PageSize}");

                _currentPage = result?.CurrentPage ?? 1;
                _totalPages = result?.TotalPages ?? 0;
                _totalUsers = result?.TotalItems ?? 0;
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = result?.Items ?? new List<UserDto>();
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản: " + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdatePaginationControls()
        {
            _lblPageInfo.Text = _totalUsers == 0
                ? "Trang 0 / 0"
                : $"Trang {_currentPage} / {_totalPages}   •   {_totalUsers:N0} tài khoản";
            _btnPreviousPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < _totalPages;
        }

        private async void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            _currentPage = 1;
            await LoadUsersAsync();
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

                    _currentPage = 1;
                    await LoadUsersAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyResponseMessage(res.StatusCode, "tạo tài khoản"),
                        "Chưa thể tạo tài khoản",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server: " + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
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
