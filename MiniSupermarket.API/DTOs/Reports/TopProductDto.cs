namespace MiniSupermarket.API.DTOs
{
    // DTO đại diện cho sản phẩm bán chạy nhất phục vụ thống kê Best-Sellers
    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
