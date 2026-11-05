using System.Text.Json.Serialization;

namespace MiniSupermarket.WinForms.DTOs
{
    public class CustomerDto
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("rewardPoints")]
        public int RewardPoints { get; set; }

        [JsonPropertyName("membershipRank")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }

    public class CustomerByPhoneResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public CustomerDto? Data { get; set; }
    }
}
