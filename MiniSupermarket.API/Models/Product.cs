using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    // Entity Product đại diện cho bảng Products trong SQL Server
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Mã vạch không được để trống.")]
        [StringLength(50, ErrorMessage = "Mã vạch tối đa 50 ký tự.")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá bán không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm.")]
        public int StockQuantity { get; set; }

        // Khóa ngoại khai báo trực tiếp trong Model bằng Data Annotation [ForeignKey]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        // Thuộc tính điều hướng (Navigation Property)
        public virtual Category? Category { get; set; }
    }
}
