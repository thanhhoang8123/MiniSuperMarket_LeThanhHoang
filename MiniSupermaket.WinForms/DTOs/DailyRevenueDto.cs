using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MiniSupermarket.WinForms.DTOs
{
    // DTO nhận dữ liệu doanh thu từng ngày phục vụ vẽ biểu đồ (Chart)
    public class DailyRevenueDto
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("totalOrders")]
        public int TotalOrders { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }

    public class DailyRevenueResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("totalDays")]
        public int TotalDays { get; set; }

        [JsonPropertyName("data")]
        public List<DailyRevenueDto> Data { get; set; } = new();
    }
}
