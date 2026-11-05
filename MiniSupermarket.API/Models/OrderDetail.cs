using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailId { get; set; }

        // Khóa ngoại liên kết tới Order khai báo trực tiếp trong Model bằng Data Annotation [ForeignKey]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        // Khóa ngoại liên kết tới Product khai báo trực tiếp trong Model bằng Data Annotation [ForeignKey]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Số lượng mua không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mua tối thiểu là 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Đơn giá không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; }

        // Thuộc tính điều hướng (Navigation Properties)
        public virtual Order? Order { get; set; }

        public virtual Product? Product { get; set; }
    }
}
