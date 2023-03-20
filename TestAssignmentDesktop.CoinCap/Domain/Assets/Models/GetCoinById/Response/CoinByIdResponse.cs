using Newtonsoft.Json;

namespace TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Response;

public class CoinByIdResponse
{
    [JsonProperty("data")]
    public CoinById Data { get; set; }
}