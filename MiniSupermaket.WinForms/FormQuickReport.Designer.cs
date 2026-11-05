using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnRunReport;
        private Button btnExportReport;

        // 4 Thẻ chỉ số tổng quan (KPI Summary Cards)
        private Panel panelTodayRevenue;
        private Label lblTitleTodayRevenue;
        private Label lblTodayRevenue;

        private Panel panelOrders;
        private Label lblTitleOrders;
        private Label lblTotalOrders;

        private Panel panelNewCustomers;
        private Label lblTitleNewCustomers;
        private Label lblNewCustomers;

        private Panel panelLowStock;
        private Label lblTitleLowStock;
        private Label lblLowStock;

        // Tab điều hướng: Biểu đồ cột, Top 5 bán chạy & Doanh thu nhân viên
        private TabControl tabReports;
        private TabPage tabChartPage;
        private TabPage tabTopProductsPage;
        private TabPage tabCashierPage;

        // Controls trong Tab Biểu đồ Doanh thu (Chart)
        private Panel panelChartFilter;
        private Label lblFromDate;
        private DateTimePicker dtpFromDate;
        private Label lblToDate;
        private DateTimePicker dtpToDate;
        private Button btnFilterChart;
        private Chart chartRevenue;

        // Controls trong Tab Top 5 Sản phẩm Bán chạy
        private Label lblTopProductsTitle;
        private DataGridView dgvTopProducts;

        // Controls trong Tab Doanh thu theo Nhân viên
        private Label lblTableTitle;
        private DataGridView dgvCashierRevenue;

        private Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            panelHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnRunReport = new Button();
            btnExportReport = new Button();

            panelTodayRevenue = new Panel();
            lblTitleTodayRevenue = new Label();
            lblTodayRevenue = new Label();

            panelOrders = new Panel();
            lblTitleOrders = new Label();
            lblTotalOrders = new Label();

            panelNewCustomers = new Panel();
            lblTitleNewCustomers = new Label();
            lblNewCustomers = new Label();

            panelLowStock = new Panel();
            lblTitleLowStock = new Label();
            lblLowStock = new Label();

            tabReports = new TabControl();
            tabChartPage = new TabPage();
            tabTopProductsPage = new TabPage();
            tabCashierPage = new TabPage();

            panelChartFilter = new Panel();
            lblFromDate = new Label();
            dtpFromDate = new DateTimePicker();
            lblToDate = new Label();
            dtpToDate = new DateTimePicker();
            btnFilterChart = new Button();
            chartRevenue = new Chart();

            lblTopProductsTitle = new Label();
            dgvTopProducts = new DataGridView();

            lblTableTitle = new Label();
            dgvCashierRevenue = new DataGridView();

            lblStatus = new Label();

            panelHeader.SuspendLayout();
            panelTodayRevenue.SuspendLayout();
            panelOrders.SuspendLayout();
            panelNewCustomers.SuspendLayout();
            panelLowStock.SuspendLayout();
            tabReports.SuspendLayout();
            tabChartPage.SuspendLayout();
            panelChartFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartRevenue).BeginInit();
            tabTopProductsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).BeginInit();
            tabCashierPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCashierRevenue).BeginInit();
            SuspendLayout();

            // FORM
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1050, 680);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormQuickReport";
            Text = "Dashboard Thống kê & Báo cáo";

            // HEADER
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = Color.White;
            panelHeader.Padding = new Padding(25, 10, 25, 10);

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 45, 60);
            lblTitle.Location = new Point(25, 10);
            lblTitle.Text = "DASHBOARD & BÁO CÁO DOANH THU";

            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.DimGray;
            lblSubtitle.Location = new Point(28, 45);
            lblSubtitle.Text = "Chỉ số kinh doanh tổng quan, biểu đồ trực quan và doanh thu chi tiết";

            btnRunReport.BackColor = Color.FromArgb(37, 99, 235);
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRunReport.Size = new Size(140, 34);
            btnRunReport.Location = new Point(880, 22);
            btnRunReport.Text = "🔄 LÀM MỚI";
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            btnExportReport.BackColor = Color.FromArgb(5, 150, 105);
            btnExportReport.FlatStyle = FlatStyle.Flat;
            btnExportReport.FlatAppearance.BorderSize = 0;
            btnExportReport.ForeColor = Color.White;
            btnExportReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnExportReport.Size = new Size(150, 34);
            btnExportReport.Location = new Point(715, 22);
            btnExportReport.Text = "📥 XUẤT EXCEL";
            btnExportReport.Cursor = Cursors.Hand;
            btnExportReport.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(btnExportReport);
            panelHeader.Controls.Add(btnRunReport);

            // ========================================================
            // 4 THẺ CHỈ SỐ TỔNG QUAN (KPI SUMMARY CARDS)
            // ========================================================

            // CARD 1: DOANH THU HÔM NAY
            panelTodayRevenue.Location = new Point(25, 95);
            panelTodayRevenue.Size = new Size(232, 95);
            panelTodayRevenue.BackColor = Color.FromArgb(5, 150, 105);

            lblTitleTodayRevenue.AutoSize = true;
            lblTitleTodayRevenue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleTodayRevenue.ForeColor = Color.White;
            lblTitleTodayRevenue.Location = new Point(14, 12);
            lblTitleTodayRevenue.Text = "💰 DOANH THU HÔM NAY";

            lblTodayRevenue.AutoSize = true;
            lblTodayRevenue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTodayRevenue.ForeColor = Color.White;
            lblTodayRevenue.Location = new Point(14, 38);
            lblTodayRevenue.Text = "0 ₫";

            panelTodayRevenue.Controls.Add(lblTitleTodayRevenue);
            panelTodayRevenue.Controls.Add(lblTodayRevenue);

            // CARD 2: TỔNG SỐ HÓA ĐƠN
            panelOrders.Location = new Point(281, 95);
            panelOrders.Size = new Size(232, 95);
            panelOrders.BackColor = Color.FromArgb(37, 99, 235);

            lblTitleOrders.AutoSize = true;
            lblTitleOrders.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleOrders.ForeColor = Color.White;
            lblTitleOrders.Location = new Point(14, 12);
            lblTitleOrders.Text = "🧾 TỔNG HÓA ĐƠN";

            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Location = new Point(14, 38);
            lblTotalOrders.Text = "0";

            panelOrders.Controls.Add(lblTitleOrders);
            panelOrders.Controls.Add(lblTotalOrders);

            // CARD 3: KHÁCH HÀNG MỚI HÔM NAY ĐĂNG KÝ
            panelNewCustomers.Location = new Point(537, 95);
            panelNewCustomers.Size = new Size(232, 95);
            panelNewCustomers.BackColor = Color.FromArgb(124, 58, 237);

            lblTitleNewCustomers.AutoSize = true;
            lblTitleNewCustomers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleNewCustomers.ForeColor = Color.White;
            lblTitleNewCustomers.Location = new Point(14, 12);
            lblTitleNewCustomers.Text = "👥 KHÁCH HÀNG MỚI HÔM NAY";

            lblNewCustomers.AutoSize = true;
            lblNewCustomers.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblNewCustomers.ForeColor = Color.White;
            lblNewCustomers.Location = new Point(14, 38);
            lblNewCustomers.Text = "0";

            panelNewCustomers.Controls.Add(lblTitleNewCustomers);
            panelNewCustomers.Controls.Add(lblNewCustomers);

            // CARD 4: SẮP HẾT KHO (<= 10)
            panelLowStock.Location = new Point(793, 95);
            panelLowStock.Size = new Size(232, 95);
            panelLowStock.BackColor = Color.FromArgb(220, 38, 38);

            lblTitleLowStock.AutoSize = true;
            lblTitleLowStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTitleLowStock.ForeColor = Color.White;
            lblTitleLowStock.Location = new Point(14, 12);
            lblTitleLowStock.Text = "⚠️ CẢNH BÁO TỒN KHO";

            lblLowStock.AutoSize = true;
            lblLowStock.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblLowStock.ForeColor = Color.White;
            lblLowStock.Location = new Point(14, 38);
            lblLowStock.Text = "0";

            panelLowStock.Controls.Add(lblTitleLowStock);
            panelLowStock.Controls.Add(lblLowStock);

            // ========================================================
            // TAB CONTROL: BIỂU ĐỒ, TOP 5 VÀ DOANH THU NHÂN VIÊN
            // ========================================================
            tabReports.Location = new Point(25, 205);
            tabReports.Size = new Size(1000, 435);
            tabReports.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabReports.Font = new Font("Segoe UI", 9.5F);
            tabReports.Controls.Add(tabChartPage);
            tabReports.Controls.Add(tabTopProductsPage);
            tabReports.Controls.Add(tabCashierPage);

            // --------------------------------------------------------
            // TAB 1: BIỂU ĐỒ CỘT DOANH THU THEO NGÀY
            // --------------------------------------------------------
            tabChartPage.Text = "📊 Biểu đồ Cột Doanh Thu Theo Ngày";
            tabChartPage.BackColor = Color.White;
            tabChartPage.Padding = new Padding(10);

            panelChartFilter.Dock = DockStyle.Top;
            panelChartFilter.Height = 45;
            panelChartFilter.BackColor = Color.FromArgb(248, 249, 250);

            lblFromDate.AutoSize = true;
            lblFromDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFromDate.ForeColor = Color.FromArgb(50, 50, 50);
            lblFromDate.Location = new Point(12, 13);
            lblFromDate.Text = "Từ ngày:";

            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Font = new Font("Segoe UI", 9.5F);
            dtpFromDate.Location = new Point(80, 10);
            dtpFromDate.Size = new Size(125, 26);
            dtpFromDate.Value = System.DateTime.Today.AddDays(-7);

            lblToDate.AutoSize = true;
            lblToDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblToDate.ForeColor = Color.FromArgb(50, 50, 50);
            lblToDate.Location = new Point(220, 13);
            lblToDate.Text = "Đến ngày:";

            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Font = new Font("Segoe UI", 9.5F);
            dtpToDate.Location = new Point(295, 10);
            dtpToDate.Size = new Size(125, 26);
            dtpToDate.Value = System.DateTime.Today;

            btnFilterChart.BackColor = Color.FromArgb(37, 99, 235);
            btnFilterChart.FlatStyle = FlatStyle.Flat;
            btnFilterChart.FlatAppearance.BorderSize = 0;
            btnFilterChart.ForeColor = Color.White;
            btnFilterChart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFilterChart.Location = new Point(435, 9);
            btnFilterChart.Size = new Size(140, 28);
            btnFilterChart.Text = "🔍 Xem Biểu Đồ";
            btnFilterChart.Cursor = Cursors.Hand;

            panelChartFilter.Controls.Add(lblFromDate);
            panelChartFilter.Controls.Add(dtpFromDate);
            panelChartFilter.Controls.Add(lblToDate);
            panelChartFilter.Controls.Add(dtpToDate);
            panelChartFilter.Controls.Add(btnFilterChart);

            chartRevenue.Dock = DockStyle.Fill;
            chartRevenue.BackColor = Color.White;

            ChartArea chartArea = new ChartArea("RevenueArea");
            chartArea.AxisX.Title = "Ngày ghi nhận";
            chartArea.AxisX.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(235, 235, 235);
            chartArea.AxisY.Title = "Doanh thu (VNĐ)";
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(235, 235, 235);
            chartArea.AxisY.LabelStyle.Format = "#,##0";
            chartRevenue.ChartAreas.Add(chartArea);

            Series series = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(41, 128, 185),
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                LabelForeColor = Color.FromArgb(35, 45, 60)
            };
            chartRevenue.Series.Add(series);

            Title chartTitle = new Title("BIỂU ĐỒ DOANH THU THEO TỪNG NGÀY", Docking.Top, new Font("Segoe UI", 11F, FontStyle.Bold), Color.FromArgb(35, 45, 60));
            chartRevenue.Titles.Add(chartTitle);

            tabChartPage.Controls.Add(chartRevenue);
            tabChartPage.Controls.Add(panelChartFilter);

            // --------------------------------------------------------
            // TAB 2: TOP 5 SẢN PHẨM BÁN CHẠY NHẤT (BEST-SELLERS)
            // --------------------------------------------------------
            tabTopProductsPage.Text = "🏆 Top 5 Sản Phẩm Bán Chạy";
            tabTopProductsPage.BackColor = Color.White;
            tabTopProductsPage.Padding = new Padding(10);

            lblTopProductsTitle.Dock = DockStyle.Top;
            lblTopProductsTitle.Height = 35;
            lblTopProductsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTopProductsTitle.ForeColor = Color.FromArgb(35, 45, 60);
            lblTopProductsTitle.Text = "TOP 5 SẢN PHẨM BÁN CHẠY NHẤT";
            lblTopProductsTitle.TextAlign = ContentAlignment.MiddleLeft;

            dgvTopProducts.Dock = DockStyle.Fill;
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.AllowUserToDeleteRows = false;
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.MultiSelect = false;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.AutoGenerateColumns = false;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.BackgroundColor = Color.White;
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.GridColor = Color.FromArgb(225, 230, 235);
            dgvTopProducts.Font = new Font("Segoe UI", 10F);
            dgvTopProducts.ColumnHeadersHeight = 38;
            dgvTopProducts.RowTemplate.Height = 36;
            dgvTopProducts.EnableHeadersVisualStyles = false;

            dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 246);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvTopProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250);
            dgvTopProducts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 45, 60);
            dgvTopProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRank",
                HeaderText = "Thứ hạng",
                FillWeight = 15
            });

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBarcode",
                HeaderText = "Mã vạch (Barcode)",
                FillWeight = 22
            });

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                HeaderText = "Tên mặt hàng",
                FillWeight = 40
            });

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantitySold",
                HeaderText = "Số lượng đã bán",
                FillWeight = 20
            });

            dgvTopProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalRevenue",
                HeaderText = "Tổng doanh thu (VNĐ)",
                FillWeight = 25
            });

            tabTopProductsPage.Controls.Add(dgvTopProducts);
            tabTopProductsPage.Controls.Add(lblTopProductsTitle);

            // --------------------------------------------------------
            // TAB 3: DOANH THU THEO NHÂN VIÊN (STORED PROCEDURE)
            // --------------------------------------------------------
            tabCashierPage.Text = "👥 Doanh Thu Theo Nhân Viên";
            tabCashierPage.BackColor = Color.White;
            tabCashierPage.Padding = new Padding(10);

            lblTableTitle.Dock = DockStyle.Top;
            lblTableTitle.Height = 35;
            lblTableTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(35, 45, 60);
            lblTableTitle.Text = "CHI TIẾT DOANH THU THEO THU NGÂN";
            lblTableTitle.TextAlign = ContentAlignment.MiddleLeft;

            dgvCashierRevenue.Dock = DockStyle.Fill;
            dgvCashierRevenue.AllowUserToAddRows = false;
            dgvCashierRevenue.AllowUserToDeleteRows = false;
            dgvCashierRevenue.ReadOnly = true;
            dgvCashierRevenue.MultiSelect = false;
            dgvCashierRevenue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCashierRevenue.AutoGenerateColumns = false;
            dgvCashierRevenue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCashierRevenue.RowHeadersVisible = false;
            dgvCashierRevenue.BackgroundColor = Color.White;
            dgvCashierRevenue.BorderStyle = BorderStyle.None;
            dgvCashierRevenue.GridColor = Color.FromArgb(225, 230, 235);
            dgvCashierRevenue.Font = new Font("Segoe UI", 10F);
            dgvCashierRevenue.ColumnHeadersHeight = 38;
            dgvCashierRevenue.RowTemplate.Height = 34;
            dgvCashierRevenue.EnableHeadersVisualStyles = false;

            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 240, 246);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvCashierRevenue.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250);
            dgvCashierRevenue.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 45, 60);
            dgvCashierRevenue.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            dgvCashierRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSTT",
                HeaderText = "STT",
                FillWeight = 12
            });

            dgvCashierRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCashier",
                HeaderText = "Tên nhân viên",
                FillWeight = 45
            });

            dgvCashierRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOrders",
                HeaderText = "Số hóa đơn đã xuất",
                FillWeight = 20
            });

            dgvCashierRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRevenue",
                HeaderText = "Tổng doanh thu (VNĐ)",
                FillWeight = 35
            });

            tabCashierPage.Controls.Add(dgvCashierRevenue);
            tabCashierPage.Controls.Add(lblTableTitle);

            // STATUS
            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 30;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Padding = new Padding(25, 0, 0, 0);
            lblStatus.Text = "Nhấn LÀM MỚI để cập nhật số liệu mới nhất từ máy chủ.";

            // ADD MAIN CONTROLS TO FORM
            Controls.Add(tabReports);
            Controls.Add(lblStatus);
            Controls.Add(panelTodayRevenue);
            Controls.Add(panelOrders);
            Controls.Add(panelNewCustomers);
            Controls.Add(panelLowStock);
            Controls.Add(panelHeader);

            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelTodayRevenue.ResumeLayout(false);
            panelTodayRevenue.PerformLayout();
            panelOrders.ResumeLayout(false);
            panelOrders.PerformLayout();
            panelNewCustomers.ResumeLayout(false);
            panelNewCustomers.PerformLayout();
            panelLowStock.ResumeLayout(false);
            panelLowStock.PerformLayout();
            tabReports.ResumeLayout(false);
            tabChartPage.ResumeLayout(false);
            panelChartFilter.ResumeLayout(false);
            panelChartFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartRevenue).EndInit();
            tabTopProductsPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopProducts).EndInit();
            tabCashierPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCashierRevenue).EndInit();
            ResumeLayout(false);
        }
    }
}


