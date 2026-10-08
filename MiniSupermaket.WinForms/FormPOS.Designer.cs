namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel panelLeft;
        private Panel panelRight;

        private Label lblBarcode;
        private TextBox txtBarcode;

        private DataGridView dgvCart;

        private Label lblCustomerPhone;
        private TextBox txtCustomerPhone;

        private Label lblCustomerNameTitle;
        private Label lblCustomerName;

        private Label lblTotalTitle;
        private Label lblTotalAmount;

        private Label lblCashReceived;
        private TextBox txtCashReceived;

        private Label lblChangeTitle;
        private Label lblChange;

        private Button btnCheckout;
        private Button btnClearCart;

        private Label lblCartTitle;
        private Label lblPaymentTitle;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            panelLeft = new Panel();
            dgvCart = new DataGridView();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblCartTitle = new Label();

            panelRight = new Panel();
            btnClearCart = new Button();
            btnCheckout = new Button();

            lblChange = new Label();
            lblChangeTitle = new Label();

            txtCashReceived = new TextBox();
            lblCashReceived = new Label();

            lblTotalAmount = new Label();
            lblTotalTitle = new Label();

            lblCustomerName = new Label();
            lblCustomerNameTitle = new Label();

            txtCustomerPhone = new TextBox();
            lblCustomerPhone = new Label();

            lblPaymentTitle = new Label();

            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panelRight.SuspendLayout();
            SuspendLayout();

            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.White;
            panelLeft.Controls.Add(dgvCart);
            panelLeft.Controls.Add(txtBarcode);
            panelLeft.Controls.Add(lblBarcode);
            panelLeft.Controls.Add(lblCartTitle);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(20);
            panelLeft.Size = new Size(680, 660);
            panelLeft.TabIndex = 0;

            // 
            // lblCartTitle
            // 
            lblCartTitle.AutoSize = true;
            lblCartTitle.Font = new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblCartTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblCartTitle.Location = new Point(20, 20);
            lblCartTitle.Name = "lblCartTitle";
            lblCartTitle.Size = new Size(190, 28);
            lblCartTitle.TabIndex = 0;
            lblCartTitle.Text = "🛒 GIỎ HÀNG";

            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblBarcode.ForeColor = Color.FromArgb(60, 60, 60);
            lblBarcode.Location = new Point(23, 68);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(109, 19);
            lblBarcode.TabIndex = 1;
            lblBarcode.Text = "Mã vạch sản phẩm:";

            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font(
                "Segoe UI",
                12F,
                FontStyle.Regular,
                GraphicsUnit.Point);
            txtBarcode.Location = new Point(20, 93);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Quét hoặc nhập mã vạch rồi nhấn Enter...";
            txtBarcode.Size = new Size(635, 29);
            txtBarcode.TabIndex = 2;

            // Sự kiện quét mã vạch
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AllowUserToResizeRows = false;
            dgvCart.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvCart.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.None;

            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.FixedSingle;

            dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(41, 100, 180),
                Font = new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold,
                    GraphicsUnit.Point),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(41, 100, 180),
                SelectionForeColor = Color.White
            };

            dgvCart.ColumnHeadersHeight = 40;
            dgvCart.EnableHeadersVisualStyles = false;

            dgvCart.Location = new Point(20, 140);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersVisible = false;

            dgvCart.RowTemplate.Height = 35;

            dgvCart.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCart.Size = new Size(635, 490);
            dgvCart.TabIndex = 3;

            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(248, 249, 251);
            panelRight.Controls.Add(btnClearCart);
            panelRight.Controls.Add(btnCheckout);

            panelRight.Controls.Add(lblChange);
            panelRight.Controls.Add(lblChangeTitle);

            panelRight.Controls.Add(txtCashReceived);
            panelRight.Controls.Add(lblCashReceived);

            panelRight.Controls.Add(lblTotalAmount);
            panelRight.Controls.Add(lblTotalTitle);

            panelRight.Controls.Add(lblCustomerName);
            panelRight.Controls.Add(lblCustomerNameTitle);

            panelRight.Controls.Add(txtCustomerPhone);
            panelRight.Controls.Add(lblCustomerPhone);

            panelRight.Controls.Add(lblPaymentTitle);

            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(680, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(25);
            panelRight.Size = new Size(370, 660);
            panelRight.TabIndex = 1;

            // 
            // lblPaymentTitle
            // 
            lblPaymentTitle.AutoSize = true;
            lblPaymentTitle.Font = new Font(
                "Segoe UI",
                15F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblPaymentTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblPaymentTitle.Location = new Point(25, 20);
            lblPaymentTitle.Name = "lblPaymentTitle";
            lblPaymentTitle.Size = new Size(225, 28);
            lblPaymentTitle.TabIndex = 0;
            lblPaymentTitle.Text = "💳 THANH TOÁN";

            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblCustomerPhone.ForeColor = Color.FromArgb(60, 60, 60);
            lblCustomerPhone.Location = new Point(25, 75);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new Size(154, 19);
            lblCustomerPhone.TabIndex = 1;
            lblCustomerPhone.Text = "SĐT khách hàng:";

            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Regular,
                GraphicsUnit.Point);
            txtCustomerPhone.Location = new Point(25, 101);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập số điện thoại...";
            txtCustomerPhone.Size = new Size(315, 27);
            txtCustomerPhone.TabIndex = 2;

            // 
            // lblCustomerNameTitle
            // 
            lblCustomerNameTitle.AutoSize = true;
            lblCustomerNameTitle.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblCustomerNameTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblCustomerNameTitle.Location = new Point(25, 145);
            lblCustomerNameTitle.Name = "lblCustomerNameTitle";
            lblCustomerNameTitle.Size = new Size(119, 19);
            lblCustomerNameTitle.TabIndex = 3;
            lblCustomerNameTitle.Text = "Khách hàng:";

            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoEllipsis = true;
            lblCustomerName.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Regular,
                GraphicsUnit.Point);
            lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            lblCustomerName.Location = new Point(25, 171);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(315, 45);
            lblCustomerName.TabIndex = 4;
            lblCustomerName.Text = "Khách vãng lai";

            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblTotalTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblTotalTitle.Location = new Point(25, 235);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(145, 20);
            lblTotalTitle.TabIndex = 5;
            lblTotalTitle.Text = "TỔNG THANH TOÁN:";

            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            lblTotalAmount.Font = new Font(
                "Segoe UI",
                24F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblTotalAmount.ForeColor = Color.FromArgb(220, 53, 69);
            lblTotalAmount.Location = new Point(25, 260);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(315, 50);
            lblTotalAmount.TabIndex = 6;
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign =
                ContentAlignment.MiddleRight;

            // 
            // lblCashReceived
            // 
            lblCashReceived.AutoSize = true;
            lblCashReceived.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblCashReceived.ForeColor = Color.FromArgb(60, 60, 60);
            lblCashReceived.Location = new Point(25, 330);
            lblCashReceived.Name = "lblCashReceived";
            lblCashReceived.Size = new Size(110, 19);
            lblCashReceived.TabIndex = 7;
            lblCashReceived.Text = "Tiền khách đưa:";

            // 
            // txtCashReceived
            // 
            txtCashReceived.Font = new Font(
                "Segoe UI",
                12F,
                FontStyle.Regular,
                GraphicsUnit.Point);
            txtCashReceived.Location = new Point(25, 356);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.PlaceholderText = "Nhập số tiền...";
            txtCashReceived.Size = new Size(315, 29);
            txtCashReceived.TabIndex = 8;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;

            // 
            // lblChangeTitle
            // 
            lblChangeTitle.AutoSize = true;
            lblChangeTitle.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblChangeTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblChangeTitle.Location = new Point(25, 405);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(90, 19);
            lblChangeTitle.TabIndex = 9;
            lblChangeTitle.Text = "Tiền thừa:";

            // 
            // lblChange
            // 
            lblChange.Font = new Font(
                "Segoe UI",
                14F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            lblChange.ForeColor = Color.FromArgb(25, 135, 84);
            lblChange.Location = new Point(25, 430);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(315, 35);
            lblChange.TabIndex = 10;
            lblChange.Text = "0 đ";
            lblChange.TextAlign =
                ContentAlignment.MiddleRight;

            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(25, 135, 84);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font(
                "Segoe UI",
                11F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(25, 490);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(315, 55);
            btnCheckout.TabIndex = 11;
            btnCheckout.Text = "💵  THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;

            // Sự kiện thanh toán
            btnCheckout.Click += btnCheckout_Click;

            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(220, 53, 69);
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold,
                GraphicsUnit.Point);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(25, 560);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(315, 45);
            btnClearCart.TabIndex = 12;
            btnClearCart.Text = "🗑  HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;

            // Sự kiện hủy giỏ hàng
            btnClearCart.Click += btnClearCart_Click;

            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);

            Controls.Add(panelRight);
            Controls.Add(panelLeft);

            Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Regular,
                GraphicsUnit.Point);

            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPOS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng POS";

            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();

            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();

            ResumeLayout(false);
        }

        #endregion
    }
}