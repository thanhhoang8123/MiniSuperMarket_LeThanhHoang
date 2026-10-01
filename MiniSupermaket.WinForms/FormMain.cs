namespace MiniSupermarket.WinForms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            btnCategory.Click += btnCategory_Click;
            btnCustomer.Click += btnCustomer_Click;
        }

        private void btnCategory_Click(object? sender, EventArgs e)
        {
            using var form = new FormCategoryManagement();
            form.ShowDialog();
        }

        private void btnCustomer_Click(object? sender, EventArgs e)
        {
            using var form = new FormCustomerManagement();
            form.ShowDialog();
        }
    }
}