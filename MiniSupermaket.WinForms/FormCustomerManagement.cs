using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        private int _currentPage = 1;
        private const int _pageSize = 5;
        private int _totalPages = 0;
        private string _keyword = string.Empty;

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7207/api/")
            };

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);
            }

            return client;
        }

        public FormCustomerManagement()
        {
            InitializeComponent();

            Load += FormCustomerManagement_Load;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            btnLoad.Click += btnLoad_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnSearch.Click += btnSearch_Click;
            btnPrevious.Click += btnPrevious_Click;
            btnNext.Click += btnNext_Click;
        }

        // Tự động tải danh sách khi mở Form
        private async void FormCustomerManagement_Load(
            object? sender,
            EventArgs e)
        {
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        // GET /api/customers/paged
        private async Task LoadCustomersAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();

                string url =
                    $"customers/paged?pageNumber={_currentPage}" +
                    $"&pageSize={_pageSize}" +
                    $"&keyword={Uri.EscapeDataString(_keyword)}";

                var result =
                    await client.GetFromJsonAsync<PagedCustomerResponse>(url);

                if (result == null)
                {
                    dgvCustomers.DataSource = null;
                    _totalPages = 0;
                    UpdatePaginationControls();
                    return;
                }

                _currentPage = result.CurrentPage;
                _totalPages = result.TotalPages;
                dgvCustomers.DataSource = result.Items;
                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách khách hàng: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdatePaginationControls()
        {
            lblPageInfo.Text = _totalPages == 0
                ? "Trang 0 / 0"
                : $"Trang {_currentPage} / {_totalPages}";

            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < _totalPages;
        }

        private async void btnPrevious_Click(object? sender, EventArgs e)
        {
            if (_currentPage <= 1)
                return;

            _currentPage--;
            await LoadCustomersAsync();
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            if (_currentPage >= _totalPages)
                return;

            _currentPage++;
            await LoadCustomersAsync();
        }

        // Nút Tải danh sách
        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        // Click một dòng -> đưa dữ liệu lên TextBox
        private void dgvCustomers_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

            txtCustomerId.Text =
                row.Cells["CustomerId"].Value?.ToString() ?? "";

            txtCustomerName.Text =
                row.Cells["CustomerName"].Value?.ToString() ?? "";

            txtPhoneNumber.Text =
                row.Cells["PhoneNumber"].Value?.ToString() ?? "";

            txtAddress.Text =
                row.Cells["Address"].Value?.ToString() ?? "";

            txtRewardPoints.Text =
                row.Cells["RewardPoints"].Value?.ToString() ?? "";

            txtMembershipRank.Text =
                row.Cells["MembershipRank"].Value?.ToString() ?? "";
        }

        // POST /api/customers
        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên và số điện thoại!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    txtRewardPoints.Text,
                    out int rewardPoints))
            {
                MessageBox.Show(
                    "Điểm tích lũy phải là số!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var newCustomer = new
            {
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = rewardPoints,
                MembershipRank = txtMembershipRank.Text.Trim()
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response = await client.PostAsJsonAsync(
                    "customers",
                    newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomersAsync();
                    ClearInputs();
                }
                else if (response.StatusCode ==
                         System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show(
                        "Bạn không có quyền thêm khách hàng!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "Thêm khách hàng thất bại!",
                        "Lỗi",
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

        // PUT /api/customers/{id}
        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(
                    txtCustomerId.Text,
                    out int id))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    txtRewardPoints.Text,
                    out int rewardPoints))
            {
                MessageBox.Show(
                    "Điểm tích lũy phải là số!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var updateCustomer = new
            {
                CustomerId = id,
                CustomerName = txtCustomerName.Text.Trim(),
                PhoneNumber = txtPhoneNumber.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                RewardPoints = rewardPoints,
                MembershipRank = txtMembershipRank.Text.Trim()
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response = await client.PutAsJsonAsync(
                    $"customers/{id}",
                    updateCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomersAsync();
                    ClearInputs();
                }
                else if (response.StatusCode ==
                         System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show(
                        "Bạn không có quyền cập nhật khách hàng!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật thất bại!",
                        "Lỗi",
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

        // DELETE /api/customers/{id}
        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(
                    txtCustomerId.Text,
                    out int id))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa khách hàng ID = {id}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.DeleteAsync($"customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa khách hàng thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadCustomersAsync();
                    ClearInputs();
                }
                else if (response.StatusCode ==
                         System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show(
                        "Bạn không có quyền xóa khách hàng!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "Xóa khách hàng thất bại!",
                        "Lỗi",
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

        // Tìm kiếm kết hợp phân trang qua API /customers/paged
        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            _keyword = txtKeyword.Text.Trim();
            _currentPage = 1;
            await LoadCustomersAsync();
        }

        // Xóa dữ liệu trên các TextBox
        private void ClearInputs()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Text = "0";
            txtMembershipRank.Text = "Chuẩn";
        }

        // Empty click handlers generated in Designer. Keep them to satisfy event assignments.
        private void label1_Click(object? sender, EventArgs e)
        {
            // No action required when labels are clicked.
        }

        private void label1_Click_1(object? sender, EventArgs e)
        {
            // No action required when labels are clicked.
        }
    }

    // DTO nhận kết quả phân trang từ API
    public class PagedCustomerResponse
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<CustomerDto> Items { get; set; } = new();
    }

    // DTO nhận dữ liệu JSON từ API
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int RewardPoints { get; set; }

        public string MembershipRank { get; set; } = string.Empty;
    }
}