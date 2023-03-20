using Newtonsoft.Json;

namespace TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Response;

public class CoinsResponse
{
    [JsonProperty("data")]
    public List<Coins> Data { get; set; }
}