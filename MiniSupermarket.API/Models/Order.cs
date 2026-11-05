using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Mã đơn hàng không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã đơn hàng tối đa 50 ký tự.")]
        public string OrderCode { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Tên thu ngân không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên thu ngân tối đa 50 ký tự.")]
        public string CashierUsername { get; set; } = string.Empty;

        // Khóa ngoại khai báo trực tiếp trong Model bằng Data Annotation [ForeignKey]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Tổng số lượng mặt hàng tối thiểu là 1.")]
        public int TotalItems { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Tổng thanh toán không được âm.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được âm.")]
        public int RewardPoints { get; set; } = 0;

        // Thuộc tính điều hướng (Navigation Property)
        public virtual Customer? Customer { get; set; }

        // Quan hệ 1-N: Một đơn hàng có nhiều chi tiết đơn hàng
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}
