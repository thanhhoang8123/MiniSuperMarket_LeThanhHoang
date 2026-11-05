using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    public class CustomerUpdateDto
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng tối đa 100 ký tự.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Địa chỉ tối đa 255 ký tự.")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm thưởng không được âm.")]
        public int RewardPoints { get; set; }

        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
