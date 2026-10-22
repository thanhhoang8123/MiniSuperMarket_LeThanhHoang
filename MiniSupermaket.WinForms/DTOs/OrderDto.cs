
using System;
using System.Collections.Generic;

namespace MiniSupermarket.WinForms.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CashierUsername { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public decimal FinalAmount { get; set; }

        public List<OrderDetailDto>? OrderDetails { get; set; }
    }
}
