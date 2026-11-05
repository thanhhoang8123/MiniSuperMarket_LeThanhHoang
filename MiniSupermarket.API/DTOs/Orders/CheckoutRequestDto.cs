using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    public class CheckoutRequest
    {
        public string CashierUsername { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        // Số điểm tích lũy khách hàng muốn đổi trừ trực tiếp vào hóa đơn (1 điểm = 1.000 đ)
        [Range(0, int.MaxValue, ErrorMessage = "Số điểm đổi thưởng không được âm.")]
        public int PointsToRedeem { get; set; } = 0;

        [Required]
        public List<CheckoutItemRequest> Items { get; set; } = new();
    }

    public class CheckoutItemRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Mã sản phẩm không hợp lệ.")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mua tối thiểu là 1.")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không hợp lệ.")]
        public decimal UnitPrice { get; set; }
    }
}
