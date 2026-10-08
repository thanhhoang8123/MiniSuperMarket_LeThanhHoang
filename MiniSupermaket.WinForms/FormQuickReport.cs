using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();

            Load += FormQuickReport_Load;
            btnRunReport.Click += btnRunReport_Click;
        }

        private void FormQuickReport_Load(object? sender, EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today;

            // Dữ liệu mẫu ban đầu
            lblTotalOrders.Text = "0";
            lblTotalRevenue.Text = "0 ₫";
            lblBestSeller.Text = "---";
        }

        private void btnRunReport_Click(object? sender, EventArgs e)
        {
            // Tạm thời dùng dữ liệu mẫu để kiểm tra giao diện
            // Sau này sẽ thay bằng dữ liệu lấy từ API Orders.

            lblTotalOrders.Text = "12";
            lblTotalRevenue.Text = "2.450.000 ₫";
            lblBestSeller.Text = "Nước mắm Nam Ngư";

            MessageBox.Show(
                $"Đã tải báo cáo ngày {dtpReportDate.Value:dd/MM/yyyy}!",
                "Báo cáo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}