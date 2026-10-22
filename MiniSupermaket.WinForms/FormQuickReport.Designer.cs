
namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnRunReport;
     

        private Panel panelOrders;
        private Panel panelRevenue;
        private Panel panelCashiers;

        private Label lblTitleOrders;
        private Label lblTitleRevenue;
        private Label lblTitleCashiers;

        private Label lblTotalOrders;
        private Label lblTotalRevenue;
        private Label lblTotalCashiers;

        private Label lblTableTitle;
        private Label lblStatus;
        private DataGridView dgvCashierRevenue;

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
           

            panelOrders = new Panel();
            panelRevenue = new Panel();
            panelCashiers = new Panel();

            lblTitleOrders = new Label();
            lblTitleRevenue = new Label();
            lblTitleCashiers = new Label();

            lblTotalOrders = new Label();
            lblTotalRevenue = new Label();
            lblTotalCashiers = new Label();

            lblTableTitle = new Label();
            lblStatus = new Label();
            dgvCashierRevenue = new DataGridView();

            SuspendLayout();

            // FORM
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1050, 650);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormQuickReport";
            Text = "Báo cáo doanh thu";

            // HEADER
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 115;
            panelHeader.BackColor = Color.White;
            panelHeader.Padding = new Padding(25, 15, 25, 10);

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(35, 45, 60);
            lblTitle.Location = new Point(25, 15);
            lblTitle.Text = "BÁO CÁO DOANH THU";

            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.DimGray;
            lblSubtitle.Location = new Point(28, 58);
            lblSubtitle.Text = "Tổng hợp hóa đơn và doanh thu theo nhân viên";

            btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRunReport.Size = new Size(145, 36);
            btnRunReport.Location = new Point(680, 35);
            btnRunReport.Text = "TẢI BÁO CÁO";
            btnRunReport.Cursor = Cursors.Hand;
            btnRunReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);
            panelHeader.Controls.Add(btnRunReport);
           

            // CARD 1 - ORDERS
            panelOrders.Location = new Point(25, 135);
            panelOrders.Size = new Size(310, 120);
            panelOrders.BackColor = Color.FromArgb(52, 152, 219);

            lblTitleOrders.AutoSize = true;
            lblTitleOrders.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitleOrders.ForeColor = Color.White;
            lblTitleOrders.Location = new Point(18, 18);
            lblTitleOrders.Text = "TỔNG SỐ HÓA ĐƠN";

            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Location = new Point(18, 48);
            lblTotalOrders.Text = "0";

            panelOrders.Controls.Add(lblTitleOrders);
            panelOrders.Controls.Add(lblTotalOrders);

            // CARD 2 - REVENUE
            panelRevenue.Location = new Point(355, 135);
            panelRevenue.Size = new Size(310, 120);
            panelRevenue.BackColor = Color.FromArgb(39, 174, 96);

            lblTitleRevenue.AutoSize = true;
            lblTitleRevenue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitleRevenue.ForeColor = Color.White;
            lblTitleRevenue.Location = new Point(18, 18);
            lblTitleRevenue.Text = "TỔNG DOANH THU";

            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Location = new Point(18, 50);
            lblTotalRevenue.Text = "0 ₫";

            panelRevenue.Controls.Add(lblTitleRevenue);
            panelRevenue.Controls.Add(lblTotalRevenue);

            // CARD 3 - CASHIERS
            panelCashiers.Location = new Point(685, 135);
            panelCashiers.Size = new Size(310, 120);
            panelCashiers.BackColor = Color.FromArgb(230, 126, 34);

            lblTitleCashiers.AutoSize = true;
            lblTitleCashiers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitleCashiers.ForeColor = Color.White;
            lblTitleCashiers.Location = new Point(18, 18);
            lblTitleCashiers.Text = "SỐ NHÂN VIÊN CÓ DOANH THU";

            lblTotalCashiers.AutoSize = true;
            lblTotalCashiers.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblTotalCashiers.ForeColor = Color.White;
            lblTotalCashiers.Location = new Point(18, 48);
            lblTotalCashiers.Text = "0";

            panelCashiers.Controls.Add(lblTitleCashiers);
            panelCashiers.Controls.Add(lblTotalCashiers);

            // TABLE TITLE
            lblTableTitle.AutoSize = true;
            lblTableTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTableTitle.ForeColor = Color.FromArgb(35, 45, 60);
            lblTableTitle.Location = new Point(25, 275);
            lblTableTitle.Text = "CHI TIẾT DOANH THU THEO NHÂN VIÊN";

            // DATA GRID VIEW
            dgvCashierRevenue.Location = new Point(25, 315);
            dgvCashierRevenue.Size = new Size(970, 265);
            dgvCashierRevenue.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom |
                AnchorStyles.Left | AnchorStyles.Right;

            dgvCashierRevenue.AllowUserToAddRows = false;
            dgvCashierRevenue.AllowUserToDeleteRows = false;
            dgvCashierRevenue.ReadOnly = true;
            dgvCashierRevenue.MultiSelect = false;
            dgvCashierRevenue.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCashierRevenue.AutoGenerateColumns = false;
            dgvCashierRevenue.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvCashierRevenue.RowHeadersVisible = false;
            dgvCashierRevenue.BackgroundColor = Color.White;
            dgvCashierRevenue.BorderStyle = BorderStyle.None;
            dgvCashierRevenue.GridColor = Color.FromArgb(225, 230, 235);
            dgvCashierRevenue.Font = new Font("Segoe UI", 10F);
            dgvCashierRevenue.ColumnHeadersHeight = 42;
            dgvCashierRevenue.RowTemplate.Height = 36;
            dgvCashierRevenue.EnableHeadersVisualStyles = false;

            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(235, 240, 246);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(35, 45, 60);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCashierRevenue.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            dgvCashierRevenue.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 250);
            dgvCashierRevenue.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(35, 45, 60);
            dgvCashierRevenue.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

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
                HeaderText = "Số hóa đơn",
                FillWeight = 20
            });

            dgvCashierRevenue.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRevenue",
                HeaderText = "Tổng doanh thu (VNĐ)",
                FillWeight = 35
            });

            // STATUS
            lblStatus.AutoSize = false;
            lblStatus.Location = new Point(25, 590);
            lblStatus.Size = new Size(970, 35);
            lblStatus.Anchor =
                AnchorStyles.Bottom | AnchorStyles.Left |
                AnchorStyles.Right;
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Text = "Nhấn TẢI BÁO CÁO để lấy dữ liệu từ máy chủ.";

            // ADD CONTROLS
            Controls.Add(panelHeader);
            Controls.Add(panelOrders);
            Controls.Add(panelRevenue);
            Controls.Add(panelCashiers);
            Controls.Add(lblTableTitle);
            Controls.Add(dgvCashierRevenue);
            Controls.Add(lblStatus);

            ResumeLayout(false);
        }
    }
}
