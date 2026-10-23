
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private List<CashierRevenueItem> _reportData = new();

        public FormQuickReport()
        {
            InitializeComponent();

            Load += FormQuickReport_Load;
            btnRunReport.Click += btnRunReport_Click;
          
        }

        private async void FormQuickReport_Load(object? sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async void btnRunReport_Click(object? sender, EventArgs e)
        {
            await LoadReportAsync();
        }

        private async Task LoadReportAsync()
        {
            btnRunReport.Enabled = false;
            

            lblStatus.Text = "Đang tải báo cáo từ máy chủ...";
            lblStatus.ForeColor = System.Drawing.Color.DimGray;

            try
            {
                var response =
                    await ApiClientService.GetAsync<RevenueReportResponse>(
                        "reports/revenue-by-cashier");

                if (response == null || !response.Success)
                {
                    throw new InvalidOperationException(
                        response?.Message ?? "API không trả về báo cáo hợp lệ.");
                }

                _reportData = response.Data ?? new List<CashierRevenueItem>();

                // Loại bỏ dòng không có tên nhân viên.
                // Những đơn hàng cũ không gắn nhân viên sẽ không được tính
                // vào bảng doanh thu theo nhân viên.
                var validCashiers = _reportData
                    .Where(x => !string.IsNullOrWhiteSpace(x.CashierUsername))
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToList();

                dgvCashierRevenue.Rows.Clear();

                int index = 1;

                foreach (var cashier in validCashiers)
                {
                    dgvCashierRevenue.Rows.Add(
                        index++,
                        cashier.CashierUsername,
                        cashier.TotalOrders.ToString("N0"),
                        cashier.TotalRevenue.ToString("N0"));
                }

                // Tổng số hóa đơn và doanh thu được tính từ các nhân viên
                // có tên hợp lệ trong báo cáo.
                int totalOrders = validCashiers.Sum(x => x.TotalOrders);
                decimal totalRevenue = validCashiers.Sum(x => x.TotalRevenue);
                int totalCashiers = validCashiers.Count;

                lblTotalOrders.Text = totalOrders.ToString("N0");
                lblTotalRevenue.Text = totalRevenue.ToString("N0") + " ₫";
                lblTotalCashiers.Text = totalCashiers.ToString("N0");

                lblStatus.Text =
                    $"Tải thành công lúc {DateTime.Now:HH:mm:ss}. " +
                    $"Có {totalCashiers} nhân viên trong báo cáo.";

                lblStatus.ForeColor = System.Drawing.Color.FromArgb(39, 140, 80);

            }
            catch (HttpRequestException ex)
            {
                lblStatus.Text = "Không thể kết nối API.";
                lblStatus.ForeColor = System.Drawing.Color.Red;

                MessageBox.Show(
                    "Không thể kết nối API.\n" +
                    "Hãy kiểm tra API có đang chạy và địa chỉ BaseAddress " +
                    "trong ApiClientService có đúng không.\n\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Tải báo cáo thất bại.";
                lblStatus.ForeColor = System.Drawing.Color.Red;

                MessageBox.Show(
                    "Không thể tải báo cáo.\n\n" + ex.Message,
                    "Lỗi báo cáo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = true;
            }
        }

        
        private static string EscapeCsv(string? value)
        {
            value ??= string.Empty;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }

    public class RevenueReportResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("totalCashiers")]
        public int TotalCashiers { get; set; }

        [JsonPropertyName("data")]
        public List<CashierRevenueItem> Data { get; set; } = new();
    }

    public class CashierRevenueItem
    {
        [JsonPropertyName("cashierUsername")]
        public string CashierUsername { get; set; } = string.Empty;

        [JsonPropertyName("totalOrders")]
        public int TotalOrders { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }
}
