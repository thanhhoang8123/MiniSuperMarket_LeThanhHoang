namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            grpCustomerInfo = new GroupBox();
            lblCustomerId = new Label();
            txtCustomerId = new TextBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblPhoneNumber = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblRewardPoints = new Label();
            txtRewardPoints = new TextBox();
            lblMembershipRank = new Label();
            txtMembershipRank = new TextBox();
            pnlActions = new Panel();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            lblKeyword = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            dgvCustomers = new DataGridView();
            grpCustomerInfo.SuspendLayout();
            pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblTitle.Location = new Point(350, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(366, 41);
            lblTitle.TabIndex = 1;
            lblTitle.Text = " QUẢN LÝ KHÁCH HÀNG";
            
            // 
            // grpCustomerInfo
            // 
            grpCustomerInfo.Controls.Add(lblMembershipRank);
            grpCustomerInfo.Controls.Add(txtMembershipRank);
            grpCustomerInfo.Controls.Add(txtRewardPoints);
            grpCustomerInfo.Controls.Add(lblRewardPoints);
            grpCustomerInfo.Controls.Add(txtAddress);
            grpCustomerInfo.Controls.Add(lblAddress);
            grpCustomerInfo.Controls.Add(txtPhoneNumber);
            grpCustomerInfo.Controls.Add(lblPhoneNumber);
            grpCustomerInfo.Controls.Add(txtCustomerName);
            grpCustomerInfo.Controls.Add(lblCustomerName);
            grpCustomerInfo.Controls.Add(txtCustomerId);
            grpCustomerInfo.Controls.Add(lblCustomerId);
            grpCustomerInfo.Location = new Point(25, 60);
            grpCustomerInfo.Name = "grpCustomerInfo";
            grpCustomerInfo.Size = new Size(950, 220);
            grpCustomerInfo.TabIndex = 2;
            grpCustomerInfo.TabStop = false;
            grpCustomerInfo.Text = " THÔNG TIN KHÁCH HÀNG";
            // 
            // lblCustomerId
            // 
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(20, 35);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(112, 20);
            lblCustomerId.TabIndex = 0;
            lblCustomerId.Text = "Mã khách hàng:";
            // 
            // txtCustomerId
            // 
            txtCustomerId.Location = new Point(150, 32);
            txtCustomerId.Name = "txtCustomerId";
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(280, 27);
            txtCustomerId.TabIndex = 1;
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(20, 80);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(114, 20);
            lblCustomerName.TabIndex = 2;
            lblCustomerName.Text = "Tên khách hàng:";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(150, 77);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(280, 27);
            txtCustomerName.TabIndex = 3;
            // 
            // lblPhoneNumber
            // 
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(20, 125);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(100, 20);
            lblPhoneNumber.TabIndex = 4;
            lblPhoneNumber.Text = "Số điện thoại:";
           
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(150, 122);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(280, 27);
            txtPhoneNumber.TabIndex = 5;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(500, 35);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(62, 20);
            lblAddress.TabIndex = 6;
            lblAddress.Text = " Địa chỉ:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(620, 32);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(280, 27);
            txtAddress.TabIndex = 7;
            // 
            // lblRewardPoints
            // 
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(500, 80);
            lblRewardPoints.Name = "lblRewardPoints";
            lblRewardPoints.Size = new Size(99, 20);
            lblRewardPoints.TabIndex = 8;
            lblRewardPoints.Text = "Điểm tích lũy:";
            // 
            // txtRewardPoints
            // 
            txtRewardPoints.Location = new Point(620, 77);
            txtRewardPoints.Name = "txtRewardPoints";
            txtRewardPoints.Size = new Size(280, 27);
            txtRewardPoints.TabIndex = 9;
            // 
            // lblMembershipRank
            // 
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(500, 125);
            lblMembershipRank.Name = "lblMembershipRank";
            lblMembershipRank.Size = new Size(120, 20);
            lblMembershipRank.TabIndex = 3;
            lblMembershipRank.Text = "Hạng thành viên:";
            
            // 
            // txtMembershipRank
            // 
            txtMembershipRank.Location = new Point(620, 122);
            txtMembershipRank.Name = "txtMembershipRank";
            txtMembershipRank.Size = new Size(280, 27);
            txtMembershipRank.TabIndex = 10;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnDelete);
            pnlActions.Controls.Add(btnUpdate);
            pnlActions.Controls.Add(btnAdd);
            pnlActions.Controls.Add(btnLoad);
            pnlActions.Location = new Point(25, 295);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(950, 55);
            pnlActions.TabIndex = 3;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(10, 10);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(130, 35);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "Tải danh sách";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(155, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(265, 10);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 35);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Sửa";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(375, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(25, 365);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(73, 20);
            lblKeyword.TabIndex = 4;
            lblKeyword.Text = "Tìm kiếm:";
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(110, 362);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(500, 27);
            txtKeyword.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(625, 360);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(120, 35);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgvCustomers
            // 
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(12, 401);
            dgvCustomers.MultiSelect = false;
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 51;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(950, 183);
            dgvCustomers.TabIndex = 11;
            dgvCustomers.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;
            // 
            // FormCustomerManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 603);
            Controls.Add(dgvCustomers);
            Controls.Add(btnSearch);
            Controls.Add(txtKeyword);
            Controls.Add(lblKeyword);
            Controls.Add(pnlActions);
            Controls.Add(grpCustomerInfo);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormCustomerManagement";
            Text = "QUẢN LÝ KHÁCH HÀNG";
            
            grpCustomerInfo.ResumeLayout(false);
            grpCustomerInfo.PerformLayout();
            pnlActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblTitle;
        private GroupBox grpCustomerInfo;
        private Label lblPhoneNumber;
        private TextBox txtCustomerName;
        private Label lblCustomerName;
        private TextBox txtCustomerId;
        private Label lblCustomerId;
        private TextBox txtRewardPoints;
        private Label lblRewardPoints;
        private TextBox txtAddress;
        private Label lblAddress;
        private TextBox txtPhoneNumber;
        private Label lblMembershipRank;
        private TextBox txtMembershipRank;
        private Panel pnlActions;
        private Button btnUpdate;
        private Button btnAdd;
        private Button btnLoad;
        private Button btnDelete;
        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private DataGridView dgvCustomers;
    }
}