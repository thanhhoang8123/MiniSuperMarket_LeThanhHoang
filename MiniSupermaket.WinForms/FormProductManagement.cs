

using System;

using System.Collections.Generic;

using System.Net.Http;

using System.Net.Http.Json;

using System.Threading.Tasks;

using System.Windows.Forms;

using MiniSupermarket.WinForms.Services;

using MiniSupermarket.WinForms.DTOs;

namespace MiniSupermarket.WinForms

{

    public partial class FormProductManagement : Form

    {

        private int _currentPage = 1;

        private const int _pageSize = 5;

        private int _totalPages = 0;

        private string _keyword = string.Empty;

        private int? _filterCategoryId = null;

        private bool _suppressFilterEvent = true;



        public FormProductManagement()

        {

            InitializeComponent();

            ConfigureProductGrid();
            if (string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase))
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnExportExcel.Visible = false;
                txtBarcode.ReadOnly = true;
                txtProductName.ReadOnly = true;
                cboCategory.Enabled = false;
                nudPrice.Enabled = false;
                nudStock.Enabled = false;
            }
            this.Resize += (s, e) => ResizeProductLayout();
            this.Load += (s, e) => ResizeProductLayout();
        }

                private void ResizeProductLayout()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            const int margin = 12;
            const int searchHeight = 55;
            const int contentTop = 72;
            const int detailWidth = 350;

            // 1. Search Panel
            panelSearch.Location = new Point(margin, margin);
            panelSearch.Size = new Size(Math.Max(500, ClientSize.Width - margin * 2), searchHeight);

            lblSearchTitle.Location = new Point(12, 16);
            btnExportExcel.Location = new Point(panelSearch.Width - btnExportExcel.Width - 12, 10);
            btnSearch.Location = new Point(btnExportExcel.Left - btnSearch.Width - 10, 10);
            btnSearch.Size = new Size(100, 32);

            cboFilterCategory.Size = new Size(180, 27);
            cboFilterCategory.Location = new Point(btnSearch.Left - cboFilterCategory.Width - 10, 12);

            txtKeyword.Location = new Point(lblSearchTitle.Right + 12, 12);
            int kwWidth = Math.Max(200, cboFilterCategory.Left - txtKeyword.Left - 10);
            txtKeyword.Size = new Size(kwWidth, 27);
            txtKeyword.PlaceholderText = "Nhập mã vạch hoặc tên sản phẩm...";

            // 2. Right Detail Panel
            int contentHeight = Math.Max(380, ClientSize.Height - contentTop - margin);
            panelDetail.Location = new Point(ClientSize.Width - detailWidth - margin, contentTop);
            panelDetail.Size = new Size(detailWidth, contentHeight);

            // Detail Inputs (Compact and perfectly spaced within 350px)
            lblDetailTitle.Location = new Point(15, 10);

            // Row 1: ID & Barcode
            lblId.Location = new Point(15, 36);
            txtId.Location = new Point(15, 56);
            txtId.Size = new Size(95, 25);

            lblBarcode.Location = new Point(120, 36);
            txtBarcode.Location = new Point(120, 56);
            txtBarcode.Size = new Size(210, 25);

            // Row 2: Product Name
            lblProductName.Location = new Point(15, 87);
            txtProductName.Location = new Point(15, 107);
            txtProductName.Size = new Size(315, 25);

            // Row 3: Category
            lblCategory.Location = new Point(15, 138);
            cboCategory.Location = new Point(15, 158);
            cboCategory.Size = new Size(315, 25);

            // Row 4: Price & Stock
            lblPrice.Location = new Point(15, 189);
            nudPrice.Location = new Point(15, 209);
            nudPrice.Size = new Size(150, 25);

            lblStock.Location = new Point(180, 189);
            nudStock.Location = new Point(180, 209);
            nudStock.Size = new Size(150, 25);

            // Row 5: Action buttons (ALWAYS VISIBLE!)
            int btnY1 = 252;
            btnAdd.Location = new Point(15, btnY1);
            btnAdd.Size = new Size(98, 38);

            btnUpdate.Location = new Point(123, btnY1);
            btnUpdate.Size = new Size(98, 38);

            btnDelete.Location = new Point(231, btnY1);
            btnDelete.Size = new Size(98, 38);

            // Row 6: Clear button
            int btnY2 = 298;
            btnClear.Location = new Point(15, btnY2);
            btnClear.Size = new Size(314, 34);

            // 3. Left Grid Panel
            int gridWidth = panelDetail.Left - margin * 2;
            panelGrid.Location = new Point(margin, contentTop);
            panelGrid.Size = new Size(Math.Max(300, gridWidth), contentHeight);

            lblProductListTitle.Location = new Point(15, 10);
            dgvProducts.Location = new Point(15, 38);
            dgvProducts.Size = new Size(panelGrid.Width - 30, panelGrid.Height - 38 - 50);

            int navY = panelGrid.Height - 42;
            btnPrevious.Location = new Point(15, navY);
            lblPageInfo.Location = new Point(btnPrevious.Right + 10, navY + 6);
            btnNext.Location = new Point(panelGrid.Width - btnNext.Width - 15, navY);
        }



    // CẤU HÌNH CỘT HIỂN THỊ VÀ ĐỊNH DẠNG TRÊN LƯỚI DATAGRIDVIEW



    private void ConfigureProductGrid()

    {

        dgvProducts.AutoGenerateColumns = false;

        dgvProducts.Columns.Clear();

        dgvProducts.RowHeadersVisible = false;
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.EnableHeadersVisualStyles = false;
        dgvProducts.ColumnHeadersHeight = 38;
        dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
        dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
        dgvProducts.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
        dgvProducts.RowTemplate.Height = 35;
        dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
        dgvProducts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 58, 138);
        dgvProducts.GridColor = Color.FromArgb(226, 232, 240);
        dgvProducts.Font = new Font("Segoe UI", 9.5F);

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "ProductId",
            Name = "ProductId",
            HeaderText = "Mã SP",
            MinimumWidth = 70,
            FillWeight = 10,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Barcode",
            Name = "Barcode",
            HeaderText = "Mã vạch",
            MinimumWidth = 125,
            FillWeight = 17,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
        });

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "ProductName",
            Name = "ProductName",
            HeaderText = "Tên sản phẩm",
            MinimumWidth = 200,
            FillWeight = 38,
        });

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "CategoryName",
            Name = "CategoryName",
            HeaderText = "Nhóm hàng",
            MinimumWidth = 130,
            FillWeight = 18,
        });

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Price",
            Name = "Price",
            HeaderText = "Đơn giá",
            MinimumWidth = 100,
            FillWeight = 13,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" }
        });

        dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "StockQuantity",
            Name = "StockQuantity",
            HeaderText = "Tồn kho",
            MinimumWidth = 90,
            FillWeight = 12,

    DefaultCellStyle = new DataGridViewCellStyle

    {

        Alignment = DataGridViewContentAlignment.MiddleRight,

        Format = "N0"

    }

});

dgvProducts.Columns.Add(new DataGridViewTextBoxColumn

{

    DataPropertyName = "CategoryId",

    Name = "CategoryId",

    Visible = false

});


    // Bắt sự kiện CellFormatting tự động tô màu cảnh báo hàng sắp hết kho (tồn kho <= 10)
    dgvProducts.CellFormatting += DgvProducts_CellFormatting;
}

// =========================================================
// CẢNH BÁO HÀNG SẮP HẾT KHO TRÊN DATAGRIDVIEW (CellFormatting)
// =========================================================
private void DgvProducts_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
{
    if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

    // Kiểm tra nếu đang định dạng ô thuộc cột Tồn kho (StockQuantity)
    if (dgvProducts.Columns[e.ColumnIndex]?.Name == "StockQuantity" && e.Value != null && e.CellStyle != null)
    {
        if (int.TryParse(e.Value.ToString(), out int stockQuantity) && stockQuantity <= 10)
        {
            // Tự động đổi màu nền đỏ cam cảnh báo và chữ trắng đậm nổi bật
            e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
            e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
            e.CellStyle.Font = new Font(dgvProducts.Font ?? SystemFonts.DefaultFont, FontStyle.Bold);
            e.CellStyle.SelectionBackColor = Color.FromArgb(239, 68, 68);
            e.CellStyle.SelectionForeColor = Color.White;
        }
    }
}



// 1. LOAD FORM



private async void FormProductManagement_Load(

object? sender, EventArgs e)

{

    lblProductListTitle.Text = "DANH SÁCH SẢN PHẨM";

    ResizeProductLayout();
    await LoadCategoriesAsync();

    _suppressFilterEvent = false;

    cboFilterCategory.SelectedIndexChanged -=

    cboFilterCategory_SelectedIndexChanged;

    cboFilterCategory.SelectedIndexChanged +=

    cboFilterCategory_SelectedIndexChanged;

    await LoadProductsAsync();

}



// 2. LOAD DANH MỤC



private async Task LoadCategoriesAsync()

{

    try

    {

        var client = ApiClientService.Client;

        var categories =

        await client.GetFromJsonAsync<List<CategoryDto>>(

        "categories");

        if (categories == null)

        return;



        // ComboBox chọn danh mục khi thêm/sửa sản phẩm

        cboCategory.DataSource = null;

        cboCategory.DisplayMember = "CategoryName";

        cboCategory.ValueMember = "CategoryId";

        cboCategory.DataSource = categories;



        // ComboBox lọc danh mục

        var filterCategories = new List<CategoryDto>

        {

            new CategoryDto

            {

                CategoryId = 0,

                CategoryName = "Tất cả danh mục"

            }

    };

filterCategories.AddRange(categories);

cboFilterCategory.DataSource = null;

cboFilterCategory.DisplayMember = "CategoryName";

cboFilterCategory.ValueMember = "CategoryId";

cboFilterCategory.DataSource = filterCategories;

cboFilterCategory.SelectedValue = 0;

}

catch (Exception ex)

{

    MessageBox.Show(

    "Không thể tải danh mục:\n" + ex.Message,

    "Lỗi",

    MessageBoxButtons.OK,

    MessageBoxIcon.Error);

}

}



// 3. LOAD SẢN PHẨM: TÌM KIẾM + LỌC + PHÂN TRANG



private async Task LoadProductsAsync()

{

    try

    {

        var client = ApiClientService.Client;

        string url =

        $"products/paged?pageNumber={_currentPage}" +

        $"&pageSize={_pageSize}" +

        $"&keyword={Uri.EscapeDataString(_keyword)}";

        if (_filterCategoryId.HasValue &&

        _filterCategoryId.Value > 0)

        {

            url += $"&categoryId={_filterCategoryId.Value}";

        }

    var result =

    await client.GetFromJsonAsync<

    PagedResultDto<ProductDto>>(url);

    if (result == null)

    {

        dgvProducts.DataSource = null;

        _totalPages = 0;

        _currentPage = 1;

        UpdatePaginationControls();

        return;

    }

if (result.TotalPages == 0)

{

    _currentPage = 1;

    _totalPages = 0;

    dgvProducts.DataSource = result.Items;

    UpdatePaginationControls();

    return;

}



// Nếu trang hiện tại không còn hợp lệ

if (_currentPage > result.TotalPages)

{

    _currentPage = result.TotalPages;

    await LoadProductsAsync();

    return;

}

_totalPages = result.TotalPages;

dgvProducts.DataSource = result.Items;

UpdatePaginationControls();

}

catch (Exception ex)

{

    MessageBox.Show(

    "Không thể tải sản phẩm:\n" + ex.Message,

    "Lỗi",

    MessageBoxButtons.OK,

    MessageBoxIcon.Error);

}

}



// 4. CẬP NHẬT PHÂN TRANG



private void UpdatePaginationControls()

{

    lblPageInfo.Text = _totalPages == 0

    ? "Trang 0 / 0"

    : $"Trang {_currentPage} / {_totalPages}";

    btnPrevious.Enabled = _currentPage > 1;

    btnNext.Enabled = _currentPage < _totalPages;

}



// 5. LỌC THEO DANH MỤC



private async void cboFilterCategory_SelectedIndexChanged(

object? sender, EventArgs e)

{

    if (_suppressFilterEvent)

    return;

    if (cboFilterCategory.SelectedValue == null)

    return;

    if (!int.TryParse(

    cboFilterCategory.SelectedValue.ToString(),

    out int categoryId))

    {

        return;

    }

_filterCategoryId = categoryId == 0

? null

: categoryId;

_currentPage = 1;

await LoadProductsAsync();

}



// 6. TRANG TRƯỚC



private async void btnPrevious_Click(

object? sender, EventArgs e)

{

    if (_currentPage <= 1)

    return;

    _currentPage--;

    await LoadProductsAsync();

}



// 7. TRANG TIẾP



private async void btnNext_Click(

object? sender, EventArgs e)

{

    if (_currentPage >= _totalPages)

    return;

    _currentPage++;

    await LoadProductsAsync();

}



// 8. CHỌN SẢN PHẨM



private void dgvProducts_CellClick(

object? sender, DataGridViewCellEventArgs e)

{

    if (e.RowIndex < 0)

    return;

    var row = dgvProducts.Rows[e.RowIndex];

    txtId.Text =

    row.Cells["ProductId"].Value?.ToString() ?? "";

    txtBarcode.Text =

    row.Cells["Barcode"].Value?.ToString() ?? "";

    txtProductName.Text =

    row.Cells["ProductName"].Value?.ToString() ?? "";

    if (row.Cells["Price"].Value != null)

    {

        decimal price =

        Convert.ToDecimal(row.Cells["Price"].Value);

        if (price >= nudPrice.Minimum &&

        price <= nudPrice.Maximum)

        {

            nudPrice.Value = price;

        }

}

if (row.Cells["StockQuantity"].Value != null)

{

    decimal stock =

    Convert.ToDecimal(row.Cells["StockQuantity"].Value);

    if (stock >= nudStock.Minimum &&

    stock <= nudStock.Maximum)

    {

        nudStock.Value = stock;

    }

}

if (row.Cells["CategoryId"].Value != null)

{

    cboCategory.SelectedValue =

    Convert.ToInt32(row.Cells["CategoryId"].Value);

}

}



// 9. THÊM SẢN PHẨM



private async void btnAdd_Click(

object? sender, EventArgs e)

{

    if (!ValidateProductInput())

    return;

    var newProduct = new

    {

        Barcode = txtBarcode.Text.Trim(),

        ProductName = txtProductName.Text.Trim(),

        Price = nudPrice.Value,

        StockQuantity = (int)nudStock.Value,

        CategoryId = Convert.ToInt32(cboCategory.SelectedValue)

    };

try

{

    var client = ApiClientService.Client;

    var response = await client.PostAsJsonAsync(

    "products", newProduct);

    if (response.IsSuccessStatusCode)

    {

        MessageBox.Show(

        "Thêm sản phẩm thành công!",

        "Thông báo",

        MessageBoxButtons.OK,

        MessageBoxIcon.Information);

        _currentPage = 1;

        await LoadProductsAsync();

        ClearInputs();

    }

else

{

    string error =

    await response.Content.ReadAsStringAsync();

    MessageBox.Show(

    $"Không thể thêm sản phẩm.\n" +

    $"Mã lỗi: {(int)response.StatusCode} - " +

    $"{response.StatusCode}\n{error}",

    "Lỗi",

    MessageBoxButtons.OK,

    MessageBoxIcon.Warning);

}

}

catch (Exception ex)

{

    MessageBox.Show(

    "Lỗi khi thêm sản phẩm:\n" + ex.Message);

}

}



// 10. CẬP NHẬT SẢN PHẨM



private async void btnUpdate_Click(

object? sender, EventArgs e)

{

    if (!int.TryParse(txtId.Text, out int id))

    {

        MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");

        return;

    }

if (!ValidateProductInput())

return;

var updateProduct = new

{

    ProductId = id,

    Barcode = txtBarcode.Text.Trim(),

    ProductName = txtProductName.Text.Trim(),

    Price = nudPrice.Value,

    StockQuantity = (int)nudStock.Value,

    CategoryId = Convert.ToInt32(cboCategory.SelectedValue)

};

try

{

    var client = ApiClientService.Client;

    var response = await client.PutAsJsonAsync(

    $"products/{id}", updateProduct);

    if (response.IsSuccessStatusCode)

    {

        MessageBox.Show("Cập nhật sản phẩm thành công!");

        await LoadProductsAsync();

        ClearInputs();

    }

else

{

    string error =

    await response.Content.ReadAsStringAsync();

    MessageBox.Show(

    $"Không thể cập nhật sản phẩm.\n" +

    $"Mã lỗi: {(int)response.StatusCode} - " +

    $"{response.StatusCode}\n{error}",

    "Lỗi",

    MessageBoxButtons.OK,

    MessageBoxIcon.Warning);

}

}

catch (Exception ex)

{

    MessageBox.Show(

    "Lỗi khi cập nhật sản phẩm:\n" + ex.Message);

}

}



// 11. XÓA SẢN PHẨM



private async void btnDelete_Click(

object? sender, EventArgs e)

{

    if (!int.TryParse(txtId.Text, out int id))

    {

        MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!");

        return;

    }

var confirm = MessageBox.Show(

"Bạn có chắc muốn xóa sản phẩm này?",

"Xác nhận xóa",

MessageBoxButtons.YesNo,

MessageBoxIcon.Question);

if (confirm != DialogResult.Yes)

return;

try

{

    var client = ApiClientService.Client;

    var response =

    await client.DeleteAsync($"products/{id}");

    if (response.IsSuccessStatusCode)

    {

        MessageBox.Show("Xóa sản phẩm thành công!");

        await LoadProductsAsync();

        ClearInputs();

    }

else

{

    string error =

    await response.Content.ReadAsStringAsync();

    MessageBox.Show(

    $"Không thể xóa sản phẩm.\n" +

    $"Mã lỗi: {(int)response.StatusCode} - " +

    $"{response.StatusCode}\n{error}",

    "Lỗi",

    MessageBoxButtons.OK,

    MessageBoxIcon.Warning);

}

}

catch (Exception ex)

{

    MessageBox.Show(

    "Lỗi khi xóa sản phẩm:\n" + ex.Message);

}

}



// 12. TÌM KIẾM



private async void btnSearch_Click(

object? sender, EventArgs e)

{

    _keyword = txtKeyword.Text.Trim();

    _currentPage = 1;

    await LoadProductsAsync();

}



// 13. LÀM MỚI



private async void btnClear_Click(

object? sender, EventArgs e)

{

    ClearInputs();

    _keyword = string.Empty;

    _currentPage = 1;

    _filterCategoryId = null;



    // Đặt lại ComboBox lọc mà không gọi API hai lần

    _suppressFilterEvent = true;

    cboFilterCategory.SelectedValue = 0;

    _suppressFilterEvent = false;

    txtKeyword.Clear();

    await LoadProductsAsync();

}



// 14. XÓA TRẮNG Ô NHẬP LIỆU



private void ClearInputs()

{

    txtId.Clear();

    txtBarcode.Clear();

    txtProductName.Clear();

    nudPrice.Value = 0;

    nudStock.Value = 0;

    if (cboCategory.Items.Count > 0)

    cboCategory.SelectedIndex = 0;

    dgvProducts.ClearSelection();

}



// 15. KIỂM TRA DỮ LIỆU



private bool ValidateProductInput()

{

    if (string.IsNullOrWhiteSpace(txtBarcode.Text))

    {

        MessageBox.Show("Vui lòng nhập mã Barcode!");

        txtBarcode.Focus();

        return false;

    }

if (string.IsNullOrWhiteSpace(txtProductName.Text))

{

    MessageBox.Show("Vui lòng nhập tên sản phẩm!");

    txtProductName.Focus();

    return false;

}

if (cboCategory.SelectedValue == null)

{

    MessageBox.Show("Vui lòng chọn danh mục sản phẩm!");

    cboCategory.Focus();

    return false;

}

return true;

}


        // =========================================================
        // XUẤT DANH SÁCH MẶT HÀNG & TỒN KHO RA TỆP EXCEL / CSV
        // =========================================================
        private async void btnExportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                btnExportExcel.Enabled = false;

                // Tải danh sách toàn bộ sản phẩm từ máy chủ API
                var allProducts = await ApiClientService.GetAsync<List<DTOs.ProductDto>>("products");

                if (allProducts == null || allProducts.Count == 0)
                {
                    MessageBox.Show(
                        "Không có dữ liệu mặt hàng nào để xuất file!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                using var saveDialog = new SaveFileDialog
                {
                    Filter = "Sổ làm việc Excel (*.xlsx)|*.xlsx|Tệp CSV (*.csv)|*.csv",
                    FileName = $"DanhSachSanPham_TonKho_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx",
                    AddExtension = true,
                    Title = "Xuất danh sách sản phẩm và tồn kho"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var headers = new[] { "Mã sản phẩm", "Mã vạch", "Tên sản phẩm", "Nhóm hàng", "Đơn giá (VNĐ)", "Số lượng tồn kho", "Tình trạng kho" };
                    var rows = allProducts.Select(item => (IReadOnlyList<object?>)new object?[]
                    {
                        item.ProductId,
                        item.Barcode,
                        item.ProductName,
                        item.CategoryName,
                        item.Price,
                        item.StockQuantity,
                        item.StockQuantity <= 10 ? "CẢNH BÁO SẮP HẾT HÀNG" : "Đang kinh doanh"
                    }).ToList();

                    if (System.IO.Path.GetExtension(saveDialog.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        SpreadsheetExportService.WriteXlsx(saveDialog.FileName, "Danh mục sản phẩm", headers, rows);
                    }
                    else
                    {
                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine(string.Join(",", headers));
                        foreach (var row in rows)
                            sb.AppendLine(string.Join(",", row.Select(value => EscapeCsv(Convert.ToString(value, System.Globalization.CultureInfo.CurrentCulture)))));
                        await System.IO.File.WriteAllTextAsync(saveDialog.FileName, sb.ToString(), new System.Text.UTF8Encoding(true));
                    }

                    var choice = MessageBox.Show(
                        $"Xuất file Excel thành công!\nĐường dẫn: {saveDialog.FileName}\n\nBạn có muốn mở file ngay bằng Excel không?",
                        "Xuất file thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (choice == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(saveDialog.FileName)
                        {
                            UseShellExecute = true
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra sự cố khi xuất tệp Excel:\n" + ex.Message,
                    "Lỗi xuất file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnExportExcel.Enabled = true;
            }
        }

        private static string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}


