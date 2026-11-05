-- ============================================================================
-- THỦ TỤC LƯU TRỮ (STORED PROCEDURE): sp_GetRevenueByCashier
-- MỤC ĐÍCH: Tính tổng số hóa đơn và tổng doanh thu theo từng thu ngân
-- ============================================================================

USE [ThanhHoang_MiniSupermarketDb];
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        CashierUsername,
        COUNT(OrderId) AS TotalOrders,
        ISNULL(SUM(FinalAmount), 0) AS TotalRevenue
    FROM dbo.Orders
    WHERE CashierUsername IS NOT NULL AND LTRIM(RTRIM(CashierUsername)) <> ''
    GROUP BY CashierUsername
    ORDER BY TotalRevenue DESC;
END;
GO
