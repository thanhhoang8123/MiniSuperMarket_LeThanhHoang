using System.Net;

using System.Net.Http.Headers;

using System.Net.Http.Json;

using MiniSupermarket.WinForms.DTOs;



namespace MiniSupermarket.WinForms

{

    public partial class FormCategoryManagement : Form

    {

        private const int PageSize = 5;
        private List<CategoryDto> _allCategories = new();
        private int _currentPage = 1;
        private readonly Button _btnPreviousPage = new() { Text = "◀ Trang trước", FlatStyle = FlatStyle.Flat };
        private readonly Button _btnNextPage = new() { Text = "Trang sau ▶", FlatStyle = FlatStyle.Flat };
        private readonly Label _lblPageInfo = new() { Text = "Trang 0 / 0", TextAlign = ContentAlignment.MiddleCenter };

        private const string ApiBaseUrl =

            "https://localhost:7207/api/";



        public FormCategoryManagement()

        {

            InitializeComponent();
            grpCategoryList.Controls.Add(_btnPreviousPage);
            grpCategoryList.Controls.Add(_lblPageInfo);
            grpCategoryList.Controls.Add(_btnNextPage);
            _lblPageInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblPageInfo.ForeColor = Color.FromArgb(51, 65, 85);
            _btnPreviousPage.Click += (s, e) => { if (_currentPage > 1) { ClearInputs(); _currentPage--; ShowCurrentPage(); } };
            _btnNextPage.Click += (s, e) => { if (_currentPage < TotalPages) { ClearInputs(); _currentPage++; ShowCurrentPage(); } };
            btnClear.Click += btnClear_Click;



            // Designer đã có Form Load

            // Chỉ đăng ký Resize tại đây.

            Resize += FormCategoryManagement_Resize;
            Load += (s, e) => ResizeCategoryLayout();

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
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            const int margin = 12;
            const int gap = 12;
            const int searchHeight = 55;
            const int contentTop = 72;
            const int infoWidth = 340;

            statusStrip.Visible = false;

            // Search
            grpSearch.Location = new Point(margin, margin);
            grpSearch.Size = new Size(Math.Max(300, ClientSize.Width - margin * 2), searchHeight);

            txtKeyword.Location = new Point(15, 20);
            txtKeyword.Size = new Size(Math.Max(200, (grpSearch.Width - 400) / 2), 25);

            btnSearch.Location = new Point(txtKeyword.Right + 12, 18);
            btnSearch.Size = new Size(100, 30);

            btnLoad.Location = new Point(btnSearch.Right + 10, 18);
            btnLoad.Size = new Size(100, 30);

            // Content
            int contentHeight = Math.Max(350, ClientSize.Height - contentTop - margin);
            int listWidth = Math.Max(300, ClientSize.Width - margin * 2 - gap - infoWidth);

            // Category List Panel (Takes all remaining width)
            grpCategoryList.Location = new Point(margin, contentTop);
            grpCategoryList.Size = new Size(listWidth, contentHeight);

            dgvCategories.Location = new Point(12, 28);
            int pageButtonY = grpCategoryList.ClientSize.Height - 40;
            dgvCategories.Size = new Size(
                grpCategoryList.ClientSize.Width - 24,
                Math.Max(120, pageButtonY - dgvCategories.Top - 8));
            _btnPreviousPage.SetBounds(12, pageButtonY, 108, 28);
            _lblPageInfo.SetBounds((grpCategoryList.ClientSize.Width - 170) / 2, pageButtonY, 170, 28);
            _btnNextPage.SetBounds(grpCategoryList.ClientSize.Width - 120, pageButtonY, 108, 28);
            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvCategories.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            colId.Width = 65;
            colId.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colCategoryName.Width = 230;
            colCategoryName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDescription.DataPropertyName = "Description";

            // Category Detail Panel (Fixed width, perfectly aligned)
            grpCategoryInfo.Location = new Point(grpCategoryList.Right + gap, contentTop);
            grpCategoryInfo.Size = new Size(infoWidth, contentHeight);

            lblId.Location = new Point(15, 30);
            txtId.Location = new Point(15, 52);
            txtId.Size = new Size(infoWidth - 30, 25);

            lblCategoryName.Location = new Point(15, 87);
            txtCategoryName.Location = new Point(15, 109);
            txtCategoryName.Size = new Size(infoWidth - 30, 25);

            lblDescription.Location = new Point(15, 144);
            txtDescription.Location = new Point(15, 166);
            txtDescription.Size = new Size(infoWidth - 30, 80);

            int btnY = 265;
            btnAdd.Location = new Point(15, btnY);
            btnAdd.Size = new Size(95, 36);

            btnUpdate.Location = new Point(118, btnY);
            btnUpdate.Size = new Size(95, 36);

            btnDelete.Location = new Point(221, btnY);
            btnDelete.Size = new Size(95, 36);

            int btnY2 = 310;
            btnClear.Location = new Point(15, btnY2);
            btnClear.Size = new Size(301, 35);
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

            // Sử dụng HttpClient dùng chung từ ApiClientService đã được gắn sẵn Bearer Token

            return MiniSupermarket.WinForms.Services.ApiClientService.Client;

        }



        // =========================================================

        // LOAD DANH SÁCH NHÓM HÀNG

        // =========================================================



        private async Task LoadDataAsync()

        {

            try

            {

                var client =

                    GetAuthenticatedClient();



                var categories =

                    await client.GetFromJsonAsync<

                        List<CategoryDto>>(

                            "categories");



                _allCategories = (categories ?? new List<CategoryDto>()).OrderBy(category => category.CategoryId).ToList();
                _currentPage = 1;
                ShowCurrentPage();

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

        private int TotalPages => Math.Max(1, (int)Math.Ceiling(_allCategories.Count / (double)PageSize));

        private void ShowCurrentPage()
        {
            int totalPages = TotalPages;
            _currentPage = Math.Clamp(_currentPage, 1, totalPages);
            var pageItems = _allCategories
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            dgvCategories.DataSource = null;
            dgvCategories.DataSource = pageItems;
            _lblPageInfo.Text = _allCategories.Count == 0
                ? "Trang 0 / 0"
                : $"Trang {_currentPage} / {totalPages}   •   {_allCategories.Count:N0} nhóm";
            _btnPreviousPage.Enabled = _currentPage > 1;
            _btnNextPage.Enabled = _currentPage < totalPages;
        }
        // NÚT TẢI LẠI

        // =========================================================



        private async void btnLoad_Click(

            object sender,

            EventArgs e)

        {
            ClearInputs();
            txtKeyword.Clear();
            _currentPage = 1;
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

                var client =

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

                var client =

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

                var client =

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

                var client =

                    GetAuthenticatedClient();



                string url =

                    "categories/search?keyword=" +

                    Uri.EscapeDataString(keyword);



                var result =

                    await client.GetFromJsonAsync<

                        List<CategoryDto>>(url);



                _allCategories = (result ?? new List<CategoryDto>()).OrderBy(category => category.CategoryId).ToList();
                _currentPage = 1;
                ShowCurrentPage();

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



        
        // Sự kiện nút Làm mới (xóa trắng các ô nhập liệu và tải lại danh mục)
        private async void btnClear_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            txtKeyword.Clear();
            _currentPage = 1;
            await LoadDataAsync();
        }


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



   

}

