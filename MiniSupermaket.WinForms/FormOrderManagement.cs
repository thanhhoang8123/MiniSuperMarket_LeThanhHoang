
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
        public FormOrderManagement()
        {
            InitializeComponent();


            // Đăng ký sự kiện
            Load += FormOrderManagement_Load;
            btnRefresh.Click += btnRefresh_Click;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;

            // Định dạng giao diện
            FormatGrids();
        }

        // =====================================================
        // KHỞI TẠO GIAO DIỆN
        // =====================================================

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

                // Lấy danh sách tất cả đơn hàng
                var result =
                    await ApiClientService.GetAsync<List<OrderDto>>("orders");

                // Nếu API trả về null thì dùng danh sách rỗng
                var orders = result ?? new List<OrderDto>();

                dgvOrders.DataSource = null;
                dgvDetails.DataSource = null;

                dgvOrders.DataSource = orders;

                lblStatus.Text = $"Tổng số đơn hàng: {orders.Count}";

                if (orders.Count > 0)
                {
                    dgvOrders.ClearSelection();
                    dgvOrders.CurrentCell = dgvOrders.Rows[0].Cells[0];
                    dgvOrders.Rows[0].Selected = true;

                    await LoadSelectedOrderDetailsAsync();
                }
                else
                {
                    lblStatus.Text = "Chưa có đơn hàng nào.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tải được danh sách đơn hàng.\n\n" + ex.Message,
                    "Lỗi tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

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
                    ex.Message,
                    "Lỗi tải chi tiết",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

      

       

      
    }
}
