namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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
            grpSearch = new GroupBox();
            btnLoad = new Button();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            grpCategoryList = new GroupBox();
            dgvCategories = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            grpCategoryInfo = new GroupBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtCategoryName = new TextBox();
            lblCategoryName = new Label();
            txtId = new TextBox();
            lblId = new Label();
            statusStrip = new ToolStrip();
            grpSearch.SuspendLayout();
            grpCategoryList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            grpCategoryInfo.SuspendLayout();
            SuspendLayout();
            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Location = new Point(10, 10);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(760, 60);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm";
            
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(380, 23);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(299, 23);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(10, 20);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(240, 23);
            txtKeyword.TabIndex = 0;
            txtKeyword.Text = "Nhập từ khóa...";
            // 
            // grpCategoryList
            // 
            grpCategoryList.Controls.Add(dgvCategories);
            grpCategoryList.Location = new Point(10, 80);
            grpCategoryList.Name = "grpCategoryList";
            grpCategoryList.Size = new Size(400, 320);
            grpCategoryList.TabIndex = 1;
            grpCategoryList.TabStop = false;
            grpCategoryList.Text = " Danh sách Nhóm hàng";
            // 
            // dgvCategories
            // 
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Columns.AddRange(new DataGridViewColumn[] { colId, colCategoryName, colDescription });
            dgvCategories.Location = new Point(10, 25);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(380, 290);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
           
            // 
            // colId
            // 
            colId.DataPropertyName = "id";
            colId.HeaderText = "Mã ID";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colCategoryName
            // 
            colCategoryName.DataPropertyName = "category_name";
            colCategoryName.HeaderText = "Tên Nhóm hàng";
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "description";
            colDescription.HeaderText = "description";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // grpCategoryInfo
            // 
            grpCategoryInfo.Controls.Add(btnDelete);
            grpCategoryInfo.Controls.Add(btnUpdate);
            grpCategoryInfo.Controls.Add(btnAdd);
            grpCategoryInfo.Controls.Add(txtDescription);
            grpCategoryInfo.Controls.Add(lblDescription);
            grpCategoryInfo.Controls.Add(txtCategoryName);
            grpCategoryInfo.Controls.Add(lblCategoryName);
            grpCategoryInfo.Controls.Add(txtId);
            grpCategoryInfo.Controls.Add(lblId);
            grpCategoryInfo.Location = new Point(420, 80);
            grpCategoryInfo.Name = "grpCategoryInfo";
            grpCategoryInfo.Size = new Size(350, 320);
            grpCategoryInfo.TabIndex = 2;
            grpCategoryInfo.TabStop = false;
            grpCategoryInfo.Text = "Thông tin Nhóm hàng";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(178, 243);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(97, 243);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(16, 243);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(16, 177);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(300, 60);
            txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 140);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(40, 15);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô Tả";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(10, 105);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(300, 23);
            txtCategoryName.TabIndex = 3;
           
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(10, 85);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(92, 15);
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên Nhóm hàng";
            
            // 
            // txtId
            // 
            txtId.Location = new Point(10, 50);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(300, 23);
            txtId.TabIndex = 1;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(10, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(38, 15);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID";
            // 
            // statusStrip
            // 
            statusStrip.Location = new Point(0, 0);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 25);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "toolStrip1";
            // 
            // FormCategoryManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStrip);
            Controls.Add(grpCategoryInfo);
            Controls.Add(grpCategoryList);
            Controls.Add(grpSearch);
            Location = new Point(70, 30);
            Name = "FormCategoryManagement";
            Text = "FormCategoryManagement";
            Load += FormCategoryManagement_Load;
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpCategoryList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            grpCategoryInfo.ResumeLayout(false);
            grpCategoryInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpSearch;
        private TextBox txtKeyword;
        private Button btnLoad;
        private Button btnSearch;
        private GroupBox grpCategoryList;
        private DataGridView dgvCategories;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colDescription;
        private GroupBox grpCategoryInfo;
        private Label lblCategoryName;
        private TextBox txtId;
        private Label lblId;
        private TextBox txtCategoryName;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private TextBox txtDescription;
        private Label lblDescription;
        private ToolStrip statusStrip;
    }
}