namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTop;
        private Panel panelGrid;
        private Panel panelDetail;

        private Label lblTitle;
        private Label lblUserList;
        private Label lblDetail;

        private Label lblUsername;
        private Label lblFullName;
        private Label lblPassword;
        private Label lblRole;

        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtPassword;
        private ComboBox cboRole;

        private DataGridView dgvUsers;

        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnToggleLock;
        private Button btnClear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            panelTop = new Panel();
            panelGrid = new Panel();
            panelDetail = new Panel();

            lblTitle = new Label();
            lblUserList = new Label();
            lblDetail = new Label();

            lblUsername = new Label();
            lblFullName = new Label();
            lblPassword = new Label();
            lblRole = new Label();

            txtUsername = new TextBox();
            txtFullName = new TextBox();
            txtPassword = new TextBox();
            cboRole = new ComboBox();

            dgvUsers = new DataGridView();

            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();
            btnClear = new Button();


            // =========================
            // FORM
            // =========================

            SuspendLayout();

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1100, 650);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài khoản";

            // =========================
            // PANEL TOP
            // =========================

            panelTop.BackColor = Color.White;
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 75;

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font(
                "Segoe UI",
                18F,
                FontStyle.Bold);

            lblTitle.ForeColor = Color.FromArgb(35, 45, 65);

            lblTitle.Location = new Point(25, 20);
            lblTitle.Text = "QUẢN TRỊ TÀI KHOẢN & PHÂN QUYỀN";

            panelTop.Controls.Add(lblTitle);

            // =========================
            // PANEL GRID
            // =========================

            panelGrid.BackColor = Color.White;
            panelGrid.Location = new Point(20, 95);
            panelGrid.Size = new Size(700, 530);
            panelGrid.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left;

            lblUserList.AutoSize = true;
            lblUserList.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

            lblUserList.ForeColor =
                Color.FromArgb(35, 45, 65);

            lblUserList.Location = new Point(15, 15);
            lblUserList.Text = "DANH SÁCH NHÂN VIÊN";

            // =========================
            // DATAGRIDVIEW
            // =========================

            dgvUsers.Location = new Point(15, 50);
            dgvUsers.Size = new Size(670, 460);

            dgvUsers.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.ReadOnly = true;

            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsers.MultiSelect = false;

            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;

            dgvUsers.RowHeadersVisible = false;

            dgvUsers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvUsers.ColumnHeadersHeight = 35;

            dgvUsers.EnableHeadersVisualStyles = false;

            dgvUsers.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(41, 100, 180);

            dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            panelGrid.Controls.Add(lblUserList);
            panelGrid.Controls.Add(dgvUsers);

            // =========================
            // PANEL DETAIL
            // =========================

            panelDetail.BackColor = Color.White;
            panelDetail.Location = new Point(740, 95);
            panelDetail.Size = new Size(340, 530);

            panelDetail.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            lblDetail.AutoSize = true;
            lblDetail.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold);

            lblDetail.ForeColor =
                Color.FromArgb(35, 45, 65);

            lblDetail.Location = new Point(20, 15);
            lblDetail.Text = "THÔNG TIN TÀI KHOẢN";

            // =========================
            // USERNAME
            // =========================

            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 65);
            lblUsername.Text = "Tên đăng nhập";

            txtUsername.Location = new Point(20, 88);
            txtUsername.Size = new Size(295, 27);

            // =========================
            // FULL NAME
            // =========================

            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 130);
            lblFullName.Text = "Họ và tên";

            txtFullName.Location = new Point(20, 153);
            txtFullName.Size = new Size(295, 27);

            // =========================
            // PASSWORD
            // =========================

            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 195);
            lblPassword.Text = "Mật khẩu";

            txtPassword.Location = new Point(20, 218);
            txtPassword.Size = new Size(295, 27);

            txtPassword.UseSystemPasswordChar = true;

            // =========================
            // ROLE
            // =========================

            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 260);
            lblRole.Text = "Vai trò";

            cboRole.Location = new Point(20, 283);
            cboRole.Size = new Size(295, 28);

            cboRole.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboRole.Items.AddRange(new object[]
            {
                "Admin",
                "Cashier",
                "Warehouse"
            });

            cboRole.SelectedIndex = 1;

            // =========================
            // BUTTON ADD
            // =========================

            btnAddUser.Location = new Point(20, 330);
            btnAddUser.Size = new Size(295, 40);

            btnAddUser.Text = "THÊM TÀI KHOẢN";
            btnAddUser.BackColor =
                Color.FromArgb(41, 100, 180);

            btnAddUser.ForeColor = Color.White;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.FlatAppearance.BorderSize = 0;

            // =========================
            // BUTTON RESET PASSWORD
            // =========================

            btnResetPassword.Location =
                new Point(20, 380);

            btnResetPassword.Size =
                new Size(295, 40);

            btnResetPassword.Text =
                "ĐẶT LẠI MẬT KHẨU";

            btnResetPassword.BackColor =
                Color.FromArgb(230, 160, 50);

            btnResetPassword.ForeColor =
                Color.White;

            btnResetPassword.FlatStyle =
                FlatStyle.Flat;

            btnResetPassword.FlatAppearance.BorderSize = 0;

            // =========================
            // BUTTON LOCK
            // =========================

            btnToggleLock.Location =
                new Point(20, 430);

            btnToggleLock.Size =
                new Size(140, 40);

            btnToggleLock.Text =
                "KHÓA / MỞ KHÓA";

            btnToggleLock.BackColor =
                Color.FromArgb(220, 80, 80);

            btnToggleLock.ForeColor =
                Color.White;

            btnToggleLock.FlatStyle =
                FlatStyle.Flat;

            btnToggleLock.FlatAppearance.BorderSize = 0;

            // =========================
            // BUTTON CLEAR
            // =========================

            btnClear.Location =
                new Point(175, 430);

            btnClear.Size =
                new Size(140, 40);

            btnClear.Text = "LÀM MỚI";

            btnClear.BackColor =
                Color.FromArgb(100, 110, 120);

            btnClear.ForeColor =
                Color.White;

            btnClear.FlatStyle =
                FlatStyle.Flat;

            btnClear.FlatAppearance.BorderSize = 0;

            // =========================
            // ADD CONTROLS
            // =========================

            panelDetail.Controls.Add(lblDetail);

            panelDetail.Controls.Add(lblUsername);
            panelDetail.Controls.Add(txtUsername);

            panelDetail.Controls.Add(lblFullName);
            panelDetail.Controls.Add(txtFullName);

            panelDetail.Controls.Add(lblPassword);
            panelDetail.Controls.Add(txtPassword);

            panelDetail.Controls.Add(lblRole);
            panelDetail.Controls.Add(cboRole);

            panelDetail.Controls.Add(btnAddUser);
            panelDetail.Controls.Add(btnResetPassword);
            panelDetail.Controls.Add(btnToggleLock);
            panelDetail.Controls.Add(btnClear);

            Controls.Add(panelTop);
            Controls.Add(panelGrid);
            Controls.Add(panelDetail);

            ResumeLayout(false);
        }
    }
}