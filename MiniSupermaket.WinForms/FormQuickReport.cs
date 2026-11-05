using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MiniSupermarket.WinForms.DTOs;
using MiniSupermarket.WinForms.Services;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private List<CashierRevenueItem> _reportData = new();
        private List<TopProductDto> _topProducts = new();
        private readonly ComboBox cboRevenueGrouping = new();

        public FormQuickReport()
        {
            InitializeComponent();

            SetupRevenueGrouping();
            this.Resize += (s, e) =>
            {
                ResizeKpiCards();
                LayoutReportHeaderActions();
            };
            panelHeader.Resize += (s, e) => LayoutReportHeaderActions();
            LayoutReportHeaderActions();

            Load += FormQuickReport_Load;
            btnRunReport.Click += btnRunReport_Click;
            btnFilterChart.Click += btnFilterChart_Click;
            btnExportReport.Click += btnExportReport_Click;
        }

        private void SetupRevenueGrouping()
        {
            cboRevenueGrouping.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRevenueGrouping.Font = new Font("Segoe UI", 9F);
            cboRevenueGrouping.Items.AddRange(new object[] { "Theo ngày", "Theo tuần", "Theo tháng" });
            cboRevenueGrouping.SelectedIndex = 0;
            cboRevenueGrouping.Location = new Point(660, 9);
            cboRevenueGrouping.Size = new Size(130, 28);

            var lblGrouping = new Label
            {
                AutoSize = true,
                Text = "Gộp theo:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(590, 14)
            };
            panelChartFilter.Controls.Add(lblGrouping);
            panelChartFilter.Controls.Add(cboRevenueGrouping);
        }
        private async void FormQuickReport_Load(object? sender, EventArgs e)
        {
            ConfigureTopProductsGrid();
            await LoadAllDataAsync();
        }

        private void ConfigureTopProductsGrid()
        {
            if (dgvTopProducts.Columns["colQuantitySold"] != null)
            {
                dgvTopProducts.Columns["colQuantitySold"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTopProducts.Columns["colQuantitySold"]!.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                dgvTopProducts.Columns["colQuantitySold"]!.DefaultCellStyle.ForeColor = Color.FromArgb(41, 100, 180);
            }

            if (dgvTopProducts.Columns["colTotalRevenue"] != null)
            {
                dgvTopProducts.Columns["colTotalRevenue"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTopProducts.Columns["colTotalRevenue"]!.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                dgvTopProducts.Columns["colTotalRevenue"]!.DefaultCellStyle.ForeColor = Color.FromArgb(39, 140, 80);
            }
        }

        private async void btnRunReport_Click(object? sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        private async void btnFilterChart_Click(object? sender, EventArgs e)
        {
            btnFilterChart.Enabled = false;
            try
            {
                await LoadChartRevenueAsync();
            }
            finally
            {
                btnFilterChart.Enabled = true;
            }
        }

        // =========================================================
        // NẠP TOÀN BỘ DỮ LIỆU BÁO CÁO (DASHBOARD + BIỂU ĐỒ + TOP 5 + THU NGÂN)
        // =========================================================
        private async Task LoadAllDataAsync()
        {
            btnRunReport.Enabled = false;
            lblStatus.Text = "Đang tải dữ liệu tổng quan, biểu đồ và báo cáo từ máy chủ...";
            lblStatus.ForeColor = Color.DimGray;

            try
            {
                // Tải song song; riêng lỗi KPI phải được đưa ra giao diện thay vì bị bỏ qua.
                var dashboardTask = LoadDashboardKpiAsync();
                await Task.WhenAll(
                    dashboardTask,
                    LoadChartRevenueAsync(),
                    LoadTopSellingProductsAsync(),
                    LoadCashierRevenueAsync());

                var dashboardError = await dashboardTask;
                if (dashboardError == null)
                {
                    lblStatus.Text = $"Cập nhật toàn bộ báo cáo thành công lúc {DateTime.Now:HH:mm:ss}.";
                    lblStatus.ForeColor = Color.FromArgb(39, 140, 80);
                }
                else
                {
                    lblStatus.Text = dashboardError;
                    lblStatus.ForeColor = Color.Red;
                }
            }
            catch (HttpRequestException ex)
            {
                lblStatus.Text = "Không thể kết nối API.";
                lblStatus.ForeColor = Color.Red;

                MessageBox.Show(
                    "Không thể kết nối máy chủ API.\n" +
                    "Hãy kiểm tra xem API có đang khởi chạy hay không.\n\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Tải dữ liệu thất bại: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                btnRunReport.Enabled = true;
            }
        }

        // =========================================================
        // 1. TẢI 4 THẺ CHỈ SỐ TỔNG QUAN (KPI SUMMARY)
        // =========================================================
        private async Task<string?> LoadDashboardKpiAsync()
        {
            SetKpiValues("…");
            try
            {
                var response = await ApiClientService.GetAsync<DashboardSummaryResponse>("reports/dashboard-summary");
                if (response?.Success == true && response.Data != null)
                {
                    var data = response.Data;
                    lblTodayRevenue.Text = data.TodayRevenue.ToString("N0") + " ₫";
                    lblTotalOrders.Text = data.TotalOrders.ToString("N0");
                    lblNewCustomers.Text = data.NewCustomers.ToString("N0");
                    lblLowStock.Text = data.LowStockProducts.ToString("N0");
                    return null;
                }

                SetKpiValues("—");
                return string.IsNullOrWhiteSpace(response?.Message)
                    ? "Không nhận được dữ liệu KPI từ máy chủ. Hãy kiểm tra API và cơ sở dữ liệu."
                    : "Không tải được KPI: " + response.Message;
            }
            catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
            {
                SetKpiValues("—");
                return $"API không tải được KPI (HTTP {(int)ex.StatusCode.Value}). Hãy kiểm tra log máy chủ và migration cơ sở dữ liệu.";
            }
            catch (HttpRequestException)
            {
                SetKpiValues("—");
                return "Không tải được KPI do không kết nối được máy chủ API.";
            }
            catch (Exception ex)
            {
                SetKpiValues("—");
                if (ex.Message.Contains("401", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("403", StringComparison.OrdinalIgnoreCase))
                {
                    return ex.Message;
                }
                return "Không tải được KPI. Hãy kiểm tra quyền Admin, cấu hình API và tình trạng cơ sở dữ liệu.";
            }
        }

        private void SetKpiValues(string value)
        {
            lblTodayRevenue.Text = value == "…" ? value : value + (value == "—" ? string.Empty : " ₫");
            lblTotalOrders.Text = value;
            lblNewCustomers.Text = value;
            lblLowStock.Text = value;
        }

        // =========================================================
        // 2. VẼ BIỂU ĐỒ CỘT DOANH THU THEO KHOẢNG THỜI GIAN
        // Gom nhóm theo ngày qua Stored Procedure / API
        // =========================================================
        private async Task LoadChartRevenueAsync()
        {
            try
            {
                if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Khoảng ngày không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var fromDate = dtpFromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var toDate = dtpToDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var response = await ApiClientService.GetAsync<DailyRevenueResponse>(
                    $"reports/daily-revenue?fromDate={fromDate}&toDate={toDate}");

                var dailyTotals = new Dictionary<DateTime, (decimal Revenue, int Orders)>();
                if (response?.Success == true && response.Data != null)
                {
                    foreach (var item in response.Data)
                    {
                        if (!DateTime.TryParseExact(item.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var day))
                            continue;
                        if (dailyTotals.TryGetValue(day.Date, out var current))
                            dailyTotals[day.Date] = (current.Revenue + item.TotalRevenue, current.Orders + item.TotalOrders);
                        else
                            dailyTotals[day.Date] = (item.TotalRevenue, item.TotalOrders);
                    }
                }

                var periodTotals = new SortedDictionary<DateTime, (decimal Revenue, int Orders)>();
                for (var day = dtpFromDate.Value.Date; day <= dtpToDate.Value.Date; day = day.AddDays(1))
                {
                    var key = cboRevenueGrouping.SelectedIndex switch
                    {
                        1 => day.AddDays(-(((int)day.DayOfWeek + 6) % 7)),
                        2 => new DateTime(day.Year, day.Month, 1),
                        _ => day
                    };
                    var daily = dailyTotals.TryGetValue(day, out var found) ? found : (0m, 0);
                    if (periodTotals.TryGetValue(key, out var current))
                        periodTotals[key] = (current.Revenue + daily.Item1, current.Orders + daily.Item2);
                    else
                        periodTotals[key] = daily;
                }

                var series = chartRevenue.Series["DoanhThu"];
                series.Points.Clear();
                series.IsXValueIndexed = true;
                series["PointWidth"] = "0.62";

                var groupingName = cboRevenueGrouping.SelectedItem?.ToString() ?? "Theo ngày";
                chartRevenue.Titles[0].Text = $"BIỂU ĐỒ DOANH THU {groupingName.ToUpperInvariant()}";
                int labelInterval = Math.Max(1, (int)Math.Ceiling(periodTotals.Count / 12.0));
                var chartArea = chartRevenue.ChartAreas["RevenueArea"];
                chartArea.AxisX.Title = cboRevenueGrouping.SelectedIndex switch
                {
                    1 => "Tuần bắt đầu từ ngày",
                    2 => "Tháng ghi nhận",
                    _ => "Ngày ghi nhận"
                };
                chartArea.AxisX.Interval = labelInterval;
                chartArea.AxisX.LabelStyle.Angle = periodTotals.Count > 8 ? -45 : 0;
                chartArea.AxisX.IsLabelAutoFit = false;
                series.IsValueShownAsLabel = periodTotals.Count <= 12;

                foreach (var period in periodTotals)
                {
                    string label = cboRevenueGrouping.SelectedIndex switch
                    {
                        1 => $"Tuần từ {period.Key:dd/MM}",
                        2 => period.Key.ToString("MM/yyyy", CultureInfo.InvariantCulture),
                        _ => period.Key.ToString("dd/MM", CultureInfo.InvariantCulture)
                    };
                    int pointIndex = series.Points.AddY((double)period.Value.Revenue);
                    var point = series.Points[pointIndex];
                    point.AxisLabel = label;
                    point.ToolTip = string.Join(Environment.NewLine, new[] { label, $"Doanh thu: {period.Value.Revenue:N0} ₫", $"Số đơn hàng: {period.Value.Orders}" });
                    point.Label = period.Value.Revenue.ToString("N0", CultureInfo.CurrentCulture);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Không thể vẽ biểu đồ doanh thu: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
        }
        // =========================================================
        // 3. THỐNG KÊ TOP 5 SẢN PHẨM BÁN CHẠY NHẤT (BEST-SELLERS)
        // LINQ: GroupBy(od => od.ProductId) -> OrderByDescending -> Take(5)
        // =========================================================
        private async Task LoadTopSellingProductsAsync()
        {
            try
            {
                var response = await ApiClientService.GetAsync<TopProductsResponse>("reports/top-selling-products?limit=5");

                dgvTopProducts.Rows.Clear();

                if (response?.Success == true && response.Data != null && response.Data.Count > 0)
                {
                    _topProducts = response.Data;
                    int rankIndex = 1;

                    foreach (var item in _topProducts)
                    {
                        string rankDisplay = rankIndex switch
                        {
                            1 => "🥇 Top 1",
                            2 => "🥈 Top 2",
                            3 => "🥉 Top 3",
                            _ => $"    {rankIndex}"
                        };

                        dgvTopProducts.Rows.Add(
                            rankDisplay,
                            item.Barcode,
                            item.ProductName,
                            item.TotalQuantitySold.ToString("N0"),
                            item.TotalRevenue.ToString("N0") + " ₫");

                        rankIndex++;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Không thể tải top sản phẩm: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
        }

        // =========================================================
        // 4. TẢI BẢNG DOANH THU THEO NHÂN VIÊN (STORED PROCEDURE)
        // =========================================================
        private async Task LoadCashierRevenueAsync()
        {
            var response = await ApiClientService.GetAsync<RevenueReportResponse>("reports/revenue-by-cashier");

            if (response == null || !response.Success)
            {
                throw new InvalidOperationException(
                    response?.Message ?? "API không trả về báo cáo doanh thu theo nhân viên.");
            }

            _reportData = response.Data ?? new List<CashierRevenueItem>();

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
        }
        // =========================================================
        // XUẤT BÁO CÁO DOANH THU & TOP BÁN CHẠY RA TỆP EXCEL / CSV
        // =========================================================
        private async void btnExportReport_Click(object? sender, EventArgs e)
        {
            try
            {
                btnExportReport.Enabled = false;

                using var saveDialog = new SaveFileDialog
                {
                    Filter = "Sổ làm việc Excel (*.xlsx)|*.xlsx|Tệp CSV (*.csv)|*.csv",
                    FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                    DefaultExt = "xlsx",
                    AddExtension = true,
                    Title = "Xuất báo cáo doanh thu"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var sb = new System.Text.StringBuilder();
                    var excelRows = new List<IReadOnlyList<object?>>();
                    string[] excelHeaders;
                    string sheetName;

                    if (tabReports.SelectedTab == tabChartPage)
                    {
                        sheetName = "Doanh thu theo ngày";
                        excelHeaders = new[] { "STT", "Ngày ghi nhận", "Doanh thu (VNĐ)" };
                        // Xuất doanh thu theo từng ngày từ biểu đồ
                        sb.AppendLine("BÁO CÁO DOANH THU THEO KHOẢNG THỜI GIAN");
                        sb.AppendLine($"Từ ngày,{dtpFromDate.Value:dd/MM/yyyy},Đến ngày,{dtpToDate.Value:dd/MM/yyyy}");
                        sb.AppendLine();
                        sb.AppendLine("STT,Ngày ghi nhận,Doanh thu (VNĐ)");

                        int idx = 1;
                        int pointIndex = 0;
                        foreach (var point in chartRevenue.Series["DoanhThu"].Points)
                        {
                            string periodLabel = cboRevenueGrouping.SelectedIndex == 0
                                ? dtpFromDate.Value.Date.AddDays(pointIndex).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                                : point.AxisLabel;
                            excelRows.Add(new object?[] { idx, periodLabel, point.YValues[0] });
                            sb.AppendLine($"{idx++},{EscapeCsv(periodLabel)},{point.YValues[0]}");
                            pointIndex++;
                        }
                    }
                    else if (tabReports.SelectedTab == tabTopProductsPage)
                    {
                        sheetName = "Sản phẩm bán chạy";
                        excelHeaders = new[] { "Thứ hạng", "Mã vạch", "Tên mặt hàng", "Số lượng đã bán", "Tổng doanh thu (VNĐ)" };
                        // Xuất bảng xếp hạng Top 5 sản phẩm bán chạy nhất
                        sb.AppendLine("BẢNG XẾP HẠNG TOP SẢN PHẨM BÁN CHẠY NHẤT");
                        sb.AppendLine();
                        sb.AppendLine("Thứ hạng,Mã vạch,Tên mặt hàng,Số lượng đã bán,Tổng doanh thu (VNĐ)");

                        int rank = 1;
                        foreach (var prod in _topProducts)
                        {
                            excelRows.Add(new object?[] { $"Top {rank}", prod.Barcode, prod.ProductName, prod.TotalQuantitySold, prod.TotalRevenue });
                            sb.AppendLine($"Top {rank++},{EscapeCsv(prod.Barcode)},{EscapeCsv(prod.ProductName)},{prod.TotalQuantitySold},{prod.TotalRevenue}");
                        }
                    }
                    else
                    {
                        sheetName = "Doanh thu theo thu ngân";
                        excelHeaders = new[] { "STT", "Tên nhân viên", "Số hóa đơn đã xuất", "Tổng doanh thu (VNĐ)" };
                        // Xuất doanh thu chi tiết theo nhân viên thu ngân
                        sb.AppendLine("BÁO CÁO CHI TIẾT DOANH THU THEO NHÂN VIÊN THU NGÂN");
                        sb.AppendLine();
                        sb.AppendLine("STT,Tên nhân viên,Số hóa đơn đã xuất,Tổng doanh thu (VNĐ)");

                        int idx = 1;
                        foreach (var item in _reportData)
                        {
                            excelRows.Add(new object?[] { idx, item.CashierUsername, item.TotalOrders, item.TotalRevenue });
                            sb.AppendLine($"{idx++},{EscapeCsv(item.CashierUsername)},{item.TotalOrders},{item.TotalRevenue}");
                        }
                    }

                    // Lưu file với UTF-8 BOM để Excel đọc đúng tiếng Việt
                    if (System.IO.Path.GetExtension(saveDialog.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        string? reportDetails = tabReports.SelectedTab == tabChartPage
                            ? $"Kỳ báo cáo: {dtpFromDate.Value:dd/MM/yyyy} – {dtpToDate.Value:dd/MM/yyyy}   |   Gộp theo: {cboRevenueGrouping.SelectedItem}"
                            : null;
                        SpreadsheetExportService.WriteXlsx(saveDialog.FileName, sheetName, excelHeaders, excelRows, reportDetails);
                    }
                    else
                        await System.IO.File.WriteAllTextAsync(saveDialog.FileName, sb.ToString(), new System.Text.UTF8Encoding(true));

                    var choice = MessageBox.Show(
                        $"Xuất báo cáo Excel thành công!\nĐường dẫn: {saveDialog.FileName}\n\nBạn có muốn mở tệp ngay bằng Excel không?",
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
                    "Đã xảy ra sự cố khi xuất báo cáo Excel:\n" + ex.Message,
                    "Lỗi xuất file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnExportReport.Enabled = true;
            }
        }

        private static string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void LayoutReportHeaderActions()
        {
            if (panelHeader.ClientSize.Width <= 0) return;

            const int rightMargin = 18;
            const int buttonGap = 10;
            int runButtonX = Math.Max(
                18,
                panelHeader.ClientSize.Width - rightMargin - btnRunReport.Width);

            btnRunReport.Location = new Point(runButtonX, 22);
            btnExportReport.Location = new Point(
                Math.Max(18, runButtonX - buttonGap - btnExportReport.Width),
                22);
        }
        private void ResizeKpiCards()
        {
            if (ClientSize.Width <= 0) return;
            int margin = 25;
            int gap = 16;
            int totalWidth = ClientSize.Width - margin * 2;
            int cardWidth = Math.Max(180, (totalWidth - gap * 3) / 4);

            panelTodayRevenue.Location = new Point(margin, 95);
            panelTodayRevenue.Size = new Size(cardWidth, 90);

            panelOrders.Location = new Point(margin + (cardWidth + gap), 95);
            panelOrders.Size = new Size(cardWidth, 90);

            panelNewCustomers.Location = new Point(margin + (cardWidth + gap) * 2, 95);
            panelNewCustomers.Size = new Size(cardWidth, 90);

            panelLowStock.Location = new Point(margin + (cardWidth + gap) * 3, 95);
            panelLowStock.Size = new Size(cardWidth, 90);
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
