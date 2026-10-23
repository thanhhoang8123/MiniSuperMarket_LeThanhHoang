namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer components = null;

        // =========================================
        // CONTROLS
        // =========================================

        private Panel panelSearch;
        private Panel panelGrid;
        private Panel panelDetail;

        private Label lblSearchTitle;
        private Label lblProductListTitle;
        private Label lblDetailTitle;

        private DataGridView dgvProducts;

        private TextBox txtId;
        private TextBox txtBarcode;
        private TextBox txtProductName;
        private TextBox txtKeyword;

        private NumericUpDown nudPrice;
        private NumericUpDown nudStock;

        private ComboBox cboCategory;
        private ComboBox cboFilterCategory;

        private Button btnSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnPrevious;
        private Button btnNext;

        private Label lblPageInfo;
        private Label lblBarcode;
        private Label lblProductName;
        private Label lblCategory;
        private Label lblPrice;
        private Label lblStock;
        private Label lblId;

        // =========================================
        // DISPOSE
        // =========================================

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // =========================================
        // INITIALIZE COMPONENT
        // =========================================

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panelSearch = new Panel();
            lblSearchTitle = new Label();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            cboFilterCategory = new ComboBox();
            panelGrid = new Panel();
            lblProductListTitle = new Label();
            dgvProducts = new DataGridView();
            btnPrevious = new Button();
            lblPageInfo = new Label();
            btnNext = new Button();
            panelDetail = new Panel();
            lblDetailTitle = new Label();
            lblId = new Label();
            txtId = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblPrice = new Label();
            nudPrice = new NumericUpDown();
            lblStock = new Label();
            nudStock = new NumericUpDown();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            panelSearch.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            SuspendLayout();
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.White;
            panelSearch.BorderStyle = BorderStyle.FixedSingle;
            panelSearch.Controls.Add(lblSearchTitle);
            panelSearch.Controls.Add(txtKeyword);
            panelSearch.Controls.Add(btnSearch);
            panelSearch.Controls.Add(cboFilterCategory);
            panelSearch.Location = new Point(17, 20);
            panelSearch.Margin = new Padding(3, 4, 3, 4);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(1223, 106);
            panelSearch.TabIndex = 0;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearchTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblSearchTitle.Location = new Point(21, 13);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(212, 23);
            lblSearchTitle.TabIndex = 0;
            lblSearchTitle.Text = "🔎 TÌM KIẾM SẢN PHẨM";
            // 
            // txtKeyword
            // 
            txtKeyword.Font = new Font("Segoe UI", 10F);
            txtKeyword.Location = new Point(23, 51);
            txtKeyword.Margin = new Padding(3, 4, 3, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập mã Barcode hoặc tên sản phẩm...";
            txtKeyword.Size = new Size(742, 30);
            txtKeyword.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(41, 100, 180);
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(783, 48);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(126, 43);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔎 Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;

            // cboFilterCategory
            cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.FormattingEnabled = true;
            cboFilterCategory.Location = new Point(825, 51);
            cboFilterCategory.Name = "cboFilterCategory";
            cboFilterCategory.Size = new Size(370, 28);
            cboFilterCategory.TabIndex = 3;
            cboFilterCategory.SelectedIndexChanged += cboFilterCategory_SelectedIndexChanged;
            // 
            // panelGrid
            // 
            panelGrid.BackColor = Color.White;
            panelGrid.BorderStyle = BorderStyle.FixedSingle;
            panelGrid.Controls.Add(lblProductListTitle);
            panelGrid.Controls.Add(dgvProducts);
            panelGrid.Controls.Add(btnPrevious);
            panelGrid.Controls.Add(lblPageInfo);
            panelGrid.Controls.Add(btnNext);
            panelGrid.Location = new Point(17, 147);
            panelGrid.Margin = new Padding(3, 4, 3, 4);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(800, 693);
            panelGrid.TabIndex = 1;
            // 
            // 
            // 
            // lblProductListTitle
            // 
            lblProductListTitle.AutoSize = true;
            lblProductListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblProductListTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblProductListTitle.Location = new Point(17, 16);
            lblProductListTitle.Name = "lblProductListTitle";
            lblProductListTitle.Size = new Size(232, 25);
            lblProductListTitle.TabIndex = 0;
            lblProductListTitle.Text = "DANH SÁCH SẢN PHẨM";
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvProducts.ColumnHeadersHeight = 35;
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.Location = new Point(17, 60);
            dgvProducts.Margin = new Padding(3, 4, 3, 4);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(763, 540);
            dgvProducts.TabIndex = 1;
            dgvProducts.CellClick += dgvProducts_CellClick;
            
            // 
            // btnPrevious
            // 
            btnPrevious.Cursor = Cursors.Hand;
            btnPrevious.Enabled = false;
            btnPrevious.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevious.Location = new Point(17, 620);
            btnPrevious.Margin = new Padding(3, 4, 3, 4);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(143, 43);
            btnPrevious.TabIndex = 2;
            btnPrevious.Text = "◀ Trang trước";
            btnPrevious.Click += btnPrevious_Click;
            // 
            // lblPageInfo
            // 
            lblPageInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPageInfo.Location = new Point(291, 629);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(194, 29);
            lblPageInfo.TabIndex = 3;
            lblPageInfo.Text = "Trang 0 / 0";
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            btnNext.Cursor = Cursors.Hand;
            btnNext.Enabled = false;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.Location = new Point(621, 620);
            btnNext.Margin = new Padding(3, 4, 3, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(160, 43);
            btnNext.TabIndex = 4;
            btnNext.Text = "Trang sau ▶";
            btnNext.Click += btnNext_Click;
            // 
            // panelDetail
            // 
            panelDetail.BackColor = Color.White;
            panelDetail.BorderStyle = BorderStyle.FixedSingle;
            panelDetail.Controls.Add(lblDetailTitle);
            panelDetail.Controls.Add(lblId);
            panelDetail.Controls.Add(txtId);
            panelDetail.Controls.Add(lblBarcode);
            panelDetail.Controls.Add(txtBarcode);
            panelDetail.Controls.Add(lblProductName);
            panelDetail.Controls.Add(txtProductName);
            panelDetail.Controls.Add(lblCategory);
            panelDetail.Controls.Add(cboCategory);
            panelDetail.Controls.Add(lblPrice);
            panelDetail.Controls.Add(nudPrice);
            panelDetail.Controls.Add(lblStock);
            panelDetail.Controls.Add(nudStock);
            panelDetail.Controls.Add(btnAdd);
            panelDetail.Controls.Add(btnUpdate);
            panelDetail.Controls.Add(btnDelete);
            panelDetail.Controls.Add(btnClear);
            panelDetail.Location = new Point(834, 147);
            panelDetail.Margin = new Padding(3, 4, 3, 4);
            panelDetail.Name = "panelDetail";
            panelDetail.Size = new Size(405, 693);
            panelDetail.TabIndex = 2;
            // 
            // lblDetailTitle
            // 
            lblDetailTitle.AutoSize = true;
            lblDetailTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetailTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblDetailTitle.Location = new Point(21, 20);
            lblDetailTitle.Name = "lblDetailTitle";
            lblDetailTitle.Size = new Size(225, 25);
            lblDetailTitle.TabIndex = 0;
            lblDetailTitle.Text = "THÔNG TIN SẢN PHẨM";
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblId.Location = new Point(23, 73);
            lblId.Name = "lblId";
            lblId.Size = new Size(103, 20);
            lblId.TabIndex = 1;
            lblId.Text = "Mã sản phẩm";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(240, 242, 245);
            txtId.Location = new Point(23, 101);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(354, 27);
            txtId.TabIndex = 2;
            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBarcode.Location = new Point(23, 149);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(92, 20);
            lblBarcode.TabIndex = 3;
            lblBarcode.Text = "Mã Barcode";
            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(23, 177);
            txtBarcode.Margin = new Padding(3, 4, 3, 4);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.Size = new Size(354, 27);
            txtBarcode.TabIndex = 4;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProductName.Location = new Point(23, 225);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(106, 20);
            lblProductName.TabIndex = 5;
            lblProductName.Text = "Tên sản phẩm";
            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(23, 253);
            txtProductName.Margin = new Padding(3, 4, 3, 4);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(354, 27);
            txtProductName.TabIndex = 6;
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategory.Location = new Point(23, 301);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(80, 20);
            lblCategory.TabIndex = 7;
            lblCategory.Text = "Danh mục";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Location = new Point(23, 329);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(354, 28);
            cboCategory.TabIndex = 8;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrice.Location = new Point(23, 377);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(62, 20);
            lblPrice.TabIndex = 9;
            lblPrice.Text = "Giá bán";
            // 
            // nudPrice
            // 
            nudPrice.Location = new Point(23, 405);
            nudPrice.Margin = new Padding(3, 4, 3, 4);
            nudPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudPrice.Name = "nudPrice";
            nudPrice.Size = new Size(354, 27);
            nudPrice.TabIndex = 10;
            nudPrice.ThousandsSeparator = true;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStock.Location = new Point(23, 453);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(66, 20);
            lblStock.TabIndex = 11;
            lblStock.Text = "Tồn kho";
            // 
            // nudStock
            // 
            nudStock.Location = new Point(23, 481);
            nudStock.Margin = new Padding(3, 4, 3, 4);
            nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(354, 27);
            nudStock.TabIndex = 12;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 167, 69);
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(23, 547);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(109, 51);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(255, 193, 7);
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.Black;
            btnUpdate.Location = new Point(143, 547);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(109, 51);
            btnUpdate.TabIndex = 14;
            btnUpdate.Text = "✏ Sửa";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(263, 547);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(114, 51);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "🗑 Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(23, 613);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(354, 47);
            btnClear.TabIndex = 16;
            btnClear.Text = "↻ Làm mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // FormProductManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1257, 867);
            Controls.Add(panelSearch);
            Controls.Add(panelGrid);
            Controls.Add(panelDetail);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormProductManagement";
            Text = "Quản lý sản phẩm";
            Load += FormProductManagement_Load;
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            panelGrid.ResumeLayout(false);
            panelGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            panelDetail.ResumeLayout(false);
            panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            ResumeLayout(false);
        }

    }
}