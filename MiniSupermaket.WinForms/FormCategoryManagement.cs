using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        private const string ApiBaseUrl =
            "https://localhost:7207/api/";

        public FormCategoryManagement()
        {
            InitializeComponent();

            // Designer đã có Form Load
            // Chỉ đăng ký Resize tại đây.
            Resize += FormCategoryManagement_Resize;
        }

        // =========================================================
        // RESIZE GIAO DIỆN
        // =========================================================

        private void FormCategoryManagement_Resize(
            object? sender,
            EventArgs e)
        {
            ResizeCategoryLayout();
        }

        private void ResizeCategoryLayout()
        {
            if (ClientSize.Width <= 0 ||
                ClientSize.Height <= 0)
            {
                return;
            }

            const int margin = 10;
            const int gap = 10;
            const int searchHeight = 60;
            const int contentTop = 80;

            // Không sử dụng StatusStrip
            statusStrip.Visible = false;

            // =====================================================
            // KHU VỰC TÌM KIẾM
            // =====================================================

            grpSearch.Location = new Point(
                margin,
                margin);

            grpSearch.Size = new Size(
                Math.Max(300, ClientSize.Width - margin * 2),
                searchHeight);

            txtKeyword.Location = new Point(10, 20);
            txtKeyword.Size = new Size(300, 23);

            btnSearch.Location = new Point(320, 20);
            btnLoad.Location = new Point(400, 20);

            // =====================================================
            // KHU VỰC NỘI DUNG
            // =====================================================

            int contentHeight =
                ClientSize.Height -
                contentTop -
                margin;

            if (contentHeight < 250)
            {
                contentHeight = 250;
            }

            int availableWidth =
                ClientSize.Width -
                margin * 2 -
                gap;

            if (availableWidth < 400)
            {
                availableWidth = 400;
            }

            // 65% danh sách - 35% thông tin
            int listWidth =
                (int)(availableWidth * 0.65);

            int infoWidth =
                availableWidth - listWidth;

            // =====================================================
            // DANH SÁCH NHÓM HÀNG
            // =====================================================

            grpCategoryList.Location = new Point(
                margin,
                contentTop);

            grpCategoryList.Size = new Size(
                listWidth,
                contentHeight);

            dgvCategories.Location = new Point(
                10,
                25);

            dgvCategories.Size = new Size(
                Math.Max(
                    100,
                    grpCategoryList.ClientSize.Width - 20),

                Math.Max(
                    100,
                    grpCategoryList.ClientSize.Height - 35));

            // =====================================================
            // THÔNG TIN NHÓM HÀNG
            // =====================================================

            int infoX =
                margin +
                listWidth +
                gap;

            grpCategoryInfo.Location = new Point(
                infoX,
                contentTop);

            grpCategoryInfo.Size = new Size(
                infoWidth,
                contentHeight);

            // =====================================================
            // CÁC Ô NHẬP LIỆU
            // =====================================================

            int inputWidth =
                grpCategoryInfo.ClientSize.Width - 30;

            if (inputWidth < 150)
            {
                inputWidth = 150;
            }

            txtId.Location = new Point(
                10,
                50);

            txtId.Width = inputWidth;

            txtCategoryName.Location = new Point(
                10,
                105);

            txtCategoryName.Width = inputWidth;

            txtDescription.Location = new Point(
                10,
                177);

            txtDescription.Width = inputWidth;
            txtDescription.Height = 80;

            // =====================================================
            // CÁC NÚT CHỨC NĂNG
            // =====================================================

            int buttonY =
                grpCategoryInfo.ClientSize.Height - 50;

            btnAdd.Location = new Point(
                10,
                buttonY);

            btnUpdate.Location = new Point(
                95,
                buttonY);

            btnDelete.Location = new Point(
                180,
                buttonY);
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private async void FormCategoryManagement_Load(
            object sender,
            EventArgs e)
        {
            // Khi Form đã được Shell nhúng vào panelMainContent,
            // lúc này kích thước Form đã chính xác.
            ResizeCategoryLayout();

            await LoadDataAsync();
        }

        // =========================================================
        // TẠO HTTP CLIENT CÓ JWT
        // =========================================================

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseUrl)
            };

            if (!string.IsNullOrWhiteSpace(
                SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);
            }

            return client;
        }

        // =========================================================
        // LOAD DANH SÁCH NHÓM HÀNG
        // =========================================================

        private async Task LoadDataAsync()
        {
            try
            {
                using var client =
                    GetAuthenticatedClient();

                var categories =
                    await client.GetFromJsonAsync<
                        List<CategoryDto>>(
                            "categories");

                dgvCategories.DataSource =
                    categories ?? new List<CategoryDto>();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không thể kết nối đến Web API.\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách nhóm hàng:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NÚT TẢI LẠI
        // =========================================================

        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================================================
        // CHỌN DÒNG TRÊN DATAGRIDVIEW
        // =========================================================

        private void dgvCategories_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row =
                dgvCategories.Rows[e.RowIndex];

            txtId.Text =
                row.Cells["colId"]
                    .Value?
                    .ToString() ?? string.Empty;

            txtCategoryName.Text =
                row.Cells["colCategoryName"]
                    .Value?
                    .ToString() ?? string.Empty;

            txtDescription.Text =
                row.Cells["colDescription"]
                    .Value?
                    .ToString() ?? string.Empty;
        }

        // =========================================================
        // XỬ LÝ LỖI API
        // =========================================================

        private string GetApiErrorMessage(
            HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest =>
                    "Dữ liệu gửi lên không hợp lệ.",

                HttpStatusCode.Unauthorized =>
                    "Phiên đăng nhập không hợp lệ hoặc đã hết hạn.\n" +
                    "Vui lòng đăng nhập lại.",

                HttpStatusCode.Forbidden =>
                    "Bạn không có quyền thực hiện thao tác này.\n" +
                    "Vui lòng đăng nhập bằng tài khoản có quyền phù hợp.",

                HttpStatusCode.NotFound =>
                    "Dữ liệu không tồn tại hoặc đã bị xóa.",

                HttpStatusCode.InternalServerError =>
                    "Server đang xảy ra lỗi.\n" +
                    "Vui lòng thử lại sau.",

                _ =>
                    $"Không thể thực hiện yêu cầu.\n" +
                    $"Mã lỗi: {(int)statusCode} - {statusCode}"
            };
        }

        // =========================================================
        // THÊM NHÓM HÀNG
        // =========================================================

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên nhóm hàng!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return;
            }

            var newCategory = new
            {
                CategoryName =
                    txtCategoryName.Text.Trim(),

                Description =
                    txtDescription.Text.Trim()
            };

            try
            {
                using var client =
                    GetAuthenticatedClient();

                var response =
                    await client.PostAsJsonAsync(
                        "categories",
                        newCategory);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        GetApiErrorMessage(
                            response.StatusCode),
                        "Không thể thêm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi thêm nhóm hàng:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT NHÓM HÀNG
        // =========================================================

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                txtId.Text,
                out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên nhóm hàng!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return;
            }

            var updateCategory = new
            {
                CategoryId = id,

                CategoryName =
                    txtCategoryName.Text.Trim(),

                Description =
                    txtDescription.Text.Trim()
            };

            try
            {
                using var client =
                    GetAuthenticatedClient();

                var response =
                    await client.PutAsJsonAsync(
                        $"categories/{id}",
                        updateCategory);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        GetApiErrorMessage(
                            response.StatusCode),
                        "Không thể cập nhật",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật nhóm hàng:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // XÓA NHÓM HÀNG
        // =========================================================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn nhóm hàng cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                txtId.Text,
                out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult confirm =
                MessageBox.Show(
                    $"Bạn có chắc muốn xóa nhóm hàng ID = {id}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using var client =
                    GetAuthenticatedClient();

                var response =
                    await client.DeleteAsync(
                        $"categories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        GetApiErrorMessage(
                            response.StatusCode),
                        "Không thể xóa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xóa nhóm hàng:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // TÌM KIẾM
        // =========================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            string keyword =
                txtKeyword.Text.Trim();

            // Không có từ khóa
            // -> tải toàn bộ dữ liệu
            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                using var client =
                    GetAuthenticatedClient();

                string url =
                    "categories/search?keyword=" +
                    Uri.EscapeDataString(keyword);

                var result =
                    await client.GetFromJsonAsync<
                        List<CategoryDto>>(url);

                dgvCategories.DataSource =
                    result ?? new List<CategoryDto>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tìm kiếm nhóm hàng.\n\n" +
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // =========================================================
        // XÓA TRẮNG FORM
        // =========================================================

        private void ClearInputs()
        {
            txtId.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();

            dgvCategories.ClearSelection();

            txtCategoryName.Focus();
        }
    }

    // =============================================================
    // DTO NHẬN DỮ LIỆU TỪ WEB API
    // =============================================================

    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
            = string.Empty;

        public string? Description { get; set; }
    }
}