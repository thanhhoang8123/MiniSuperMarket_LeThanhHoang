using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MiniSupermarket.WinForms.DTOs
{
    // DTO nhận dữ liệu top sản phẩm bán chạy nhất từ API
    public class TopProductDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("barcode")]
        public string Barcode { get; set; } = string.Empty;

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("totalQuantitySold")]
        public int TotalQuantitySold { get; set; }

        [JsonPropertyName("totalRevenue")]
        public decimal TotalRevenue { get; set; }
    }

    public class TopProductsResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("data")]
        public List<TopProductDto> Data { get; set; } = new();
    }
}
