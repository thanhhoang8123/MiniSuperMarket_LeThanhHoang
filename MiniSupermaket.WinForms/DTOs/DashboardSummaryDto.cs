using System.Text.Json.Serialization;

namespace MiniSupermarket.WinForms.DTOs
{
    // DTO nhận dữ liệu thống kê tổng quan (KPI Dashboard) từ API
    public class DashboardSummaryDto
    {
        [JsonPropertyName("todayRevenue")]
        public decimal TodayRevenue { get; set; }

        [JsonPropertyName("totalOrders")]
        public int TotalOrders { get; set; }

        [JsonPropertyName("newCustomers")]
        public int NewCustomers { get; set; }

        [JsonPropertyName("lowStockProducts")]
        public int LowStockProducts { get; set; }
    }

    public class DashboardSummaryResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public DashboardSummaryDto? Data { get; set; }
    }
}
