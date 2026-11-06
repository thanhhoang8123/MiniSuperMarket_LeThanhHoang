
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.DTOs;
using MiniSupermarket.WinForms.Services;


namespace MiniSupermarket.WinForms
{
    public partial class FormOrderManagement : Form
    {
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages;
        private int _totalOrders;
        private bool _suppressSelectionChanged;
        private readonly Button btnExportOrders = new();
        private readonly Button btnPreviousPage = new() { Text = "◀ Trang trước" };
        private readonly Button btnNextPage = new() { Text = "Trang sau ▶" };
        private readonly Label lblPageInfo = new() { TextAlign = System.Drawing.ContentAlignment.MiddleCenter };
        public FormOrderManagement()
        {
            InitializeComponent();

            btnExportOrders.Text = "Xuất Excel / CSV";
            btnExportOrders.Size = new System.Drawing.Size(160, 36);
            btnExportOrders.FlatStyle = FlatStyle.Flat;
            btnExportOrders.FlatAppearance.BorderSize = 0;
            btnExportOrders.BackColor = System.Drawing.Color.FromArgb(5, 150, 105);
            btnExportOrders.ForeColor = System.Drawing.Color.White;
            btnExportOrders.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnExportOrders.Click += btnExportOrders_Click;
            Controls.Add(btnExportOrders);

            StyleToolbarButton(btnRefresh, System.Drawing.Color.FromArgb(37, 99, 235));
            btnRefresh.Text = "↻ Làm mới";
            StyleToolbarButton(btnPreviousPage, System.Drawing.Color.FromArgb(71, 85, 105), false);
            StyleToolbarButton(btnNextPage, System.Drawing.Color.FromArgb(71, 85, 105), false);
            lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            Controls.Add(btnPreviousPage);
            Controls.Add(lblPageInfo);
            Controls.Add(btnNextPage);
            btnPreviousPage.Click += async (s, e) =>
            {
                if (_currentPage > 1) { _currentPage--; await LoadOrdersAsync(); }
            };
            btnNextPage.Click += async (s, e) =>
            {
                if (_currentPage < _totalPages) { _currentPage++; await LoadOrdersAsync(); }
            };

            // Đăng ký sự kiện
            Load += FormOrderManagement_Load;
            btnRefresh.Click += btnRefresh_Click;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;

            // Định dạng giao diện
            FormatGrids();

            Resize += (s, e) => ResizeOrderLayout();
            Load += (s, e) => ResizeOrderLayout();
        }

        private void ResizeOrderLayout()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0) return;

            dgvOrders.Anchor = AnchorStyles.None;
            dgvDetails.Anchor = AnchorStyles.None;
            btnRefresh.Anchor = AnchorStyles.None;
            lblStatus.Anchor = AnchorStyles.None;
            lblDetailsTitle.Anchor = AnchorStyles.None;

            const int margin = 20;
            int w = Math.Max(300, ClientSize.Width - margin * 2);

            lblTitle.Location = new System.Drawing.Point(margin, 16);
            btnRefresh.Location = new System.Drawing.Point(Math.Max(margin + 200, ClientSize.Width - margin - btnRefresh.Width), 14);
            btnExportOrders.Location = new System.Drawing.Point(btnRefresh.Left - btnExportOrders.Width - 10, 14);

            int topH = Math.Clamp((int)(ClientSize.Height * 0.38), 180, 300);

            dgvOrders.Location = new System.Drawing.Point(margin, 60);
            dgvOrders.Size = new System.Drawing.Size(w, topH);

            int pagerY = dgvOrders.Bottom + 5;
            btnPreviousPage.SetBounds(margin, pagerY, 118, 30);
            lblPageInfo.SetBounds((ClientSize.Width - 300) / 2, pagerY, 300, 30);
            btnNextPage.SetBounds(ClientSize.Width - margin - 118, pagerY, 118, 30);

            lblStatus.Location = new System.Drawing.Point(margin, pagerY + 34);
            lblStatus.Size = new System.Drawing.Size(w, 24);

            lblDetailsTitle.Location = new System.Drawing.Point(margin, lblStatus.Bottom + 3);

            int bottomTop = lblDetailsTitle.Bottom + 6;
            int bottomH = Math.Max(100, ClientSize.Height - bottomTop - margin);
            dgvDetails.Location = new System.Drawing.Point(margin, bottomTop);
            dgvDetails.Size = new System.Drawing.Size(w, bottomH);
        }

        private static void StyleToolbarButton(Button button, System.Drawing.Color color, bool prominent = true)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = prominent ? 0 : 1;
            button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            button.BackColor = color;
            button.ForeColor = System.Drawing.Color.White;
            button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        // =====================================================
        // KHỞI TẠO GIAO DIỆN
        // =====================================================

        private static void ApplyModernGridStyle(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dgv.RowTemplate.Height = 34;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            dgv.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            dgv.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = BorderStyle.None;
        }

        private void FormatGrids()
        {
            dgvOrders.AutoGenerateColumns = false;
            dgvDetails.AutoGenerateColumns = false;

            dgvOrders.ReadOnly = true;
            dgvDetails.ReadOnly = true;

            dgvOrders.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvOrders.MultiSelect = false;
            dgvDetails.MultiSelect = false;

            dgvOrders.AllowUserToAddRows = false;
            dgvDetails.AllowUserToAddRows = false;

            ApplyModernGridStyle(dgvOrders);
            ApplyModernGridStyle(dgvDetails);

            // Cột danh sách đơn hàng
            dgvOrders.Columns.Clear();

            dgvOrders.Columns.Add(CreateColumn(
                "OrderId", "Mã đơn"));

            dgvOrders.Columns.Add(CreateColumn(
                "OrderCode", "Mã hóa đơn"));

            dgvOrders.Columns.Add(CreateColumn(
                "CreatedDate", "Ngày tạo"));

            dgvOrders.Columns.Add(CreateColumn(
                "CashierUsername", "Thu ngân"));

            dgvOrders.Columns.Add(CreateColumn(
                "CustomerName", "Khách hàng"));

            dgvOrders.Columns.Add(CreateColumn(
                "FinalAmount", "Tổng tiền", "N0"));

            // Cột chi tiết đơn hàng
            dgvDetails.Columns.Clear();

            dgvDetails.Columns.Add(CreateColumn(
                "ProductId", "Mã sản phẩm"));

            dgvDetails.Columns.Add(CreateColumn(
                "ProductName", "Tên sản phẩm"));

            dgvDetails.Columns.Add(CreateColumn(
                "Quantity", "Số lượng"));

            dgvDetails.Columns.Add(CreateColumn(
                "UnitPrice", "Đơn giá", "N0"));

            dgvDetails.Columns.Add(CreateColumn(
                "LineTotal", "Thành tiền", "N0"));
        }

        private static DataGridViewTextBoxColumn CreateColumn(
            string propertyName,
            string headerText,
            string? format = null)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = propertyName,
                DataPropertyName = propertyName,
                HeaderText = headerText,
                ReadOnly = true,
                MinimumWidth = 80,
                AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill
            };

            if (!string.IsNullOrEmpty(format))
            {
                column.DefaultCellStyle.Format = format;
            }

            return column;
        }

        // =====================================================
        // SỰ KIỆN FORM
        // =====================================================

        private async void FormOrderManagement_Load(
            object? sender,
            EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async void btnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async void dgvOrders_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (_suppressSelectionChanged) return;
            await LoadSelectedOrderDetailsAsync();
        }

        // =====================================================
        // TẢI DANH SÁCH ĐƠN HÀNG
        // =====================================================


        private async Task LoadOrdersAsync()
        {
            try
            {
                btnRefresh.Enabled = false;
                lblStatus.Text = "Đang tải danh sách đơn hàng...";

                var result = await ApiClientService.GetAsync<PagedResultDto<OrderDto>>(
                    $"orders/paged?pageNumber={_currentPage}&pageSize={PageSize}");
                var orders = result?.Items ?? new List<OrderDto>();
                _currentPage = result?.CurrentPage ?? 1;
                _totalPages = result?.TotalPages ?? 0;
                _totalOrders = result?.TotalItems ?? 0;

                _suppressSelectionChanged = true;
                dgvOrders.DataSource = null;
                dgvDetails.DataSource = null;
                dgvOrders.DataSource = orders;
                UpdatePaginationControls();

                if (orders.Count > 0)
                {
                    dgvOrders.ClearSelection();
                    dgvOrders.CurrentCell = dgvOrders.Rows[0].Cells[0];
                    dgvOrders.Rows[0].Selected = true;
                    _suppressSelectionChanged = false;
                    await LoadSelectedOrderDetailsAsync();
                }
                else
                {
                    _suppressSelectionChanged = false;
                    lblStatus.Text = _totalOrders == 0 ? "Chưa có đơn hàng nào." : "Trang này không có đơn hàng.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh sách đơn hàng.\n\n" + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
                    "Lỗi tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _suppressSelectionChanged = false;
                btnRefresh.Enabled = true;
            }
        }

        private void UpdatePaginationControls()
        {
            lblPageInfo.Text = _totalOrders == 0
                ? "Trang 0 / 0"
                : $"Trang {_currentPage} / {_totalPages}   •   {_totalOrders:N0} đơn hàng";
            btnPreviousPage.Enabled = _currentPage > 1;
            btnNextPage.Enabled = _currentPage < _totalPages;
        }

        private async void btnExportOrders_Click(object? sender, EventArgs e)
        {
            try
            {
                var orders = await ApiClientService.GetAsync<List<OrderDto>>("orders") ?? new List<OrderDto>();
                if (orders.Count == 0)
                {
                    MessageBox.Show("Chưa có hóa đơn để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var dialog = new SaveFileDialog
                {
                    Filter = "Sổ làm việc Excel (*.xlsx)|*.xlsx|Tệp CSV (*.csv)|*.csv",
                    FileName = $"DanhSachHoaDon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx",
                    AddExtension = true,
                    Title = "Xuất danh sách hóa đơn"
                };
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var lines = new List<string>
                {
                    "Mã đơn,Mã hóa đơn,Ngày tạo,Thu ngân,Khách hàng,Số mặt hàng,Tổng tiền (VNĐ)"
                };
                foreach (var order in orders)
                {
                    lines.Add(string.Join(",", new[]
                    {
                        order.OrderId.ToString(),
                        Csv(order.OrderCode),
                        Csv(order.CreatedDate.ToString("dd/MM/yyyy HH:mm")),
                        Csv(order.CashierUsername),
                        Csv(order.CustomerName),
                        order.TotalItems.ToString(),
                        order.FinalAmount.ToString(System.Globalization.CultureInfo.InvariantCulture)
                    }));
                }
                if (System.IO.Path.GetExtension(dialog.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    var headers = new[] { "Mã đơn", "Mã hóa đơn", "Ngày tạo", "Thu ngân", "Khách hàng", "Số mặt hàng", "Tổng tiền (VNĐ)" };
                    var rows = orders.Select(order => (IReadOnlyList<object?>)new object?[]
                    {
                        order.OrderId,
                        order.OrderCode,
                        order.CreatedDate,
                        order.CashierUsername,
                        order.CustomerName,
                        order.TotalItems,
                        order.FinalAmount
                    }).ToList();
                    SpreadsheetExportService.WriteXlsx(dialog.FileName, "Hóa đơn", headers, rows);
                }
                else
                {
                    await System.IO.File.WriteAllTextAsync(dialog.FileName, string.Join(Environment.NewLine, lines), new System.Text.UTF8Encoding(true));
                }
                MessageBox.Show($"Đã xuất {orders.Count} hóa đơn ra tệp CSV.", "Xuất thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xuất danh sách hóa đơn." + Environment.NewLine + Environment.NewLine + MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex), "Lỗi xuất file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Csv(string? value) => "\"" + (value ?? string.Empty).Replace("\"", "\"\"") + "\"";
        // =====================================================
        // XEM CHI TIẾT ĐƠN HÀNG
        // =====================================================

        private async Task LoadSelectedOrderDetailsAsync()
        {
            if (dgvOrders.CurrentRow?.DataBoundItem
                is not OrderDto selectedOrder)
            {
                return;
            }

            try
            {
                lblStatus.Text =
                    $"Đang tải chi tiết đơn #{selectedOrder.OrderId}...";

                var order =
                    await ApiClientService.GetAsync<OrderDto>(
                        $"orders/{selectedOrder.OrderId}");

                var details =
                    order?.OrderDetails ??
                    new List<OrderDetailDto>();

                dgvDetails.DataSource = null;
                dgvDetails.DataSource = details;

                lblStatus.Text =
                    $"Đơn hàng #{selectedOrder.OrderId} — " +
                    $"{details.Count} dòng chi tiết.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được chi tiết đơn hàng.\n\n" +
                    MiniSupermarket.WinForms.Services.ApiClientService.GetFriendlyExceptionMessage(ex),
                    "Lỗi tải chi tiết",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

      

       

      
    }
}
