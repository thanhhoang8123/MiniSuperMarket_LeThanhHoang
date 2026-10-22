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

        private Button btnSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;

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
            components = new System.ComponentModel.Container();

            panelSearch = new Panel();
            panelGrid = new Panel();
            panelDetail = new Panel();

            lblSearchTitle = new Label();
            lblProductListTitle = new Label();
            lblDetailTitle = new Label();

            dgvProducts = new DataGridView();

            txtId = new TextBox();
            txtBarcode = new TextBox();
            txtProductName = new TextBox();
            txtKeyword = new TextBox();

            nudPrice = new NumericUpDown();
            nudStock = new NumericUpDown();

            cboCategory = new ComboBox();

            btnSearch = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            lblId = new Label();
            lblBarcode = new Label();
            lblProductName = new Label();
            lblCategory = new Label();
            lblPrice = new Label();
            lblStock = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();

            SuspendLayout();

            // =========================================
            // FORM
            // =========================================

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;

            BackColor = Color.FromArgb(245, 247, 250);

            ClientSize = new Size(1100, 650);

            FormBorderStyle = FormBorderStyle.None;

            Name = "FormProductManagement";
            Text = "Quản lý sản phẩm";

            // =========================================
            // PANEL SEARCH
            // =========================================

            panelSearch.BackColor = Color.White;

            panelSearch.Location = new Point(15, 15);
            panelSearch.Size = new Size(1070, 80);

            panelSearch.BorderStyle = BorderStyle.FixedSingle;

            // -----------------------------------------
            // SEARCH TITLE
            // -----------------------------------------

            lblSearchTitle.AutoSize = true;

            lblSearchTitle.Font = new Font(
                "Segoe UI",
                10F,
                FontStyle.Bold);

            lblSearchTitle.ForeColor =
                Color.FromArgb(40, 40, 40);

            lblSearchTitle.Location =
                new Point(18, 10);

            lblSearchTitle.Text =
                "🔎 TÌM KIẾM SẢN PHẨM";

            // -----------------------------------------
            // SEARCH TEXTBOX
            // -----------------------------------------

            txtKeyword.Location =
                new Point(20, 38);

            txtKeyword.Size =
                new Size(650, 27);

            txtKeyword.Font =
                new Font("Segoe UI", 10F);

            txtKeyword.PlaceholderText =
                "Nhập mã Barcode hoặc tên sản phẩm...";

            // -----------------------------------------
            // SEARCH BUTTON
            // -----------------------------------------

            btnSearch.Location =
                new Point(685, 36);

            btnSearch.Size =
                new Size(110, 32);

            btnSearch.Text =
                "🔎 Tìm kiếm";

            btnSearch.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnSearch.BackColor =
                Color.FromArgb(41, 100, 180);

            btnSearch.ForeColor =
                Color.White;

            btnSearch.FlatStyle =
                FlatStyle.Flat;

            btnSearch.FlatAppearance.BorderSize = 0;

            btnSearch.Cursor =
                Cursors.Hand;

            // =========================================
            // PANEL GRID
            // =========================================

            panelGrid.BackColor =
                Color.White;

            panelGrid.Location =
                new Point(15, 110);

            panelGrid.Size =
                new Size(700, 520);

            panelGrid.BorderStyle =
                BorderStyle.FixedSingle;

            // -----------------------------------------
            // GRID TITLE
            // -----------------------------------------

            lblProductListTitle.AutoSize = true;

            lblProductListTitle.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            lblProductListTitle.ForeColor =
                Color.FromArgb(40, 40, 40);

            lblProductListTitle.Location =
                new Point(15, 12);

            lblProductListTitle.Text =
                "DANH SÁCH SẢN PHẨM";

            // =========================================
            // DATAGRIDVIEW
            // =========================================

            dgvProducts.Location =
                new Point(15, 45);

            dgvProducts.Size =
                new Size(668, 455);

            dgvProducts.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvProducts.BackgroundColor =
                Color.White;

            dgvProducts.BorderStyle =
                BorderStyle.None;

            dgvProducts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.MultiSelect = false;

            dgvProducts.ReadOnly = true;

            dgvProducts.AllowUserToAddRows = false;

            dgvProducts.AllowUserToDeleteRows = false;

            dgvProducts.AllowUserToResizeRows = false;

            dgvProducts.RowHeadersVisible = false;

            dgvProducts.AutoGenerateColumns = true;

            dgvProducts.ColumnHeadersHeight = 35;

            dgvProducts.EnableHeadersVisualStyles = false;

            dgvProducts.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(41, 100, 180),
                    ForeColor = Color.White,
                    Font = new Font(
                        "Segoe UI",
                        9F,
                        FontStyle.Bold),
                    Alignment =
                        DataGridViewContentAlignment.MiddleCenter
                };

            dgvProducts.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    Font = new Font(
                        "Segoe UI",
                        9F),
                    SelectionBackColor =
                        Color.FromArgb(220, 235, 252),
                    SelectionForeColor =
                        Color.Black
                };

            // =========================================
            // PANEL DETAIL
            // =========================================

            panelDetail.BackColor =
                Color.White;

            panelDetail.Location =
                new Point(730, 110);

            panelDetail.Size =
                new Size(355, 520);

            panelDetail.BorderStyle =
                BorderStyle.FixedSingle;

            // -----------------------------------------
            // DETAIL TITLE
            // -----------------------------------------

            lblDetailTitle.AutoSize = true;

            lblDetailTitle.Font =
                new Font(
                    "Segoe UI",
                    11F,
                    FontStyle.Bold);

            lblDetailTitle.ForeColor =
                Color.FromArgb(40, 40, 40);

            lblDetailTitle.Location =
                new Point(18, 15);

            lblDetailTitle.Text =
                "THÔNG TIN SẢN PHẨM";

            // =========================================
            // ID
            // =========================================

            lblId.AutoSize = true;

            lblId.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblId.Location =
                new Point(20, 55);

            lblId.Text =
                "Mã sản phẩm";

            txtId.Location =
                new Point(20, 76);

            txtId.Size =
                new Size(310, 27);

            txtId.ReadOnly = true;

            txtId.BackColor =
                Color.FromArgb(240, 242, 245);

            // =========================================
            // BARCODE
            // =========================================

            lblBarcode.AutoSize = true;

            lblBarcode.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblBarcode.Location =
                new Point(20, 112);

            lblBarcode.Text =
                "Mã Barcode";

            txtBarcode.Location =
                new Point(20, 133);

            txtBarcode.Size =
                new Size(310, 27);

            // =========================================
            // PRODUCT NAME
            // =========================================

            lblProductName.AutoSize = true;

            lblProductName.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblProductName.Location =
                new Point(20, 169);

            lblProductName.Text =
                "Tên sản phẩm";

            txtProductName.Location =
                new Point(20, 190);

            txtProductName.Size =
                new Size(310, 27);

            // =========================================
            // CATEGORY
            // =========================================

            lblCategory.AutoSize = true;

            lblCategory.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblCategory.Location =
                new Point(20, 226);

            lblCategory.Text =
                "Danh mục";

            cboCategory.Location =
                new Point(20, 247);

            cboCategory.Size =
                new Size(310, 28);

            cboCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // =========================================
            // PRICE
            // =========================================

            lblPrice.AutoSize = true;

            lblPrice.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblPrice.Location =
                new Point(20, 283);

            lblPrice.Text =
                "Giá bán";

            nudPrice.Location =
                new Point(20, 304);

            nudPrice.Size =
                new Size(310, 27);

            nudPrice.Minimum = 0;

            nudPrice.Maximum =
                1000000000;

            nudPrice.DecimalPlaces = 0;

            nudPrice.ThousandsSeparator = true;

            // =========================================
            // STOCK
            // =========================================

            lblStock.AutoSize = true;

            lblStock.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblStock.Location =
                new Point(20, 340);

            lblStock.Text =
                "Tồn kho";

            nudStock.Location =
                new Point(20, 361);

            nudStock.Size =
                new Size(310, 27);

            nudStock.Minimum = 0;

            nudStock.Maximum =
                1000000;

            // =========================================
            // BUTTON ADD
            // =========================================

            btnAdd.Location =
                new Point(20, 410);

            btnAdd.Size =
                new Size(95, 38);

            btnAdd.Text =
                "➕ Thêm";

            btnAdd.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnAdd.BackColor =
                Color.FromArgb(40, 167, 69);

            btnAdd.ForeColor =
                Color.White;

            btnAdd.FlatStyle =
                FlatStyle.Flat;

            btnAdd.FlatAppearance.BorderSize = 0;

            btnAdd.Cursor =
                Cursors.Hand;

            // =========================================
            // BUTTON UPDATE
            // =========================================

            btnUpdate.Location =
                new Point(125, 410);

            btnUpdate.Size =
                new Size(95, 38);

            btnUpdate.Text =
                "✏ Sửa";

            btnUpdate.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnUpdate.BackColor =
                Color.FromArgb(255, 193, 7);

            btnUpdate.ForeColor =
                Color.Black;

            btnUpdate.FlatStyle =
                FlatStyle.Flat;

            btnUpdate.FlatAppearance.BorderSize = 0;

            btnUpdate.Cursor =
                Cursors.Hand;

            // =========================================
            // BUTTON DELETE
            // =========================================

            btnDelete.Location =
                new Point(230, 410);

            btnDelete.Size =
                new Size(100, 38);

            btnDelete.Text =
                "🗑 Xóa";

            btnDelete.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnDelete.BackColor =
                Color.FromArgb(220, 53, 69);

            btnDelete.ForeColor =
                Color.White;

            btnDelete.FlatStyle =
                FlatStyle.Flat;

            btnDelete.FlatAppearance.BorderSize = 0;

            btnDelete.Cursor =
                Cursors.Hand;

            // =========================================
            // BUTTON CLEAR
            // =========================================

            btnClear.Location =
                new Point(20, 460);

            btnClear.Size =
                new Size(310, 35);

            btnClear.Text =
                "↻ Làm mới";

            btnClear.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnClear.BackColor =
                Color.FromArgb(108, 117, 125);

            btnClear.ForeColor =
                Color.White;

            btnClear.FlatStyle =
                FlatStyle.Flat;

            btnClear.FlatAppearance.BorderSize = 0;

            btnClear.Cursor =
                Cursors.Hand;

            // =========================================
            // ADD CONTROLS - SEARCH
            // =========================================

            panelSearch.Controls.Add(
                lblSearchTitle);

            panelSearch.Controls.Add(
                txtKeyword);

            panelSearch.Controls.Add(
                btnSearch);

            // =========================================
            // ADD CONTROLS - GRID
            // =========================================

            panelGrid.Controls.Add(
                lblProductListTitle);

            panelGrid.Controls.Add(
                dgvProducts);

            // =========================================
            // ADD CONTROLS - DETAIL
            // =========================================

            panelDetail.Controls.Add(
                lblDetailTitle);

            panelDetail.Controls.Add(
                lblId);

            panelDetail.Controls.Add(
                txtId);

            panelDetail.Controls.Add(
                lblBarcode);

            panelDetail.Controls.Add(
                txtBarcode);

            panelDetail.Controls.Add(
                lblProductName);

            panelDetail.Controls.Add(
                txtProductName);

            panelDetail.Controls.Add(
                lblCategory);

            panelDetail.Controls.Add(
                cboCategory);

            panelDetail.Controls.Add(
                lblPrice);

            panelDetail.Controls.Add(
                nudPrice);

            panelDetail.Controls.Add(
                lblStock);

            panelDetail.Controls.Add(
                nudStock);

            panelDetail.Controls.Add(
                btnAdd);

            panelDetail.Controls.Add(
                btnUpdate);

            panelDetail.Controls.Add(
                btnDelete);

            panelDetail.Controls.Add(
                btnClear);

            // =========================================
            // ADD PANELS TO FORM
            // =========================================

            Controls.Add(panelSearch);
            Controls.Add(panelGrid);
            Controls.Add(panelDetail);

            // =========================================
            // EVENTS
            // =========================================

            Load += FormProductManagement_Load;

            dgvProducts.CellClick +=
                dgvProducts_CellClick;

            btnAdd.Click +=
                btnAdd_Click;

            btnUpdate.Click +=
                btnUpdate_Click;

            btnDelete.Click +=
                btnDelete_Click;

            btnClear.Click +=
                btnClear_Click;

            btnSearch.Click +=
                btnSearch_Click;

            // =========================================
            // FINISH
            // =========================================

            ((System.ComponentModel.ISupportInitialize)
                dgvProducts).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                nudPrice).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                nudStock).EndInit();

            ResumeLayout(false);
            PerformLayout();
        }
    }
}