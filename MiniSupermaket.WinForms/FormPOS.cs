using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.DTOs;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new();
        private DTOs.CustomerDto? _currentCustomer = null;
        private bool _customerLookupFailed;
        private string _customerLookupErrorMessage = string.Empty;

        public FormPOS()
        {
            InitializeComponent();

            SetupCartGrid();
            SetupLoyaltyEvents();
        }

        // =====================================================
        // CẤU HÌNH DATAGRIDVIEW GIỎ HÀNG
        // =====================================================
        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                MinimumWidth = 65,
                FillWeight = 12,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên sản phẩm",
                MinimumWidth = 130,
                FillWeight = 34,
                DefaultCellStyle = { Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Giá bán",
                MinimumWidth = 90,
                FillWeight = 20,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng",
                MinimumWidth = 75,
                FillWeight = 15,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành tiền",
                MinimumWidth = 110,
                FillWeight = 24,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight, ForeColor = Color.FromArgb(16, 185, 129), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) }
            });

            // Tích hợp Menu chuột phải để xóa từng món hàng khỏi giỏ
            var cartMenu = new ContextMenuStrip();
            var mnuDelete = new ToolStripMenuItem("🗑️ Xóa mặt hàng này khỏi giỏ");
            mnuDelete.Click += (s, e) =>
            {
                if (dgvCart.CurrentRow?.DataBoundItem is CartItemDto item)
                {
                    _cart.Remove(item);
                    UpdateCartDisplay();
                }
            };
            cartMenu.Items.Add(mnuDelete);
            dgvCart.ContextMenuStrip = cartMenu;

            // Hỗ trợ phím Delete để xóa nhanh món hàng đang chọn
            dgvCart.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete && dgvCart.CurrentRow?.DataBoundItem is CartItemDto item)
                {
                    _cart.Remove(item);
                    UpdateCartDisplay();
                    e.Handled = true;
                }
            };

            dgvCart.ReadOnly = true;
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.MultiSelect = false;
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersHeight = 38;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvCart.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvCart.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgvCart.RowTemplate.Height = 36;
            dgvCart.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dgvCart.GridColor = Color.FromArgb(226, 232, 240);
            dgvCart.Font = new Font("Segoe UI", 9.5F);
        }
        // =====================================================
        // CẤU HÌNH SỰ KIỆN TÍCH ĐIỂM & ĐỔI ĐIỂM THƯỞNG
        // =====================================================
        private void SetupLoyaltyEvents()
        {
            txtCustomerPhone.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await LookupCustomerAsync();
                }
            };

            txtCustomerPhone.Leave += async (s, e) =>
            {
                await LookupCustomerAsync();
            };

            chkUseRewardPoints.CheckedChanged += (s, e) =>
            {
                nudPointsToRedeem.Enabled = chkUseRewardPoints.Checked;
                if (chkUseRewardPoints.Checked && nudPointsToRedeem.Value == 0 && _currentCustomer != null && _currentCustomer.RewardPoints > 0)
                {
                    // Mặc định đề xuất đổi 50 điểm (hoặc số điểm khách hiện có)
                    nudPointsToRedeem.Value = Math.Min(50m, Math.Min(_currentCustomer.RewardPoints, nudPointsToRedeem.Maximum));
                }
                RecalculatePayment();
            };

            nudPointsToRedeem.ValueChanged += (s, e) =>
            {
                RecalculatePayment();
            };
        }

        // =====================================================
        // TRA CỨU KHÁCH HÀNG THÂN THIẾT & ĐIỂM THƯỞNG TÍCH LŨY
        // =====================================================
        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                _customerLookupFailed = false;
                _customerLookupErrorMessage = string.Empty;
                _currentCustomer = null;
                lblCustomerName.Text = "Khách vãng lai";
                lblCustomerName.ForeColor = Color.FromArgb(37, 99, 235);
                lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";
                chkUseRewardPoints.Checked = false;
                chkUseRewardPoints.Enabled = false;
                nudPointsToRedeem.Value = 0;
                nudPointsToRedeem.Enabled = false;
                RecalculatePayment();
                return;
            }

            try
            {
                var response = await ApiClientService.GetAsync<CustomerByPhoneResponse>($"customers/by-phone/{Uri.EscapeDataString(phone)}");

                if (response?.Success == true && response.Data != null)
                {
                    _customerLookupFailed = false;
                    _customerLookupErrorMessage = string.Empty;
                    _currentCustomer = response.Data;
                    lblCustomerName.Text = $"{_currentCustomer.CustomerName} ({_currentCustomer.MembershipRank})";
                    lblCustomerName.ForeColor = Color.FromArgb(37, 99, 235);
                    lblCustomerPoints.Text = $"⭐ Điểm tích lũy: {_currentCustomer.RewardPoints:N0} điểm";

                    if (_currentCustomer.RewardPoints > 0)
                    {
                        chkUseRewardPoints.Enabled = true;
                        nudPointsToRedeem.Maximum = _currentCustomer.RewardPoints;
                    }
                    else
                    {
                        chkUseRewardPoints.Checked = false;
                        chkUseRewardPoints.Enabled = false;
                        nudPointsToRedeem.Value = 0;
                    }
                }
                else
                {
                    _customerLookupFailed = true;
                    _customerLookupErrorMessage = "Không tìm thấy khách hàng với số điện thoại này.";
                    _currentCustomer = null;
                    lblCustomerName.Text = "Không tìm thấy khách hàng";
                    lblCustomerName.ForeColor = Color.FromArgb(185, 28, 28);
                    lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";
                    chkUseRewardPoints.Checked = false;
                    chkUseRewardPoints.Enabled = false;
                    nudPointsToRedeem.Value = 0;
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _customerLookupFailed = true;
                _customerLookupErrorMessage = "Không tìm thấy khách hàng với số điện thoại này.";
                _currentCustomer = null;
                lblCustomerName.Text = "Không tìm thấy khách hàng";
                lblCustomerName.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";
                chkUseRewardPoints.Checked = false;
                chkUseRewardPoints.Enabled = false;
                nudPointsToRedeem.Value = 0;
            }
            catch (HttpRequestException)
            {
                _customerLookupFailed = true;
                _customerLookupErrorMessage = "Không kết nối được máy chủ để tra cứu khách hàng.";
                _currentCustomer = null;
                lblCustomerName.Text = "Lỗi kết nối khi tra cứu khách hàng";
                lblCustomerName.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";
                chkUseRewardPoints.Checked = false;
                chkUseRewardPoints.Enabled = false;
                nudPointsToRedeem.Value = 0;
            }
            catch (Exception)
            {
                _customerLookupFailed = true;
                _customerLookupErrorMessage = "Tra cứu khách hàng thất bại. Hãy kiểm tra kết nối hoặc số điện thoại.";
                _currentCustomer = null;
                lblCustomerName.Text = "Tra cứu khách hàng thất bại";
                lblCustomerName.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";
                chkUseRewardPoints.Checked = false;
                chkUseRewardPoints.Enabled = false;
                nudPointsToRedeem.Value = 0;
            }

            RecalculatePayment();
        }

        // =====================================================
        // QUÉT BARCODE
        // =====================================================
        private async void txtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                txtBarcode.Focus();
                return;
            }

            txtBarcode.Clear();

            try
            {
                await AddProductToCartByBarcodeAsync(barcode);
            }
            finally
            {
                txtBarcode.Focus();
            }
        }

        // =====================================================
        // TÌM SẢN PHẨM THEO BARCODE
        // =====================================================
        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await ApiClientService.GetAsync<ProductDto>(
                    "ado-products/barcode/" + Uri.EscapeDataString(barcode));

                if (product == null)
                {
                    MessageBox.Show(
                        $"Không tìm thấy sản phẩm có mã vạch '{barcode}' trong hệ thống!",
                        "Cảnh báo quét mã",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra nếu sản phẩm đã hết hàng trong kho
                if (product.StockQuantity <= 0)
                {
                    MessageBox.Show(
                        $"Sản phẩm '{product.ProductName}' hiện đã hết hàng trong kho!",
                        "Cảnh báo hết hàng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);

                // Tự cộng dồn số lượng nếu đã có trong giỏ hàng
                if (existingItem != null)
                {
                    if (existingItem.Quantity >= product.StockQuantity)
                    {
                        MessageBox.Show(
                            $"Sản phẩm '{product.ProductName}' trong kho chỉ còn {product.StockQuantity} cái, không thể thêm tiếp!",
                            "Cảnh báo tồn kho",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    existingItem.Quantity++;
                }
                else
                {
                    // Thêm sản phẩm mới vào giỏ hàng
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show(
                    "Không thể kết nối đến máy chủ Backend Web API!\nVui lòng kiểm tra lại dịch vụ máy chủ.",
                    "Lỗi mất kết nối mạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra sự cố khi tra cứu sản phẩm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CẬP NHẬT HIỂN THỊ GIỎ HÀNG
        // =====================================================
        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();
            RecalculatePayment();
        }

        // =====================================================
        // TÍNH TOÁN TIỀN THANH TOÁN & GIẢM GIÁ ĐỔI ĐIỂM THƯỞNG
        // =====================================================
        private void RecalculatePayment()
        {
            decimal subTotal = _cart.Sum(x => x.TotalPrice);
            lblSubTotal.Text = $"{subTotal:N0} đ";

            int affordablePoints = (int)Math.Min(int.MaxValue, decimal.Floor(subTotal / 1000m));
            int customerPoints = _currentCustomer?.RewardPoints ?? 0;
            int maxRedeemablePoints = Math.Min(affordablePoints, customerPoints);
            nudPointsToRedeem.Maximum = maxRedeemablePoints;
            chkUseRewardPoints.Enabled = maxRedeemablePoints > 0;
            nudPointsToRedeem.Enabled = chkUseRewardPoints.Checked && maxRedeemablePoints > 0;
            if (maxRedeemablePoints == 0 && chkUseRewardPoints.Checked)
            {
                chkUseRewardPoints.Checked = false;
            }

            decimal discount = 0m;
            if (chkUseRewardPoints.Checked && nudPointsToRedeem.Value > 0)
            {
                discount = nudPointsToRedeem.Value * 1000m;
            }

            lblDiscountValue.Text = discount > 0 ? $"-{discount:N0} đ" : "-0 đ";

            decimal finalTotal = Math.Max(0m, subTotal - discount);
            lblTotalAmount.Text = $"{finalTotal:N0} đ";

            CalculateChange(finalTotal);
        }

        // =====================================================
        // TÍNH TIỀN THỪA CHO KHÁCH
        // =====================================================
        private void txtCashReceived_TextChanged(object? sender, EventArgs e)
        {
            RecalculatePayment();
        }

        private void CalculateChange(decimal finalTotal)
        {
            if (decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                decimal change = cashReceived - finalTotal;

                if (change >= 0)
                {
                    lblChange.Text = $"{change:N0} đ";
                    lblChange.ForeColor = Color.FromArgb(25, 135, 84); // Xanh lá
                }
                else
                {
                    lblChange.Text = $"Thiếu: {Math.Abs(change):N0} đ";
                    lblChange.ForeColor = Color.FromArgb(220, 53, 69); // Đỏ cảnh báo
                }
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.FromArgb(25, 135, 84);
            }
        }

        // =====================================================
        // THANH TOÁN HÓA ĐƠN
        // =====================================================
        private async void btnCheckout_Click(object? sender, EventArgs e)
        {
            if (_customerLookupFailed && !string.IsNullOrWhiteSpace(txtCustomerPhone.Text))
            {
                MessageBox.Show(
                    $"{_customerLookupErrorMessage}\n\nHãy kiểm tra lại số điện thoại hoặc xóa ô SĐT nếu bán cho khách vãng lai.",
                    "Chưa xác minh khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCustomerPhone.Focus();
                return;
            }

            if (_cart.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            decimal subTotal = _cart.Sum(x => x.TotalPrice);
            int pointsToRedeem = chkUseRewardPoints.Checked ? (int)nudPointsToRedeem.Value : 0;
            decimal discount = pointsToRedeem * 1000m;
            decimal finalTotal = Math.Max(0m, subTotal - discount);

            if (!decimal.TryParse(txtCashReceived.Text, out decimal cashReceived))
            {
                MessageBox.Show(
                    "Vui lòng nhập số tiền khách đưa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cashReceived < finalTotal)
            {
                MessageBox.Show(
                    "Số tiền khách đưa chưa đủ để thanh toán!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                PointsToRedeem = pointsToRedeem,
                Items = _cart.Select(i => new
                {
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                }).ToList()
            };

            btnCheckout.Enabled = false;

            try
            {
                using var response = await ApiClientService.PostAsync("orders/checkout", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    string successMessage = pointsToRedeem > 0
                        ? $"Thanh toán thành công và đã in hóa đơn!\n\n" +
                          $"• Tạm tính: {subTotal:N0} đ\n" +
                          $"• Đổi {pointsToRedeem} điểm thưởng: -{discount:N0} đ\n" +
                          $"• Tổng thanh toán: {finalTotal:N0} đ\n" +
                          $"• Tiền khách đưa: {cashReceived:N0} đ\n" +
                          $"• Tiền thối lại: {(cashReceived - finalTotal):N0} đ"
                        : $"Thanh toán thành công và đã in hóa đơn!\n\n" +
                          $"• Tổng thanh toán: {finalTotal:N0} đ\n" +
                          $"• Tiền khách đưa: {cashReceived:N0} đ\n" +
                          $"• Tiền thối lại: {(cashReceived - finalTotal):N0} đ";

                    MessageBox.Show(
                        successMessage,
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearCart();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show(
                        "Bạn không có quyền thực hiện thanh toán!",
                        "Từ chối truy cập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    string errContent = await response.Content.ReadAsStringAsync();
                    string friendlyMessage = "Thao tác thanh toán không thành công.";

                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(errContent);
                        if (doc.RootElement.TryGetProperty("message", out var msgProp))
                        {
                            friendlyMessage = msgProp.GetString() ?? friendlyMessage;
                        }
                    }
                    catch
                    {
                        // Giữ thông báo thân thiện mặc định nếu body không phải JSON
                    }

                    MessageBox.Show(
                        friendlyMessage,
                        "Thông báo thanh toán",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show(
                    "Mất kết nối với máy chủ API khi gửi đơn thanh toán!\nVui lòng kiểm tra lại mạng và thử lại.",
                    "Lỗi mất kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã xảy ra lỗi không xác định:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
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
                "Bạn có chắc chắn muốn hủy toàn bộ giỏ hàng không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ClearCart();
            }
        }

        private void ClearCart()
        {
            _cart.Clear();
            _currentCustomer = null;
            _customerLookupFailed = false;
            _customerLookupErrorMessage = string.Empty;

            UpdateCartDisplay();

            txtCashReceived.Clear();
            txtCustomerPhone.Clear();
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.ForeColor = Color.FromArgb(37, 99, 235);
            lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";

            chkUseRewardPoints.Checked = false;
            chkUseRewardPoints.Enabled = false;
            nudPointsToRedeem.Value = 0;
            nudPointsToRedeem.Enabled = false;
            lblDiscountValue.Text = "-0 đ";
            lblSubTotal.Text = "0 đ";
            lblTotalAmount.Text = "0 đ";
            lblChange.Text = "0 đ";
            lblChange.ForeColor = Color.FromArgb(25, 135, 84);

            txtBarcode.Focus();
        }

        // =====================================================
        // PHÍM TẮT BÁN HÀNG F9
        // =====================================================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnCheckout.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =====================================================
        // DTO NỘI BỘ MÀN HÌNH POS
        // =====================================================
        public class CartItemDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; } = string.Empty;
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice => UnitPrice * Quantity;
        }

        public class ProductDto
        {
            public int ProductId { get; set; }
            public string Barcode { get; set; } = string.Empty;
            public string ProductName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public int CategoryId { get; set; }
        }
    }
}


