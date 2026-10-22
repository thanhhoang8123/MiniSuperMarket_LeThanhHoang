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
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


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

            modelBuilder.Entity<Order>()
    .HasOne(o => o.Customer)
    .WithMany()
    .HasForeignKey(o => o.CustomerId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
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
                    Address = "15 Đường Thành Thái, Phường 12, Quận 10, TP.HCM",
                    RewardPoints = 850,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị Bình",
                    PhoneNumber = "0918877665",
                    Address = "28 Đường Võ Văn Tần, Phường 6, Quận 3, TP.HCM",
                    RewardPoints = 520,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn Cường",
                    PhoneNumber = "0983344556",
                    Address = "42 Đường Nguyễn Trãi, Phường 3, Quận 5, TP.HCM",
                    RewardPoints = 120,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị Dung",
                    PhoneNumber = "0934567812",
                    Address = "56 Đường Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM",
                    RewardPoints = 1250,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Minh Đức",
                    PhoneNumber = "0978123456",
                    Address = "73 Đường Lãnh Binh Thăng, Phường 12, Quận 11, TP.HCM",
                    RewardPoints = 320,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị Hạnh",
                    PhoneNumber = "0908765432",
                    Address = "91 Đường Cộng Hòa, Phường 4, Quận Tân Bình, TP.HCM",
                    RewardPoints = 90,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Quốc Huy",
                    PhoneNumber = "0912345678",
                    Address = "36 Đường Xô Viết Nghệ Tĩnh, Phường 21, Quận Bình Thạnh, TP.HCM",
                    RewardPoints = 680,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Ngọc Lan",
                    PhoneNumber = "0987654321",
                    Address = "108 Đường Quang Trung, Phường 10, Quận Gò Vấp, TP.HCM",
                    RewardPoints = 1580,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Nguyễn Thị Mai",
                    PhoneNumber = "0945678123",
                    Address = "64 Đường Phan Đình Phùng, Phường 2, Quận Phú Nhuận, TP.HCM",
                    RewardPoints = 210,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Trương Minh Nam",
                    PhoneNumber = "0961234789",
                    Address = "125 Đường Lê Trọng Tấn, Phường Sơn Kỳ, Quận Tân Phú, TP.HCM",
                    RewardPoints = 760,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Phan Thị Oanh",
                    PhoneNumber = "0903456789",
                    Address = "39 Đường Hậu Giang, Phường 5, Quận 6, TP.HCM",
                    RewardPoints = 1800,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Đỗ Hoàng Phúc",
                    PhoneNumber = "0973456128",
                    Address = "82 Đường Nguyễn Thị Thập, Phường Tân Quy, Quận 7, TP.HCM",
                    RewardPoints = 60,
                    MembershipRank = "Chuẩn"
                },

                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Lý Minh Quân",
                    PhoneNumber = "0937894561",
                    Address = "47 Đường Võ Văn Ngân, Phường Linh Chiểu, TP. Thủ Đức, TP.HCM",
                    RewardPoints = 430,
                    MembershipRank = "Bạc"
                },

                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Nguyễn Ngọc Thảo",
                    PhoneNumber = "0915678234",
                    Address = "116 Đường Tên Lửa, Phường Bình Trị Đông B, Quận Bình Tân, TP.HCM",
                    RewardPoints = 1020,
                    MembershipRank = "Vàng"
                },

                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Trần Minh Tú",
                    PhoneNumber = "0981234567",
                    Address = "53 Đường Nguyễn Văn Quá, Phường Đông Hưng Thuận, Quận 12, TP.HCM",
                    RewardPoints = 150,
                    MembershipRank = "Chuẩn"
                }
            );
            // ==================================================
            // 4. USER - 15 TÀI KHOẢN PHÂN QUYỀN
            // ==================================================

            modelBuilder.Entity<User>().HasData(

                new User
                {
                    UserId = 1,
                    Username = "admin01",
                    PasswordHash = "123456",
                    FullName = "Nguyễn Quản Trị",
                    Role = "Admin",
                    IsActive = true
                },

                new User
                {
                    UserId = 2,
                    Username = "admin02",
                    PasswordHash = "123456",
                    FullName = "Trần Giám Đốc",
                    Role = "Admin",
                    IsActive = true
                },

                new User
                {
                    UserId = 3,
                    Username = "cashier01",
                    PasswordHash = "123456",
                    FullName = "Lê Thu Ngân",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 4,
                    Username = "cashier02",
                    PasswordHash = "123456",
                    FullName = "Phạm Bán Hàng",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 5,
                    Username = "cashier03",
                    PasswordHash = "123456",
                    FullName = "Hoàng Thu Ngân",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 6,
                    Username = "cashier04",
                    PasswordHash = "123456",
                    FullName = "Vũ Thị Quầy",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 7,
                    Username = "cashier05",
                    PasswordHash = "123456",
                    FullName = "Đỗ Bán Lẻ",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 8,
                    Username = "ware01",
                    PasswordHash = "123456",
                    FullName = "Ngô Quản Kho",
                    Role = "Warehouse",
                    IsActive = true
                },

                new User
                {
                    UserId = 9,
                    Username = "ware02",
                    PasswordHash = "123456",
                    FullName = "Bùi Kiểm Kê",
                    Role = "Warehouse",
                    IsActive = true
                },

                new User
                {
                    UserId = 10,
                    Username = "ware03",
                    PasswordHash = "123456",
                    FullName = "Dương Thủ Kho",
                    Role = "Warehouse",
                    IsActive = true
                },

                new User
                {
                    UserId = 11,
                    Username = "ware04",
                    PasswordHash = "123456",
                    FullName = "Lý Nhập Hàng",
                    Role = "Warehouse",
                    IsActive = true
                },

                new User
                {
                    UserId = 12,
                    Username = "admin_backup",
                    PasswordHash = "123456",
                    FullName = "Đặng Hỗ Trợ",
                    Role = "Admin",
                    IsActive = true
                },

                new User
                {
                    UserId = 13,
                    Username = "cashier06",
                    PasswordHash = "123456",
                    FullName = "Hồ Ca Chiều",
                    Role = "Cashier",
                    IsActive = true
                },

                new User
                {
                    UserId = 14,
                    Username = "ware05",
                    PasswordHash = "123456",
                    FullName = "Trương Vận Chuyển",
                    Role = "Warehouse",
                    IsActive = true
                },

                new User
                {
                    UserId = 15,
                    Username = "supervisor",
                    PasswordHash = "123456",
                    FullName = "Mai Giám Sát",
                    Role = "Admin",
                    IsActive = true
                }
            );
            // ==================================================
            // 5. ORDER - 15 HÓA ĐƠN MẪU TẠP HÓA PHỐ THỊ
            // ==================================================

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderCode = "PT-SEED-20261015-001", CreatedDate = new DateTime(2026, 10, 10, 8, 15, 0), CashierUsername = "cashier01", CustomerId = 1, TotalItems = 9, FinalAmount = 54500m, RewardPoints = 2 },
                new Order { OrderId = 2, OrderCode = "PT-SEED-20261015-002", CreatedDate = new DateTime(2026, 10, 10, 9, 20, 0), CashierUsername = "cashier02", CustomerId = 2, TotalItems = 5, FinalAmount = 52000m, RewardPoints = 1 },
                new Order { OrderId = 3, OrderCode = "PT-SEED-20261015-003", CreatedDate = new DateTime(2026, 10, 10, 10, 35, 0), CashierUsername = "cashier03", CustomerId = 3, TotalItems = 12, FinalAmount = 81000m, RewardPoints = 1 },
                new Order { OrderId = 4, OrderCode = "PT-SEED-20261015-004", CreatedDate = new DateTime(2026, 10, 11, 8, 45, 0), CashierUsername = "cashier01", CustomerId = 4, TotalItems = 3, FinalAmount = 209000m, RewardPoints = 3 },
                new Order { OrderId = 5, OrderCode = "PT-SEED-20261015-005", CreatedDate = new DateTime(2026, 10, 11, 11, 10, 0), CashierUsername = "cashier04", CustomerId = 5, TotalItems = 6, FinalAmount = 96000m, RewardPoints = 2 },
                new Order { OrderId = 6, OrderCode = "PT-SEED-20261015-006", CreatedDate = new DateTime(2026, 10, 11, 14, 30, 0), CashierUsername = "cashier05", CustomerId = 6, TotalItems = 4, FinalAmount = 60000m, RewardPoints = 1 },
                new Order { OrderId = 7, OrderCode = "PT-SEED-20261015-007", CreatedDate = new DateTime(2026, 10, 12, 9, 0, 0), CashierUsername = "cashier02", CustomerId = 7, TotalItems = 9, FinalAmount = 102500m, RewardPoints = 2 },
                new Order { OrderId = 8, OrderCode = "PT-SEED-20261015-008", CreatedDate = new DateTime(2026, 10, 12, 10, 50, 0), CashierUsername = "cashier06", CustomerId = 8, TotalItems = 5, FinalAmount = 175000m, RewardPoints = 3 },
                new Order { OrderId = 9, OrderCode = "PT-SEED-20261015-009", CreatedDate = new DateTime(2026, 10, 12, 15, 25, 0), CashierUsername = "cashier03", CustomerId = 9, TotalItems = 3, FinalAmount = 92000m, RewardPoints = 1 },
                new Order { OrderId = 10, OrderCode = "PT-SEED-20261015-010", CreatedDate = new DateTime(2026, 10, 13, 8, 20, 0), CashierUsername = "cashier04", CustomerId = 10, TotalItems = 3, FinalAmount = 173000m, RewardPoints = 2 },
                new Order { OrderId = 11, OrderCode = "PT-SEED-20261015-011", CreatedDate = new DateTime(2026, 10, 13, 10, 15, 0), CashierUsername = "cashier01", CustomerId = 11, TotalItems = 4, FinalAmount = 146000m, RewardPoints = 3 },
                new Order { OrderId = 12, OrderCode = "PT-SEED-20261015-012", CreatedDate = new DateTime(2026, 10, 13, 13, 40, 0), CashierUsername = "cashier05", CustomerId = 12, TotalItems = 13, FinalAmount = 99000m, RewardPoints = 1 },
                new Order { OrderId = 13, OrderCode = "PT-SEED-20261015-013", CreatedDate = new DateTime(2026, 10, 14, 9, 30, 0), CashierUsername = "cashier06", CustomerId = 13, TotalItems = 7, FinalAmount = 120000m, RewardPoints = 2 },
                new Order { OrderId = 14, OrderCode = "PT-SEED-20261015-014", CreatedDate = new DateTime(2026, 10, 14, 14, 10, 0), CashierUsername = "cashier02", CustomerId = 14, TotalItems = 6, FinalAmount = 88000m, RewardPoints = 3 },
                new Order { OrderId = 15, OrderCode = "PT-SEED-20261015-015", CreatedDate = new DateTime(2026, 10, 15, 10, 0, 0), CashierUsername = "cashier03", CustomerId = 15, TotalItems = 7, FinalAmount = 87000m, RewardPoints = 1 }
            );

            // ==================================================
            // 6. ORDER DETAIL - 33 DÒNG CHI TIẾT HÓA ĐƠN
            // ==================================================

            modelBuilder.Entity<OrderDetail>().HasData(
                // Hóa đơn 1: 54.500đ
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 6000m, LineTotal = 12000m },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 2, Quantity = 2, UnitPrice = 10000m, LineTotal = 20000m },
                new OrderDetail { OrderDetailId = 3, OrderId = 1, ProductId = 6, Quantity = 5, UnitPrice = 4500m, LineTotal = 22500m },

                // Hóa đơn 2: 52.000đ
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 3, Quantity = 4, UnitPrice = 7500m, LineTotal = 30000m },
                new OrderDetail { OrderDetailId = 5, OrderId = 2, ProductId = 4, Quantity = 1, UnitPrice = 22000m, LineTotal = 22000m },

                // Hóa đơn 3: 81.000đ
                new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 5, Quantity = 2, UnitPrice = 18000m, LineTotal = 36000m },
                new OrderDetail { OrderDetailId = 7, OrderId = 3, ProductId = 6, Quantity = 10, UnitPrice = 4500m, LineTotal = 45000m },

                // Hóa đơn 4: 209.000đ
                new OrderDetail { OrderDetailId = 8, OrderId = 4, ProductId = 7, Quantity = 1, UnitPrice = 145000m, LineTotal = 145000m },
                new OrderDetail { OrderDetailId = 9, OrderId = 4, ProductId = 8, Quantity = 2, UnitPrice = 32000m, LineTotal = 64000m },

                // Hóa đơn 5: 96.000đ
                new OrderDetail { OrderDetailId = 10, OrderId = 5, ProductId = 2, Quantity = 4, UnitPrice = 10000m, LineTotal = 40000m },
                new OrderDetail { OrderDetailId = 11, OrderId = 5, ProductId = 9, Quantity = 2, UnitPrice = 28000m, LineTotal = 56000m },

                // Hóa đơn 6: 60.000đ
                new OrderDetail { OrderDetailId = 12, OrderId = 6, ProductId = 1, Quantity = 3, UnitPrice = 6000m, LineTotal = 18000m },
                new OrderDetail { OrderDetailId = 13, OrderId = 6, ProductId = 10, Quantity = 1, UnitPrice = 42000m, LineTotal = 42000m },

                // Hóa đơn 7: 102.500đ
                new OrderDetail { OrderDetailId = 14, OrderId = 7, ProductId = 4, Quantity = 2, UnitPrice = 22000m, LineTotal = 44000m },
                new OrderDetail { OrderDetailId = 15, OrderId = 7, ProductId = 5, Quantity = 2, UnitPrice = 18000m, LineTotal = 36000m },
                new OrderDetail { OrderDetailId = 16, OrderId = 7, ProductId = 6, Quantity = 5, UnitPrice = 4500m, LineTotal = 22500m },

                // Hóa đơn 8: 175.000đ
                new OrderDetail { OrderDetailId = 17, OrderId = 8, ProductId = 7, Quantity = 1, UnitPrice = 145000m, LineTotal = 145000m },
                new OrderDetail { OrderDetailId = 18, OrderId = 8, ProductId = 3, Quantity = 4, UnitPrice = 7500m, LineTotal = 30000m },

                // Hóa đơn 9: 92.000đ
                new OrderDetail { OrderDetailId = 19, OrderId = 9, ProductId = 8, Quantity = 2, UnitPrice = 32000m, LineTotal = 64000m },
                new OrderDetail { OrderDetailId = 20, OrderId = 9, ProductId = 9, Quantity = 1, UnitPrice = 28000m, LineTotal = 28000m },

                // Hóa đơn 10: 173.000đ
                new OrderDetail { OrderDetailId = 21, OrderId = 10, ProductId = 10, Quantity = 2, UnitPrice = 42000m, LineTotal = 84000m },
                new OrderDetail { OrderDetailId = 22, OrderId = 10, ProductId = 11, Quantity = 1, UnitPrice = 89000m, LineTotal = 89000m },

                // Hóa đơn 11: 146.000đ
                new OrderDetail { OrderDetailId = 23, OrderId = 11, ProductId = 12, Quantity = 2, UnitPrice = 38000m, LineTotal = 76000m },
                new OrderDetail { OrderDetailId = 24, OrderId = 11, ProductId = 13, Quantity = 2, UnitPrice = 35000m, LineTotal = 70000m },

                // Hóa đơn 12: 99.000đ
                new OrderDetail { OrderDetailId = 25, OrderId = 12, ProductId = 14, Quantity = 3, UnitPrice = 18000m, LineTotal = 54000m },
                new OrderDetail { OrderDetailId = 26, OrderId = 12, ProductId = 6, Quantity = 10, UnitPrice = 4500m, LineTotal = 45000m },

                // Hóa đơn 13: 120.000đ
                new OrderDetail { OrderDetailId = 27, OrderId = 13, ProductId = 15, Quantity = 2, UnitPrice = 45000m, LineTotal = 90000m },
                new OrderDetail { OrderDetailId = 28, OrderId = 13, ProductId = 1, Quantity = 5, UnitPrice = 6000m, LineTotal = 30000m },

                // Hóa đơn 14: 88.000đ
                new OrderDetail { OrderDetailId = 29, OrderId = 14, ProductId = 2, Quantity = 3, UnitPrice = 10000m, LineTotal = 30000m },
                new OrderDetail { OrderDetailId = 30, OrderId = 14, ProductId = 4, Quantity = 1, UnitPrice = 22000m, LineTotal = 22000m },
                new OrderDetail { OrderDetailId = 31, OrderId = 14, ProductId = 5, Quantity = 2, UnitPrice = 18000m, LineTotal = 36000m },

                // Hóa đơn 15: 87.000đ
                new OrderDetail { OrderDetailId = 32, OrderId = 15, ProductId = 3, Quantity = 6, UnitPrice = 7500m, LineTotal = 45000m },
                new OrderDetail { OrderDetailId = 33, OrderId = 15, ProductId = 10, Quantity = 1, UnitPrice = 42000m, LineTotal = 42000m }
            );
        }
    }
}