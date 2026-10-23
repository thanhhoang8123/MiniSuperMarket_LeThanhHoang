
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
        }

        // 1. LOAD FORM
        private async void FormProductManagement_Load(
            object? sender, EventArgs e)
        {
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
    }
}
