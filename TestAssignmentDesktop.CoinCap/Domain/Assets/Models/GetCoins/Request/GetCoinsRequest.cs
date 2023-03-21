namespace TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;

public class GetCoinsRequest
{
    public GetCoinsRequest(string search, int limit, byte offset)
    {
        Search = search;
        Limit = limit;
        Offset = offset;
    }

    public string Search { get; set; } 

    public int Limit { get; set; }

    public byte Offset { get; set; }

    public IDictionary<string, string> ToQueryParametersDictionary()
    {
        var parameters = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("search", Search),
            new KeyValuePair<string, string>("limit", Limit.ToString() ?? "2000"),
            new KeyValuePair<string, string>("offset", Offset.ToString() ?? string.Empty)
        };

        return new Dictionary<string, string>(parameters.Where(p => !string.IsNullOrWhiteSpace(p.Value)));
    }

}