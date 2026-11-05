namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            panelNav = new Panel();
            btnUserManage = new Button();
            btnReports = new Button();
            btnOrders = new Button();
            btnCustomer = new Button();
            btnProduct = new Button();
            btnCategory = new Button();
            btnPOS = new Button();
            panelLogo = new Panel();
            lblLogoSub = new Label();
            lblLogo = new Label();
            panelUserFooter = new Panel();
            btnLogout = new Button();
            panelTopHeader = new Panel();
            lblUserInfo = new Label();
            lblTitle = new Label();
            panelMainContent = new Panel();

            panelSidebar.SuspendLayout();
            panelNav.SuspendLayout();
            panelLogo.SuspendLayout();
            panelUserFooter.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();

            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(15, 23, 42);
            panelSidebar.Controls.Add(panelNav);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Controls.Add(panelLogo);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(240, 680);
            panelSidebar.TabIndex = 0;

            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(9, 13, 22);
            panelLogo.Controls.Add(lblLogoSub);
            panelLogo.Controls.Add(lblLogo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(240, 70);
            panelLogo.TabIndex = 0;

            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(16, 12);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(205, 28);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🏪 TẠP HÓA PHỐ THỊ";

            // 
            // lblLogoSub
            // 
            lblLogoSub.AutoSize = true;
            lblLogoSub.Font = new Font("Segoe UI", 8.25F);
            lblLogoSub.ForeColor = Color.FromArgb(148, 163, 184);
            lblLogoSub.Location = new Point(20, 42);
            lblLogoSub.Name = "lblLogoSub";
            lblLogoSub.Size = new Size(180, 19);
            lblLogoSub.TabIndex = 1;
            lblLogoSub.Text = "Cửa hàng Tiện lợi Thông minh";

            // 
            // panelNav
            // 
            panelNav.AutoScroll = true;
            panelNav.BackColor = Color.FromArgb(15, 23, 42);
            panelNav.Controls.Add(btnUserManage);
            panelNav.Controls.Add(btnReports);
            panelNav.Controls.Add(btnOrders);
            panelNav.Controls.Add(btnCustomer);
            panelNav.Controls.Add(btnProduct);
            panelNav.Controls.Add(btnCategory);
            panelNav.Controls.Add(btnPOS);
            panelNav.Dock = DockStyle.Fill;
            panelNav.Location = new Point(0, 70);
            panelNav.Name = "panelNav";
            panelNav.Size = new Size(240, 550);
            panelNav.TabIndex = 1;

            // 
            // btnUserManage
            // 
            btnUserManage.BackColor = Color.FromArgb(15, 23, 42);
            btnUserManage.Cursor = Cursors.Hand;
            btnUserManage.Dock = DockStyle.Top;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("Segoe UI", 10F);
            btnUserManage.ForeColor = Color.FromArgb(203, 213, 225);
            btnUserManage.Location = new Point(0, 288);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(18, 0, 0, 0);
            btnUserManage.Size = new Size(240, 48);
            btnUserManage.TabIndex = 6;
            btnUserManage.Text = "⚙️   Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = false;
            btnUserManage.Click += btnUserManage_Click;

            // 
            // btnReports
            // 
            btnReports.BackColor = Color.FromArgb(15, 23, 42);
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 10F);
            btnReports.ForeColor = Color.FromArgb(203, 213, 225);
            btnReports.Location = new Point(0, 240);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(18, 0, 0, 0);
            btnReports.Size = new Size(240, 48);
            btnReports.TabIndex = 5;
            btnReports.Text = "📊   Báo cáo & KPI";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;
            btnReports.Click += btnReports_Click;

            // 
            // btnOrders
            // 
            btnOrders.BackColor = Color.FromArgb(15, 23, 42);
            btnOrders.Cursor = Cursors.Hand;
            btnOrders.Dock = DockStyle.Top;
            btnOrders.FlatAppearance.BorderSize = 0;
            btnOrders.FlatStyle = FlatStyle.Flat;
            btnOrders.Font = new Font("Segoe UI", 10F);
            btnOrders.ForeColor = Color.FromArgb(203, 213, 225);
            btnOrders.Location = new Point(0, 192);
            btnOrders.Name = "btnOrders";
            btnOrders.Padding = new Padding(18, 0, 0, 0);
            btnOrders.Size = new Size(240, 48);
            btnOrders.TabIndex = 4;
            btnOrders.Text = "🧾   Đơn hàng";
            btnOrders.TextAlign = ContentAlignment.MiddleLeft;
            btnOrders.UseVisualStyleBackColor = false;
            btnOrders.Click += btnOrders_Click;

            // 
            // btnCustomer
            // 
            btnCustomer.BackColor = Color.FromArgb(15, 23, 42);
            btnCustomer.Cursor = Cursors.Hand;
            btnCustomer.Dock = DockStyle.Top;
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10F);
            btnCustomer.ForeColor = Color.FromArgb(203, 213, 225);
            btnCustomer.Location = new Point(0, 144);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Padding = new Padding(18, 0, 0, 0);
            btnCustomer.Size = new Size(240, 48);
            btnCustomer.TabIndex = 3;
            btnCustomer.Text = "👥   Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;

            // 
            // btnProduct
            // 
            btnProduct.BackColor = Color.FromArgb(15, 23, 42);
            btnProduct.Cursor = Cursors.Hand;
            btnProduct.Dock = DockStyle.Top;
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10F);
            btnProduct.ForeColor = Color.FromArgb(203, 213, 225);
            btnProduct.Location = new Point(0, 96);
            btnProduct.Name = "btnProduct";
            btnProduct.Padding = new Padding(18, 0, 0, 0);
            btnProduct.Size = new Size(240, 48);
            btnProduct.TabIndex = 2;
            btnProduct.Text = "📦   Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = false;
            btnProduct.Click += btnProduct_Click;

            // 
            // btnCategory
            // 
            btnCategory.BackColor = Color.FromArgb(15, 23, 42);
            btnCategory.Cursor = Cursors.Hand;
            btnCategory.Dock = DockStyle.Top;
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10F);
            btnCategory.ForeColor = Color.FromArgb(203, 213, 225);
            btnCategory.Location = new Point(0, 48);
            btnCategory.Name = "btnCategory";
            btnCategory.Padding = new Padding(18, 0, 0, 0);
            btnCategory.Size = new Size(240, 48);
            btnCategory.TabIndex = 1;
            btnCategory.Text = "📁   Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;

            // 
            // btnPOS
            // 
            btnPOS.BackColor = Color.FromArgb(15, 23, 42);
            btnPOS.Cursor = Cursors.Hand;
            btnPOS.Dock = DockStyle.Top;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10F);
            btnPOS.ForeColor = Color.FromArgb(203, 213, 225);
            btnPOS.Location = new Point(0, 0);
            btnPOS.Name = "btnPOS";
            btnPOS.Padding = new Padding(18, 0, 0, 0);
            btnPOS.Size = new Size(240, 48);
            btnPOS.TabIndex = 0;
            btnPOS.Text = "🛒   Bán hàng POS";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnPOS_Click;

            // 
            // panelUserFooter
            // 
            panelUserFooter.BackColor = Color.FromArgb(9, 13, 22);
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 620);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(240, 60);
            panelUserFooter.TabIndex = 2;

            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(9, 13, 22);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(248, 113, 113);
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(18, 0, 0, 0);
            btnLogout.Size = new Size(240, 60);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪   Đăng xuất";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;

            // 
            // panelTopHeader
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(240, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1040, 65);
            panelTopHeader.TabIndex = 1;

            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.BackColor = Color.FromArgb(239, 246, 255);
            lblUserInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.FromArgb(29, 78, 216);
            lblUserInfo.Location = new Point(710, 18);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Padding = new Padding(12, 6, 12, 6);
            lblUserInfo.Size = new Size(240, 33);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "👤 Nhân viên: ... | Vai trò: [...]";
            lblUserInfo.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitle.Location = new Point(25, 17);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(310, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TỔNG QUAN HỆ THỐNG";

            // 
            // panelMainContent
            // 
            panelMainContent.BackColor = Color.FromArgb(241, 245, 249);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(240, 65);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1040, 615);
            panelMainContent.TabIndex = 2;

            // 
            // FormMainShell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 680);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1100, 640);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phần mềm Quản lý Cửa hàng Tiện lợi - Tạp hóa Phố Thị";
            Load += FormMainShell_Load;

            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            panelNav.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            panelUserFooter.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // ==========================
        // PANELS
        // ==========================

        private Panel panelSidebar;
        private Panel panelNav;
        private Panel panelLogo;
        private Panel panelTopHeader;
        private Panel panelMainContent;
        private Panel panelUserFooter;

        // ==========================
        // SIDEBAR
        // ==========================

        private Label lblLogo;
        private Label lblLogoSub;

        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnOrders;

        private Button btnReports;
        private Button btnUserManage;
        private Button btnLogout;

        // ==========================
        // HEADER
        // ==========================

        private Label lblTitle;
        private Label lblUserInfo;
    }
}
