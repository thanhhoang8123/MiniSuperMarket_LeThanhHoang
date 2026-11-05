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

        // Đổi Điểm Thưởng Trừ Tiền Hóa Đơn
        private Label lblCustomerPoints;
        private Panel panelRedeem;
        private CheckBox chkUseRewardPoints;
        private Label lblRedeemPrompt;
        private NumericUpDown nudPointsToRedeem;
        private Label lblDiscountValue;

        private Label lblSubTotalTitle;
        private Label lblSubTotal;

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

            lblSubTotal = new Label();
            lblSubTotalTitle = new Label();

            panelRedeem = new Panel();
            lblDiscountValue = new Label();
            nudPointsToRedeem = new NumericUpDown();
            lblRedeemPrompt = new Label();
            chkUseRewardPoints = new CheckBox();

            lblCustomerPoints = new Label();
            lblCustomerName = new Label();
            lblCustomerNameTitle = new Label();

            txtCustomerPhone = new TextBox();
            lblCustomerPhone = new Label();

            lblPaymentTitle = new Label();

            panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            panelRight.SuspendLayout();
            panelRedeem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPointsToRedeem).BeginInit();
            SuspendLayout();

            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.White;
            panelLeft.Controls.Add(dgvCart);
            panelLeft.Controls.Add(txtBarcode);
            panelLeft.Controls.Add(lblBarcode);
            panelLeft.Controls.Add(lblCartTitle);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(20);
            panelLeft.Size = new Size(680, 660);
            panelLeft.TabIndex = 0;

            // 
            // lblCartTitle
            // 
            lblCartTitle.AutoSize = true;
            lblCartTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
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
            lblBarcode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.FromArgb(60, 60, 60);
            lblBarcode.Location = new Point(23, 68);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(140, 19);
            lblBarcode.TabIndex = 1;
            lblBarcode.Text = "Mã vạch sản phẩm:";

            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(20, 93);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Quét hoặc nhập mã vạch rồi nhấn Enter...";
            txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBarcode.Size = new Size(635, 29);
            txtBarcode.TabIndex = 2;
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.AllowUserToResizeRows = false;
            dgvCart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.FixedSingle;
            dgvCart.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(41, 100, 180),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
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
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            panelRight.Controls.Add(lblSubTotal);
            panelRight.Controls.Add(lblSubTotalTitle);
            panelRight.Controls.Add(panelRedeem);
            panelRight.Controls.Add(lblCustomerPoints);
            panelRight.Controls.Add(lblCustomerName);
            panelRight.Controls.Add(lblCustomerNameTitle);
            panelRight.Controls.Add(txtCustomerPhone);
            panelRight.Controls.Add(lblCustomerPhone);
            panelRight.Controls.Add(lblPaymentTitle);
            panelRight.Dock = DockStyle.Right;
            panelRight.Width = 370;
            panelRight.Location = new Point(680, 0);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(25);
            panelRight.Size = new Size(370, 660);
            panelRight.TabIndex = 1;

            // 
            // lblPaymentTitle
            // 
            lblPaymentTitle.AutoSize = true;
            lblPaymentTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblPaymentTitle.ForeColor = Color.FromArgb(35, 35, 35);
            lblPaymentTitle.Location = new Point(25, 15);
            lblPaymentTitle.Name = "lblPaymentTitle";
            lblPaymentTitle.Size = new Size(225, 28);
            lblPaymentTitle.TabIndex = 0;
            lblPaymentTitle.Text = "💳 THANH TOÁN";

            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomerPhone.ForeColor = Color.FromArgb(60, 60, 60);
            lblCustomerPhone.Location = new Point(25, 55);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new Size(116, 17);
            lblCustomerPhone.TabIndex = 1;
            lblCustomerPhone.Text = "SĐT khách hàng:";

            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Font = new Font("Segoe UI", 10.5F);
            txtCustomerPhone.Location = new Point(25, 76);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT rồi nhấn Enter...";
            txtCustomerPhone.Size = new Size(315, 26);
            txtCustomerPhone.TabIndex = 2;

            // 
            // lblCustomerNameTitle
            // 
            lblCustomerNameTitle.AutoSize = true;
            lblCustomerNameTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomerNameTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblCustomerNameTitle.Location = new Point(25, 110);
            lblCustomerNameTitle.Name = "lblCustomerNameTitle";
            lblCustomerNameTitle.Size = new Size(84, 17);
            lblCustomerNameTitle.TabIndex = 3;
            lblCustomerNameTitle.Text = "Khách hàng:";

            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoEllipsis = true;
            lblCustomerName.Font = new Font("Segoe UI", 9.5F);
            lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            lblCustomerName.Location = new Point(25, 130);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(315, 22);
            lblCustomerName.TabIndex = 4;
            lblCustomerName.Text = "Khách vãng lai";

            // 
            // lblCustomerPoints
            // 
            lblCustomerPoints.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomerPoints.ForeColor = Color.FromArgb(217, 119, 6); // Amber
            lblCustomerPoints.Location = new Point(25, 155);
            lblCustomerPoints.Name = "lblCustomerPoints";
            lblCustomerPoints.Size = new Size(315, 22);
            lblCustomerPoints.TabIndex = 5;
            lblCustomerPoints.Text = "⭐ Điểm tích lũy: 0 điểm";

            // 
            // panelRedeem
            // 
            panelRedeem.BackColor = Color.FromArgb(254, 243, 199); // Soft Gold
            panelRedeem.BorderStyle = BorderStyle.FixedSingle;
            panelRedeem.Controls.Add(lblDiscountValue);
            panelRedeem.Controls.Add(nudPointsToRedeem);
            panelRedeem.Controls.Add(lblRedeemPrompt);
            panelRedeem.Controls.Add(chkUseRewardPoints);
            panelRedeem.Location = new Point(25, 180);
            panelRedeem.Name = "panelRedeem";
            panelRedeem.Size = new Size(315, 62);
            panelRedeem.TabIndex = 6;

            // 
            // chkUseRewardPoints
            // 
            chkUseRewardPoints.AutoSize = true;
            chkUseRewardPoints.Enabled = false;
            chkUseRewardPoints.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkUseRewardPoints.ForeColor = Color.FromArgb(146, 64, 14);
            chkUseRewardPoints.Location = new Point(8, 6);
            chkUseRewardPoints.Name = "chkUseRewardPoints";
            chkUseRewardPoints.Size = new Size(215, 19);
            chkUseRewardPoints.TabIndex = 0;
            chkUseRewardPoints.Text = "Đổi điểm thưởng";
            chkUseRewardPoints.UseVisualStyleBackColor = true;

            // 
            // lblRedeemPrompt
            // 
            lblRedeemPrompt.AutoSize = true;
            lblRedeemPrompt.Font = new Font("Segoe UI", 8.5F);
            lblRedeemPrompt.ForeColor = Color.FromArgb(146, 64, 14);
            lblRedeemPrompt.Location = new Point(8, 33);
            lblRedeemPrompt.Name = "lblRedeemPrompt";
            lblRedeemPrompt.Size = new Size(68, 15);
            lblRedeemPrompt.TabIndex = 1;
            lblRedeemPrompt.Text = "Dùng điểm:";

            // 
            // nudPointsToRedeem
            // 
            nudPointsToRedeem.Enabled = false;
            nudPointsToRedeem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nudPointsToRedeem.Location = new Point(82, 30);
            nudPointsToRedeem.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudPointsToRedeem.Name = "nudPointsToRedeem";
            nudPointsToRedeem.Size = new Size(75, 23);
            nudPointsToRedeem.TabIndex = 2;

            // 
            // lblDiscountValue
            // 
            lblDiscountValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDiscountValue.ForeColor = Color.FromArgb(185, 28, 28);
            lblDiscountValue.Location = new Point(165, 30);
            lblDiscountValue.Name = "lblDiscountValue";
            lblDiscountValue.Size = new Size(140, 23);
            lblDiscountValue.TabIndex = 3;
            lblDiscountValue.Text = "-0 đ";
            lblDiscountValue.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblSubTotalTitle
            // 
            lblSubTotalTitle.AutoSize = true;
            lblSubTotalTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSubTotalTitle.ForeColor = Color.FromArgb(70, 70, 70);
            lblSubTotalTitle.Location = new Point(25, 252);
            lblSubTotalTitle.Name = "lblSubTotalTitle";
            lblSubTotalTitle.Size = new Size(68, 17);
            lblSubTotalTitle.TabIndex = 7;
            lblSubTotalTitle.Text = "Tạm tính:";

            // 
            // lblSubTotal
            // 
            lblSubTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSubTotal.ForeColor = Color.FromArgb(50, 50, 50);
            lblSubTotal.Location = new Point(160, 250);
            lblSubTotal.Name = "lblSubTotal";
            lblSubTotal.Size = new Size(180, 20);
            lblSubTotal.TabIndex = 8;
            lblSubTotal.Text = "0 đ";
            lblSubTotal.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblTotalTitle.Location = new Point(25, 280);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(145, 19);
            lblTotalTitle.TabIndex = 9;
            lblTotalTitle.Text = "TỔNG THANH TOÁN:";

            // 
            // lblTotalAmount
            // 
            lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTotalAmount.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(5, 150, 105);
            lblTotalAmount.Location = new Point(25, 303);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(315, 42);
            lblTotalAmount.TabIndex = 10;
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;

            // 
            // lblCashReceived
            // 
            lblCashReceived.AutoSize = true;
            lblCashReceived.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCashReceived.ForeColor = Color.FromArgb(60, 60, 60);
            lblCashReceived.Location = new Point(25, 352);
            lblCashReceived.Name = "lblCashReceived";
            lblCashReceived.Size = new Size(108, 17);
            lblCashReceived.TabIndex = 11;
            lblCashReceived.Text = "Tiền khách đưa:";

            // 
            // txtCashReceived
            // 
            txtCashReceived.Font = new Font("Segoe UI", 11.5F);
            txtCashReceived.Location = new Point(25, 373);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.PlaceholderText = "Nhập số tiền...";
            txtCashReceived.Size = new Size(315, 28);
            txtCashReceived.TabIndex = 12;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;

            // 
            // lblChangeTitle
            // 
            lblChangeTitle.AutoSize = true;
            lblChangeTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblChangeTitle.ForeColor = Color.FromArgb(60, 60, 60);
            lblChangeTitle.Location = new Point(25, 412);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(72, 17);
            lblChangeTitle.TabIndex = 13;
            lblChangeTitle.Text = "Tiền thừa:";

            // 
            // lblChange
            // 
            lblChange.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblChange.ForeColor = Color.FromArgb(37, 99, 235);
            lblChange.Location = new Point(25, 432);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(315, 28);
            lblChange.TabIndex = 14;
            lblChange.Text = "0 đ";
            lblChange.TextAlign = ContentAlignment.MiddleRight;

            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.FromArgb(5, 150, 105);
            btnCheckout.Cursor = Cursors.Hand;
            btnCheckout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(25, 480);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(315, 52);
            btnCheckout.TabIndex = 15;
            btnCheckout.Text = "💵  THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;

            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(71, 85, 105);
            btnClearCart.Cursor = Cursors.Hand;
            btnClearCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearCart.FlatAppearance.BorderSize = 0;
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(25, 545);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(315, 42);
            btnClearCart.TabIndex = 16;
            btnClearCart.Text = "🗑  HỦY GIỎ HÀNG";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;

            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(panelLeft);
            Controls.Add(panelRight);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPOS";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán hàng POS";

            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            panelRedeem.ResumeLayout(false);
            panelRedeem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPointsToRedeem).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
