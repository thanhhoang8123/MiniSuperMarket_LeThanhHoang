using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        // ==============================
        // CÁC BẢNG
        // ==============================

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }


        // ==============================
        // DATA SEEDING
        // ==============================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ==================================================
            // 1. CATEGORY - 15 NHÓM HÀNG
            // ==================================================

            modelBuilder.Entity<Category>().HasData(

                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Nước giải khát",
                    Description = "Nước ngọt, nước tăng lực, nước khoáng"
                },

                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Sữa & sản phẩm từ sữa",
                    Description = "Sữa tươi, sữa hộp, sữa chua"
                },

                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Bánh kẹo",
                    Description = "Bánh quy, kẹo, socola"
                },

                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Đồ ăn vặt",
                    Description = "Snack, rong biển, đồ ăn nhẹ"
                },

                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Mì & thực phẩm ăn liền",
                    Description = "Mì gói, phở, cháo ăn liền"
                },

                new Category
                {
                    CategoryId = 6,
                    CategoryName = "Gạo & thực phẩm khô",
                    Description = "Gạo, đậu, các loại hạt"
                },

                new Category
                {
                    CategoryId = 7,
                    CategoryName = "Gia vị & dầu ăn",
                    Description = "Nước mắm, nước tương, dầu ăn"
                },

                new Category
                {
                    CategoryId = 8,
                    CategoryName = "Thực phẩm đóng hộp",
                    Description = "Cá hộp, thịt hộp, pate"
                },

                new Category
                {
                    CategoryId = 9,
                    CategoryName = "Đồ uống dinh dưỡng",
                    Description = "Sữa hạt, nước yến, thức uống dinh dưỡng"
                },

                new Category
                {
                    CategoryId = 10,
                    CategoryName = "Cà phê & trà",
                    Description = "Cà phê hòa tan, cà phê gói, trà"
                },

                new Category
                {
                    CategoryId = 11,
                    CategoryName = "Hóa mỹ phẩm",
                    Description = "Dầu gội, sữa tắm, sản phẩm chăm sóc cơ thể"
                },

                new Category
                {
                    CategoryId = 12,
                    CategoryName = "Chăm sóc cá nhân",
                    Description = "Kem đánh răng, khăn giấy, khẩu trang"
                },

                new Category
                {
                    CategoryId = 13,
                    CategoryName = "Đồ dùng gia đình",
                    Description = "Nước rửa chén, túi rác, đồ dùng gia đình"
                },

                new Category
                {
                    CategoryId = 14,
                    CategoryName = "Văn phòng phẩm",
                    Description = "Bút, vở, dụng cụ học tập"
                },

                new Category
                {
                    CategoryId = 15,
                    CategoryName = "Đồ dùng tiện ích",
                    Description = "Pin, bật lửa và các vật dụng tiện ích"
                }
            );


            // ==================================================
            // 2. PRODUCT - 15 SẢN PHẨM
            // ==================================================

            modelBuilder.Entity<Product>().HasData(

                new Product
                {
                    ProductId = 1,
                    Barcode = "8938505970011",
                    ProductName = "Nước suối Aquafina 500ml",
                    Price = 6000,
                    StockQuantity = 120,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 2,
                    Barcode = "8935049500028",
                    ProductName = "Coca Cola lon 330ml",
                    Price = 10000,
                    StockQuantity = 100,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 3,
                    Barcode = "8934673700035",
                    ProductName = "Sữa tươi Vinamilk 180ml",
                    Price = 7500,
                    StockQuantity = 80,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 4,
                    Barcode = "8934680030042",
                    ProductName = "Bánh Oreo vị chocolate 133g",
                    Price = 22000,
                    StockQuantity = 50,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 5,
                    Barcode = "8934572900059",
                    ProductName = "Snack khoai tây Lay's 95g",
                    Price = 18000,
                    StockQuantity = 70,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 6,
                    Barcode = "8934563150066",
                    ProductName = "Mì Hảo Hảo tôm chua cay",
                    Price = 4500,
                    StockQuantity = 150,
                    CategoryId = 5
                },

                new Product
                {
                    ProductId = 7,
                    Barcode = "8938505970073",
                    ProductName = "Gạo ST25 túi 5kg",
                    Price = 145000,
                    StockQuantity = 30,
                    CategoryId = 6
                },

                new Product
                {
                    ProductId = 8,
                    Barcode = "8934588010080",
                    ProductName = "Nước mắm Nam Ngư 500ml",
                    Price = 32000,
                    StockQuantity = 45,
                    CategoryId = 7
                },

                new Product
                {
                    ProductId = 9,
                    Barcode = "8934564700097",
                    ProductName = "Cá ngừ đóng hộp 170g",
                    Price = 28000,
                    StockQuantity = 35,
                    CategoryId = 8
                },

                new Product
                {
                    ProductId = 10,
                    Barcode = "8934673200103",
                    ProductName = "Cà phê hòa tan G7 3in1",
                    Price = 42000,
                    StockQuantity = 60,
                    CategoryId = 10
                },

                new Product
                {
                    ProductId = 11,
                    Barcode = "8935217100110",
                    ProductName = "Dầu gội Sunsilk 650g",
                    Price = 89000,
                    StockQuantity = 25,
                    CategoryId = 11
                },

                new Product
                {
                    ProductId = 12,
                    Barcode = "8935001700127",
                    ProductName = "Kem đánh răng P/S 180g",
                    Price = 38000,
                    StockQuantity = 40,
                    CategoryId = 12
                },

                new Product
                {
                    ProductId = 13,
                    Barcode = "8934868000134",
                    ProductName = "Nước rửa chén Sunlight 750ml",
                    Price = 35000,
                    StockQuantity = 55,
                    CategoryId = 13
                },

                new Product
                {
                    ProductId = 14,
                    Barcode = "8938501230141",
                    ProductName = "Vở học sinh 200 trang",
                    Price = 18000,
                    StockQuantity = 40,
                    CategoryId = 14
                },

                new Product
                {
                    ProductId = 15,
                    Barcode = "8936001250158",
                    ProductName = "Pin AA Panasonic 4 viên",
                    Price = 45000,
                    StockQuantity = 20,
                    CategoryId = 15
                }
            );


            // ==================================================
            // 3. CUSTOMER - 15 KHÁCH HÀNG
            // ==================================================

            modelBuilder.Entity<Customer>().HasData(

                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn An",
                    PhoneNumber = "0901122334",
                    Address = "Quận 10, TP.HCM",
                    RewardPoints = 850,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0918877665",
                    Address = "Quận 3, TP.HCM",
                    RewardPoints = 520,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0983344556",
                    Address = "Quận 5, TP.HCM",
                    RewardPoints = 120,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0934567812",
                    Address = "Quận 1, TP.HCM",
                    RewardPoints = 1250,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Minh Đức",
                    PhoneNumber = "0978123456",
                    Address = "Quận 11, TP.HCM",
                    RewardPoints = 320,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0908765432",
                    Address = "Tân Bình, TP.HCM",
                    RewardPoints = 90,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Quốc Huy",
                    PhoneNumber = "0912345678",
                    Address = "Bình Thạnh, TP.HCM",
                    RewardPoints = 680,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Ngọc Lan",
                    PhoneNumber = "0987654321",
                    Address = "Gò Vấp, TP.HCM",
                    RewardPoints = 1580,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Nguyễn Thị Mai",
                    PhoneNumber = "0945678123",
                    Address = "Phú Nhuận, TP.HCM",
                    RewardPoints = 210,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Trương Minh Nam",
                    PhoneNumber = "0961234789",
                    Address = "Tân Phú, TP.HCM",
                    RewardPoints = 760,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Phan Thị Oanh",
                    PhoneNumber = "0903456789",
                    Address = "Quận 6, TP.HCM",
                    RewardPoints = 1800,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Đỗ Hoàng Phúc",
                    PhoneNumber = "0973456128",
                    Address = "Quận 7, TP.HCM",
                    RewardPoints = 60,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Lý Minh Quân",
                    PhoneNumber = "0937894561",
                    Address = "Thủ Đức, TP.HCM",
                    RewardPoints = 430,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Nguyễn Ngọc Thảo",
                    PhoneNumber = "0915678234",
                    Address = "Bình Tân, TP.HCM",
                    RewardPoints = 1020,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Trần Minh Tú",
                    PhoneNumber = "0981234567",
                    Address = "Quận 12, TP.HCM",
                    RewardPoints = 150,
                    MembershipRank = "Chuẩn"
                }
            );
        }
    }
}