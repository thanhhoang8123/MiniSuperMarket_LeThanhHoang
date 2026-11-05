namespace MiniSupermarket.API.DTOs
{
    // DTO biểu diễn doanh thu gom nhóm theo từng ngày phục vụ vẽ biểu đồ (Chart)
    public class DailyRevenueDto
    {
        public string Date { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
