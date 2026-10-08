using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        public FormProductManagement()
        {
            InitializeComponent();

            Load += FormProductManagement_Load;

            dgvProducts.CellClick += dgvProducts_CellClick;

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnSearch.Click += btnSearch_Click;
        }
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
                        SessionManager.JwtToken
                    );
            }

            return client;
        }

        // =========================================
        // LOAD FORM
        // =========================================

        private async void FormProductManagement_Load(
            object? sender,
            EventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadProductsAsync();
        }

        // =========================================
        // LOAD CATEGORY
        // =========================================

        private async Task LoadCategoriesAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();

                var categories =
                    await client
                        .GetFromJsonAsync<List<CategoryDto>>(
                            "categories");

                if (categories == null)
                    return;

                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";
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

        // =========================================
        // LOAD PRODUCTS
        // =========================================

        private async Task LoadProductsAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();

                var products =
                    await client.GetFromJsonAsync<List<ProductDto>>(
                        "products");

                dgvProducts.DataSource = products;
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

        // =========================================
        // CLICK DATAGRIDVIEW
        // =========================================

        private void dgvProducts_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
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
                nudPrice.Value =
                    Convert.ToDecimal(
                        row.Cells["Price"].Value);
            }

            if (row.Cells["StockQuantity"].Value != null)
            {
                nudStock.Value =
                    Convert.ToDecimal(
                        row.Cells["StockQuantity"].Value);
            }

            if (row.Cells["CategoryId"].Value != null)
            {
                cboCategory.SelectedValue =
                    Convert.ToInt32(
                        row.Cells["CategoryId"].Value);
            }
        }

        // =========================================
        // THÊM
        // =========================================

        private async void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtBarcode.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã Barcode!");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtProductName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm!");

                return;
            }

            if (cboCategory.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn danh mục!");

                return;
            }

            var newProduct = new
            {
                Barcode = txtBarcode.Text.Trim(),

                ProductName =
                    txtProductName.Text.Trim(),

                Price = nudPrice.Value,

                StockQuantity =
                    (int)nudStock.Value,

                CategoryId =
                    Convert.ToInt32(
                        cboCategory.SelectedValue)
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.PostAsJsonAsync(
                        "products",
                        newProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm sản phẩm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadProductsAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        $"Không thể thêm sản phẩm.\n" +
                        $"Mã lỗi: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message);
            }
        }

        // =========================================
        // SỬA
        // =========================================

        private async void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(
                txtId.Text,
                out int id))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần sửa!");

                return;
            }

            var updateProduct = new
            {
                ProductId = id,

                Barcode =
                    txtBarcode.Text.Trim(),

                ProductName =
                    txtProductName.Text.Trim(),

                Price = nudPrice.Value,

                StockQuantity =
                    (int)nudStock.Value,

                CategoryId =
                    Convert.ToInt32(
                        cboCategory.SelectedValue)
            };

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.PutAsJsonAsync(
                        $"products/{id}",
                        updateProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật thành công!");

                    await LoadProductsAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        $"Không thể cập nhật.\n" +
                        $"Mã lỗi: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message);
            }
        }

        // =========================================
        // XÓA
        // =========================================

        private async void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            if (!int.TryParse(
                txtId.Text,
                out int id))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa!");

                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using var client = GetAuthenticatedClient();

                var response =
                    await client.DeleteAsync(
                        $"products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa sản phẩm thành công!");

                    await LoadProductsAsync();

                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        $"Không thể xóa.\n" +
                        $"Mã lỗi: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message);
            }
        }

        // =========================================
        // TÌM KIẾM
        // =========================================

        private async void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            string keyword =
                txtKeyword.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                await LoadProductsAsync();
                return;
            }

            try
            {
                using var client = GetAuthenticatedClient();

                var products =
                    await client.GetFromJsonAsync<List<ProductDto>>(
                        $"products/search?keyword={Uri.EscapeDataString(keyword)}");

                dgvProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tìm thấy sản phẩm:\n" +
                    ex.Message);
            }
        }

        // =========================================
        // LÀM MỚI
        // =========================================

        private void btnClear_Click(
            object? sender,
            EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();

            nudPrice.Value = 0;
            nudStock.Value = 0;

            txtKeyword.Clear();

            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            dgvProducts.ClearSelection();
        }
    }

    // =========================================
    // PRODUCT DTO
    // =========================================

    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = "";

        public string ProductName { get; set; } = "";

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
    }
}