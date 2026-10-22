
namespace MiniSupermarket.WinForms.DTOs
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        public ProductDto? Product { get; set; }

        public string ProductName =>
            Product?.ProductName ?? "Không rõ sản phẩm";
    }
}
