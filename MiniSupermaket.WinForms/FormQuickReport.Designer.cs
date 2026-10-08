namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;

        private Panel panelOrders;
        private Panel panelRevenue;
        private Panel panelBestSeller;

        private Label lblTitleOrders;
        private Label lblTitleRevenue;
        private Label lblTitleBestSeller;

        private Label lblTotalOrders;
        private Label lblTotalRevenue;
        private Label lblBestSeller;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.panelHeader = new Panel();
            this.lblTitle = new Label();
            this.lblDate = new Label();
            this.dtpReportDate = new DateTimePicker();
            this.btnRunReport = new Button();

            this.panelOrders = new Panel();
            this.panelRevenue = new Panel();
            this.panelBestSeller = new Panel();

            this.lblTitleOrders = new Label();
            this.lblTitleRevenue = new Label();
            this.lblTitleBestSeller = new Label();

            this.lblTotalOrders = new Label();
            this.lblTotalRevenue = new Label();
            this.lblBestSeller = new Label();

            // =========================
            // FORM
            // =========================
            this.SuspendLayout();

            this.ClientSize = new Size(1050, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FormQuickReport";
            this.Text = "Báo cáo doanh thu";

            // =========================
            // PANEL HEADER
            // =========================
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 110;
            this.panelHeader.BackColor = Color.White;

            // TITLE
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font(
                "Segoe UI",
                20F,
                FontStyle.Bold);

            this.lblTitle.Location = new Point(30, 20);
            this.lblTitle.Text = "BÁO CÁO DOANH THU";

            // DATE LABEL
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new Font(
                "Segoe UI",
                10F);

            this.lblDate.Location = new Point(32, 70);
            this.lblDate.Text = "Ngày báo cáo:";

            // DATE PICKER
            this.dtpReportDate.Format =
                DateTimePickerFormat.Short;

            this.dtpReportDate.Location =
                new Point(135, 67);

            this.dtpReportDate.Size =
                new Size(130, 27);

            // BUTTON
            this.btnRunReport.BackColor =
                Color.FromArgb(41, 100, 180);

            this.btnRunReport.FlatStyle =
                FlatStyle.Flat;

            this.btnRunReport.FlatAppearance.BorderSize = 0;

            this.btnRunReport.ForeColor =
                Color.White;

            this.btnRunReport.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            this.btnRunReport.Location =
                new Point(285, 65);

            this.btnRunReport.Size =
                new Size(150, 32);

            this.btnRunReport.Text =
                "XEM BÁO CÁO";

            this.btnRunReport.Cursor =
                Cursors.Hand;

            // ADD HEADER CONTROLS
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.lblDate);
            this.panelHeader.Controls.Add(this.dtpReportDate);
            this.panelHeader.Controls.Add(this.btnRunReport);

            // =========================
            // CARD 1 - ORDERS
            // =========================
            this.panelOrders.Location =
                new Point(30, 145);

            this.panelOrders.Size =
                new Size(300, 180);

            this.panelOrders.BackColor =
                Color.FromArgb(52, 152, 219);

            this.lblTitleOrders.AutoSize = true;

            this.lblTitleOrders.Font =
                new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold);

            this.lblTitleOrders.ForeColor =
                Color.White;

            this.lblTitleOrders.Location =
                new Point(20, 20);

            this.lblTitleOrders.Text =
                "TỔNG SỐ HÓA ĐƠN";

            this.lblTotalOrders.AutoSize = true;

            this.lblTotalOrders.Font =
                new Font(
                    "Segoe UI",
                    32F,
                    FontStyle.Bold);

            this.lblTotalOrders.ForeColor =
                Color.White;

            this.lblTotalOrders.Location =
                new Point(20, 65);

            this.lblTotalOrders.Text = "0";

            this.panelOrders.Controls.Add(
                this.lblTitleOrders);

            this.panelOrders.Controls.Add(
                this.lblTotalOrders);

            // =========================
            // CARD 2 - REVENUE
            // =========================
            this.panelRevenue.Location =
                new Point(360, 145);

            this.panelRevenue.Size =
                new Size(300, 180);

            this.panelRevenue.BackColor =
                Color.FromArgb(39, 174, 96);

            this.lblTitleRevenue.AutoSize = true;

            this.lblTitleRevenue.Font =
                new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold);

            this.lblTitleRevenue.ForeColor =
                Color.White;

            this.lblTitleRevenue.Location =
                new Point(20, 20);

            this.lblTitleRevenue.Text =
                "TỔNG DOANH THU";

            this.lblTotalRevenue.AutoSize = true;

            this.lblTotalRevenue.Font =
                new Font(
                    "Segoe UI",
                    24F,
                    FontStyle.Bold);

            this.lblTotalRevenue.ForeColor =
                Color.White;

            this.lblTotalRevenue.Location =
                new Point(20, 70);

            this.lblTotalRevenue.Text =
                "0 ₫";

            this.panelRevenue.Controls.Add(
                this.lblTitleRevenue);

            this.panelRevenue.Controls.Add(
                this.lblTotalRevenue);

            // =========================
            // CARD 3 - BEST SELLER
            // =========================
            this.panelBestSeller.Location =
                new Point(690, 145);

            this.panelBestSeller.Size =
                new Size(300, 180);

            this.panelBestSeller.BackColor =
                Color.FromArgb(230, 126, 34);

            this.lblTitleBestSeller.AutoSize = true;

            this.lblTitleBestSeller.Font =
                new Font(
                    "Segoe UI",
                    12F,
                    FontStyle.Bold);

            this.lblTitleBestSeller.ForeColor =
                Color.White;

            this.lblTitleBestSeller.Location =
                new Point(20, 20);

            this.lblTitleBestSeller.Text =
                "MẶT HÀNG BÁN CHẠY NHẤT";

            this.lblBestSeller.AutoSize = false;

            this.lblBestSeller.Font =
                new Font(
                    "Segoe UI",
                    17F,
                    FontStyle.Bold);

            this.lblBestSeller.ForeColor =
                Color.White;

            this.lblBestSeller.Location =
                new Point(20, 70);

            this.lblBestSeller.Size =
                new Size(260, 70);

            this.lblBestSeller.Text =
                "---";

            this.lblBestSeller.TextAlign =
                ContentAlignment.MiddleLeft;

            this.panelBestSeller.Controls.Add(
                this.lblTitleBestSeller);

            this.panelBestSeller.Controls.Add(
                this.lblBestSeller);

            // =========================
            // ADD TO FORM
            // =========================
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.panelRevenue);
            this.Controls.Add(this.panelBestSeller);

            this.ResumeLayout(false);
        }
    }
}