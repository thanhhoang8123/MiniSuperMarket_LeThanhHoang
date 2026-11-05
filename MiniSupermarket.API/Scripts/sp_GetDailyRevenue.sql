-- =====================================================================
-- STORED PROCEDURE: BÁO CÁO DOANH THU THEO KHOẢNG THỜI GIAN (TỪ NGÀY -> ĐẾN NGÀY)
-- Gom nhóm theo ngày sử dụng GROUP BY CAST(CreatedDate AS DATE)
-- =====================================================================
USE [ThanhHoang_MiniSupermarketDb];
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetDailyRevenue
    @FromDate DATE,
    @ToDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CAST(CreatedDate AS DATE) AS [Date],
        COUNT(OrderId) AS [TotalOrders],
        ISNULL(SUM(FinalAmount), 0) AS [TotalRevenue]
    FROM dbo.Orders
    WHERE CAST(CreatedDate AS DATE) >= @FromDate 
      AND CAST(CreatedDate AS DATE) <= @ToDate
    GROUP BY CAST(CreatedDate AS DATE)
    ORDER BY [Date] ASC;
END
GO
