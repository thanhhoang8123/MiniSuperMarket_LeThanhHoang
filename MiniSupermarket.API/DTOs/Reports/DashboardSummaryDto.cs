namespace MiniSupermarket.API.DTOs
{
    // DTO chứa các chỉ số tổng quan phục vụ màn hình Dashboard (KPI Summary)
    public class DashboardSummaryDto
    {
        // 1. Doanh thu hôm nay (VNĐ)
        public decimal TodayRevenue { get; set; }

        // 2. Tổng số hóa đơn đã xuất
        public int TotalOrders { get; set; }

        // 3. Số lượng khách hàng mới đăng ký
        public int NewCustomers { get; set; }

        // 4. Số mặt hàng sắp hết hàng (tồn kho <= 10)
        public int LowStockProducts { get; set; }
    }
}
