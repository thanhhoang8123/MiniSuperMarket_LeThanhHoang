using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();

        public FormPOS()
        {
            InitializeComponent();

            SetupCartGrid();

           
        }

        // =====================================================
        // HTTP CLIENT CÓ TOKEN
        // =====================================================

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

        // =====================================================
        // CẤU HÌNH DATAGRIDVIEW GIỎ HÀNG
        // =====================================================

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductId",
                    HeaderText = "Mã SP",
                    Width = 80
                });

            dgvCart.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "ProductName",
                    HeaderText = "Tên Sản Phẩm",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill
                });

            dgvCart.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "UnitPrice",
                    HeaderText = "Đơn Giá",
                    Width = 110,
                    DefaultCellStyle =
                    {
                        Format = "N0"
                    }
                });

            dgvCart.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Quantity",
                    HeaderText = "SL",
                    Width = 70
                });

            dgvCart.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TotalPrice",
                    HeaderText = "Thành Tiền",
                    Width = 120,
                    DefaultCellStyle =
                    {
                        Format = "N0"
                    }
                });

            dgvCart.ReadOnly = true;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
        }

        // =====================================================
        // QUÉT BARCODE
        // =====================================================

        private async void txtBarcode_KeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter &&
                !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                e.SuppressKeyPress = true;

                string barcode = txtBarcode.Text.Trim();

                txtBarcode.Clear();

                await AddProductToCartByBarcodeAsync(barcode);

                txtBarcode.Focus();
            }
        }

        // =====================================================
        // TÌM SẢN PHẨM THEO BARCODE
        // =====================================================

        private async Task AddProductToCartByBarcodeAsync(
            string barcode)
        {
            try
            {
                using var client = GetAuthenticatedClient();

                var product =
                    await client.GetFromJsonAsync<ProductDto>(
                        $"products/barcode/{barcode}");

                if (product == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy sản phẩm có mã vạch này!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                var existingItem =
                    _cart.FirstOrDefault(
                        c => c.ProductId == product.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(
                        new CartItemDto
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            UnitPrice = product.Price,
                            Quantity = 1
                        });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối máy chủ:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CẬP NHẬT GIỎ HÀNG
        // =====================================================

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            decimal total =
                _cart.Sum(x => x.TotalPrice);

            lblTotalAmount.Text =
                $"{total:N0} đ";

            CalculateChange();
        }

        // =====================================================
        // TÍNH TIỀN THỪA
        // =====================================================

        private void txtCashReceived_TextChanged(
            object? sender,
            EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total =
                _cart.Sum(x => x.TotalPrice);

            if (decimal.TryParse(
                txtCashReceived.Text,
                out decimal cashReceived))
            {
                decimal change =
                    cashReceived - total;

                if (change >= 0)
                {
                    lblChange.Text =
                        $"{change:N0} đ";

                    lblChange.ForeColor =
                        Color.Black;
                }
                else
                {
                    lblChange.Text =
                        "Chưa đủ tiền!";

                    lblChange.ForeColor =
                        Color.Red;
                }
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
            }
        }

        // =====================================================
        // THANH TOÁN
        // =====================================================

        private async void btnCheckout_Click(
            object? sender,
            EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal total =
                _cart.Sum(x => x.TotalPrice);

            if (!decimal.TryParse(
                txtCashReceived.Text,
                out decimal cashReceived))
            {
                MessageBox.Show(
                    "Vui lòng nhập số tiền khách đưa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cashReceived < total)
            {
                MessageBox.Show(
                    "Số tiền khách đưa chưa đủ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var orderRequest = new
            {
                CashierUsername =
                    SessionManager.CurrentUsername,

                CustomerPhone =
                    txtCustomerPhone.Text.Trim(),

                Items = _cart.Select(
                    i => new
                    {
                        i.ProductId,
                        i.Quantity,
                        i.UnitPrice
                    }).ToList()
            };

            try
            {
                using var client =
                    GetAuthenticatedClient();

                var response =
                    await client.PostAsJsonAsync(
                        "orders/checkout",
                        orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thanh toán thành công và đã in hóa đơn!",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearCart();
                }
                else if (
                    response.StatusCode ==
                    System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show(
                        "Bạn không có quyền thực hiện thanh toán!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"Thanh toán thất bại!\nMã lỗi: {response.StatusCode}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // HỦY GIỎ HÀNG
        // =====================================================

        private void btnClearCart_Click(object? sender, EventArgs e)
        {
            if (_cart.Count == 0)
                return;

            var result = MessageBox.Show(
                "Bạn có chắc muốn hủy toàn bộ giỏ hàng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _cart.Clear();
                UpdateCartDisplay();

                txtCashReceived.Clear();
                txtCustomerPhone.Clear();
                lblCustomerName.Text = "Khách vãng lai";

                txtBarcode.Focus();
            }
        }

        private void ClearCart()
        {
            _cart.Clear();

            UpdateCartDisplay();

            txtCashReceived.Clear();
            txtCustomerPhone.Clear();

            lblCustomerName.Text =
                "Khách vãng lai";

            txtBarcode.Focus();
        }

        // =====================================================
        // DTO
        // =====================================================

        public class CartItemDto
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
                = string.Empty;

            public decimal UnitPrice { get; set; }

            public int Quantity { get; set; }

            public decimal TotalPrice =>
                UnitPrice * Quantity;
        }

        public class ProductDto
        {
            public int ProductId { get; set; }

            public string Barcode { get; set; }
                = string.Empty;

            public string ProductName { get; set; }
                = string.Empty;

            public decimal Price { get; set; }

            public int StockQuantity { get; set; }

            public int CategoryId { get; set; }
        }
    }
}