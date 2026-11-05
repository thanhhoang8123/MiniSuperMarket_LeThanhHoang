namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
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
            panelBrand = new Panel();
            lblFeature4 = new Label();
            lblFeature3 = new Label();
            lblFeature2 = new Label();
            lblFeature1 = new Label();
            lblBrandSubtitle = new Label();
            lblBrandTitle = new Label();
            lblBrandIcon = new Label();
            panelContent = new Panel();
            btnLogin = new Button();
            txtPass = new TextBox();
            lblPass = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            lblWelcomeSub = new Label();
            lblWelcome = new Label();
            panelBrand.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();
            // 
            // panelBrand
            // 
            panelBrand.BackColor = Color.FromArgb(15, 23, 42);
            panelBrand.Controls.Add(lblFeature4);
            panelBrand.Controls.Add(lblFeature3);
            panelBrand.Controls.Add(lblFeature2);
            panelBrand.Controls.Add(lblFeature1);
            panelBrand.Controls.Add(lblBrandSubtitle);
            panelBrand.Controls.Add(lblBrandTitle);
            panelBrand.Controls.Add(lblBrandIcon);
            panelBrand.Dock = DockStyle.Left;
            panelBrand.Location = new Point(0, 0);
            panelBrand.Name = "panelBrand";
            panelBrand.Size = new Size(300, 460);
            panelBrand.TabIndex = 0;
            // 
            // lblBrandIcon
            // 
            lblBrandIcon.AutoSize = true;
            lblBrandIcon.Font = new Font("Segoe UI", 40F);
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.Location = new Point(20, 35);
            lblBrandIcon.Name = "lblBrandIcon";
            lblBrandIcon.Size = new Size(111, 89);
            lblBrandIcon.TabIndex = 0;
            lblBrandIcon.Text = "🏪";
            // 
            // lblBrandTitle
            // 
            lblBrandTitle.AutoSize = true;
            lblBrandTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBrandTitle.ForeColor = Color.White;
            lblBrandTitle.Location = new Point(22, 130);
            lblBrandTitle.Name = "lblBrandTitle";
            lblBrandTitle.Size = new Size(260, 41);
            lblBrandTitle.TabIndex = 1;
            lblBrandTitle.Text = "TẠP HÓA PHỐ THỊ";
            // 
            // lblBrandSubtitle
            // 
            lblBrandSubtitle.Font = new Font("Segoe UI", 10F);
            lblBrandSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblBrandSubtitle.Location = new Point(25, 175);
            lblBrandSubtitle.Name = "lblBrandSubtitle";
            lblBrandSubtitle.Size = new Size(255, 30);
            lblBrandSubtitle.TabIndex = 2;
            lblBrandSubtitle.Text = "Cửa hàng Tiện lợi Thông minh";
            // 
            // lblFeature1
            // 
            lblFeature1.AutoSize = true;
            lblFeature1.Font = new Font("Segoe UI", 9.5F);
            lblFeature1.ForeColor = Color.FromArgb(203, 213, 225);
            lblFeature1.Location = new Point(25, 245);
            lblFeature1.Name = "lblFeature1";
            lblFeature1.Size = new Size(250, 21);
            lblFeature1.TabIndex = 3;
            lblFeature1.Text = "✓   Bán hàng & thu ngân POS tiện lợi";
            // 
            // lblFeature2
            // 
            lblFeature2.AutoSize = true;
            lblFeature2.Font = new Font("Segoe UI", 9.5F);
            lblFeature2.ForeColor = Color.FromArgb(203, 213, 225);
            lblFeature2.Location = new Point(25, 285);
            lblFeature2.Name = "lblFeature2";
            lblFeature2.Size = new Size(245, 21);
            lblFeature2.TabIndex = 4;
            lblFeature2.Text = "✓   Kiểm soát tồn kho & danh mục";
            // 
            // lblFeature3
            // 
            lblFeature3.AutoSize = true;
            lblFeature3.Font = new Font("Segoe UI", 9.5F);
            lblFeature3.ForeColor = Color.FromArgb(203, 213, 225);
            lblFeature3.Location = new Point(25, 325);
            lblFeature3.Name = "lblFeature3";
            lblFeature3.Size = new Size(240, 21);
            lblFeature3.TabIndex = 5;
            lblFeature3.Text = "✓   Tích điểm & tri ân khách hàng";
            // 
            // lblFeature4
            // 
            lblFeature4.AutoSize = true;
            lblFeature4.Font = new Font("Segoe UI", 9.5F);
            lblFeature4.ForeColor = Color.FromArgb(203, 213, 225);
            lblFeature4.Location = new Point(25, 365);
            lblFeature4.Name = "lblFeature4";
            lblFeature4.Size = new Size(240, 21);
            lblFeature4.TabIndex = 6;
            lblFeature4.Text = "✓   Báo cáo doanh thu & biểu đồ KPI";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.White;
            panelContent.Controls.Add(btnLogin);
            panelContent.Controls.Add(txtPass);
            panelContent.Controls.Add(lblPass);
            panelContent.Controls.Add(txtUser);
            panelContent.Controls.Add(lblUser);
            panelContent.Controls.Add(lblWelcomeSub);
            panelContent.Controls.Add(lblWelcome);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(300, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(450, 460);
            panelContent.TabIndex = 1;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(15, 23, 42);
            lblWelcome.Location = new Point(40, 45);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(224, 46);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "ĐĂNG NHẬP";
            // 
            // lblWelcomeSub
            // 
            lblWelcomeSub.AutoSize = true;
            lblWelcomeSub.Font = new Font("Segoe UI", 9.5F);
            lblWelcomeSub.ForeColor = Color.FromArgb(100, 116, 139);
            lblWelcomeSub.Location = new Point(44, 95);
            lblWelcomeSub.Name = "lblWelcomeSub";
            lblWelcomeSub.Size = new Size(286, 21);
            lblWelcomeSub.TabIndex = 1;
            lblWelcomeSub.Text = "Vui lòng nhập tài khoản để vào hệ thống";
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(51, 65, 85);
            lblUser.Location = new Point(44, 150);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(100, 21);
            lblUser.TabIndex = 2;
            lblUser.Text = "TÀI KHOẢN:";
            // 
            // txtUser
            // 
            txtUser.Font = new Font("Segoe UI", 11F);
            txtUser.Location = new Point(44, 178);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Nhập tên tài khoản...";
            txtUser.Size = new Size(360, 32);
            txtUser.TabIndex = 3;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(51, 65, 85);
            lblPass.Location = new Point(44, 235);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(95, 21);
            lblPass.TabIndex = 4;
            lblPass.Text = "MẬT KHẨU:";
            // 
            // txtPass
            // 
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.Location = new Point(44, 263);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Nhập mật khẩu...";
            txtPass.Size = new Size(360, 32);
            txtPass.TabIndex = 5;
            txtPass.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(44, 340);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(360, 46);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "ĐĂNG NHẬP VÀO HỆ THỐNG";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(750, 460);
            Controls.Add(panelContent);
            Controls.Add(panelBrand);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Tạp hóa Phố Thị";
            Load += FormLogin_Load;
            panelBrand.ResumeLayout(false);
            panelBrand.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBrand;
        private Label lblBrandIcon;
        private Label lblBrandTitle;
        private Label lblBrandSubtitle;
        private Label lblFeature1;
        private Label lblFeature2;
        private Label lblFeature3;
        private Label lblFeature4;

        private Panel panelContent;
        private Label lblWelcome;
        private Label lblWelcomeSub;
        private Label lblUser;
        private TextBox txtUser;
        private Label lblPass;
        private TextBox txtPass;
        private Button btnLogin;
    }
}
