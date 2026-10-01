using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc giữa ứng dụng
    // và cơ sở dữ liệu SQL Server.
    //
    // Có thể hiểu đơn giản:
    // DbContext = "cầu nối" giữa C# và Database.
    public class SupermarketDbContext : DbContext
    {
        // Constructor nhận DbContextOptions từ Dependency Injection (DI)
        // Các cấu hình như SQL Server Connection String
        // sẽ được truyền vào đây từ Program.cs.
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        // DbSet<Category> đại diện cho bảng Categories
        // trong cơ sở dữ liệu.
        public DbSet<Category> Categories { get; set; }

        // DbSet<Product> đại diện cho bảng Products
        // trong cơ sở dữ liệu.
        public DbSet<Product> Products { get; set; }
        // Đại diện cho bảng Customers trong SQL Server
        public DbSet<Customer> Customers { get; set; }
        // OnModelCreating:
        // Dùng để cấu hình thêm cho Model khi EF Core xây dựng
        // cấu trúc cơ sở dữ liệu.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gọi cấu hình mặc định của lớp DbContext
            base.OnModelCreating(modelBuilder);

            // Data Seeding:
            // Nạp sẵn dữ liệu mẫu vào bảng Categories.
            //
            // Khi chạy Migration, EF Core sẽ tạo các bản ghi này
            // trong database.
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Bánh kẹo & Đồ ăn vặt",
                    Description = "Snack, bánh quy, kẹo dẻo"
                },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Nước giải khát & Trà",
                    Description = "Nước ngọt, nước khoáng, trà"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Sữa & Sản phẩm từ sữa",
                    Description = "Sữa tươi, sữa chua, phô mai"
                },

                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Mì gói & Thực phẩm ăn liền",
                    Description = "Mì ăn liền, phở khô, cháo gói"
                },

                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Gia vị & Dầu ăn",
                    Description = "Nước mắm, hạt nêm, dầu thực vật"
                }
            );
            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901122334",
                    MembershipRank = "Vàng",
                    RewardPoints = 150
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0918877665",
                    MembershipRank = "Bạc",
                    RewardPoints = 50
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0983344556",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10
                }
            );
        }
    }
}