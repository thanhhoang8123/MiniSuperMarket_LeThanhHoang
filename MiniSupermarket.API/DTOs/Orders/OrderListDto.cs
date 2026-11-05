
namespace MiniSupermarket.API.DTOs
{
    public class OrderListDto
    {
        public int OrderId { get; set; }

        public string OrderCode { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string CashierUsername { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public int TotalItems { get; set; }

        public decimal FinalAmount { get; set; }
    }
}
