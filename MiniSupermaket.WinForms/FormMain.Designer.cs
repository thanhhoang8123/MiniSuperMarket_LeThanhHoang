namespace MiniSupermarket.WinForms
{
    partial class FormMain
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
            btnCategory = new Button();
            btnCustomer = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCategory
            // 
            btnCategory.Location = new Point(88, 123);
            btnCategory.Name = "btnCategory";
            btnCategory.Size = new Size(240, 43);
            btnCategory.TabIndex = 0;
            btnCategory.Text = "Quản lý nhóm hàng";
            btnCategory.UseVisualStyleBackColor = true;
            // 
            // btnCustomer
            // 
            btnCustomer.Location = new Point(349, 123);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Size = new Size(250, 43);
            btnCustomer.TabIndex = 1;
            btnCustomer.Text = "Quản lý khách hàng";
            btnCustomer.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(171, 37);
            label1.Name = "label1";
            label1.Size = new Size(339, 41);
            label1.TabIndex = 2;
            label1.Text = "  MINI SUPERMARKET ";
           
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnCustomer);
            Controls.Add(btnCategory);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCategory;
        private Button btnCustomer;
        private Label label1;
    }
}