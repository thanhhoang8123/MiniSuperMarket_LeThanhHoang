namespace MiniSupermarket.API.DTOs
{
    // DTO bao bọc kết quả phân trang
    public class PagedResult<T>
    {
        public int CurrentPage { get; set; }       // Trang hiện tại (1, 2, 3...)
        public int PageSize { get; set; }          // Số phần tử trên mỗi trang (ví dụ: 5 dòng)
        public int TotalItems { get; set; }         // Tổng số sản phẩm thỏa mãn điều kiện
        public int TotalPages { get; set; }         // Tổng số trang = Ceiling(TotalItems / PageSize)
        public List<T> Items { get; set; } = new(); // Danh sách dữ liệu của trang hiện hành
    }
}

