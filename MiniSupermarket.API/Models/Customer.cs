using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng tối đa 100 ký tự.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ tối đa 200 ký tự.")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm tích lũy không được âm.")]
        public int RewardPoints { get; set; } = 0;

        [StringLength(50, ErrorMessage = "Hạng thành viên tối đa 50 ký tự.")]
        public string MembershipRank { get; set; } = "Chuẩn";

        public DateTime? RegisteredAt { get; set; }

        // Quan hệ 1-N: Một khách hàng có nhiều đơn hàng
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

