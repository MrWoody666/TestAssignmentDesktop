namespace TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;

public class GetCoinsRequest
{
    public string SearchQuery { get; set; }

    public string SearchIds { get; set; }

    public int Limit { get; set; }

    public byte Offset { get; set; }

    public IDictionary<string, string> ToQueryParametersDictionary()
    {
        var parameters = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("q", SearchQuery),
            new KeyValuePair<string, string>("ids", SearchIds),
            new KeyValuePair<string, string>("limit", Limit.ToString() ?? "2000"),
            new KeyValuePair<string, string>("offset", Offset.ToString() ?? string.Empty)
        };

        return new Dictionary<string, string>(parameters.Where(p => !string.IsNullOrWhiteSpace(p.Value)));
    }

}