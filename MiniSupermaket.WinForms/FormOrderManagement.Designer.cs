
namespace MiniSupermarket.WinForms
{
    partial class FormOrderManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.DataGridView dgvDetails;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new System.Windows.Forms.Label();
            btnRefresh = new System.Windows.Forms.Button();
            dgvOrders = new System.Windows.Forms.DataGridView();
            lblStatus = new System.Windows.Forms.Label();
            lblDetailsTitle = new System.Windows.Forms.Label();
            dgvDetails = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 16F,
                System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(250, 30);
            lblTitle.Text = "QUẢN LÝ ĐƠN HÀNG";

            // btnRefresh
            btnRefresh.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Right;
            btnRefresh.Location = new System.Drawing.Point(1030, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(130, 36);
            btnRefresh.Text = "Làm mới";
            btnRefresh.UseVisualStyleBackColor = true;

            // dgvOrders
            dgvOrders.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            dgvOrders.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrders.Location = new System.Drawing.Point(20, 70);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.Size = new System.Drawing.Size(1140, 300);
            dgvOrders.TabIndex = 2;
            dgvOrders.ReadOnly = true;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // lblStatus
            lblStatus.AutoSize = false;
            lblStatus.Location = new System.Drawing.Point(20, 378);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(1140, 28);
            lblStatus.Text = "Sẵn sàng.";
            lblStatus.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            // lblDetailsTitle
            lblDetailsTitle.AutoSize = true;
            lblDetailsTitle.Font = new System.Drawing.Font(
                "Segoe UI", 12F,
                System.Drawing.FontStyle.Bold);
            lblDetailsTitle.Location = new System.Drawing.Point(20, 414);
            lblDetailsTitle.Name = "lblDetailsTitle";
            lblDetailsTitle.Size = new System.Drawing.Size(180, 25);
            lblDetailsTitle.Text = "CHI TIẾT ĐƠN HÀNG";

            // dgvDetails
            dgvDetails.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;
            dgvDetails.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetails.Location = new System.Drawing.Point(20, 445);
            dgvDetails.Name = "dgvDetails";
            dgvDetails.Size = new System.Drawing.Size(1140, 230);
            dgvDetails.TabIndex = 5;
            dgvDetails.ReadOnly = true;
            dgvDetails.AllowUserToAddRows = false;
            dgvDetails.AllowUserToDeleteRows = false;
            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvDetails.MultiSelect = false;
            dgvDetails.RowHeadersVisible = false;
            dgvDetails.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // FormOrderManagement
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1180, 700);
            Controls.Add(dgvDetails);
            Controls.Add(lblDetailsTitle);
            Controls.Add(lblStatus);
            Controls.Add(dgvOrders);
            Controls.Add(btnRefresh);
            Controls.Add(lblTitle);
            MinimumSize = new System.Drawing.Size(950, 600);
            Name = "FormOrderManagement";
            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý đơn hàng";

            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
