using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MiniSupermarket.API.Models
{
    // Entity Category đại diện cho bảng Categories trong SQL Server
    [Table("Categories")]
    public class Category
    {
        // Khóa chính, SQL Server tự tăng ID
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        // Tên danh mục, bắt buộc và tối đa 100 ký tự
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        // Mô tả danh mục, có thể để trống
        [StringLength(500)]
        public string? Description { get; set; }

        // Quan hệ 1-N: Một Category có nhiều Product
        // JsonIgnore tránh vòng lặp khi chuyển sang JSON
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
