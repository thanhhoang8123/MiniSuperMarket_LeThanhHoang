using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    // Entity Product đại diện cho bảng Products trong SQL Server
    public class Product
    {
        // Khóa chính của sản phẩm
        // SQL Server sẽ tự động tăng giá trị ProductId
        [Key]
        public int ProductId { get; set; }

        // Mã vạch sản phẩm
        // Bắt buộc nhập và tối đa 50 ký tự
        [Required]
        [StringLength(50)]
        public string Barcode { get; set; } = string.Empty;

        // Tên sản phẩm
        // Bắt buộc nhập và tối đa 200 ký tự
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        // Giá bán sản phẩm
        //
        // decimal(18,2):
        // - Tổng cộng tối đa 18 chữ số
        // - Trong đó có 2 chữ số sau dấu thập phân
        //
        // Ví dụ:
        // 15000.00
        // 125000.50
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Số lượng sản phẩm hiện đang tồn kho
        public int StockQuantity { get; set; }

        // Foreign Key (khóa ngoại)
        // Dùng để liên kết Product với Category
        public int CategoryId { get; set; }

        // Navigation Property
        // Cho phép truy cập Category của Product
        //
        // Ví dụ:
        // product.Category.CategoryName
        public Category? Category { get; set; }
    }
}