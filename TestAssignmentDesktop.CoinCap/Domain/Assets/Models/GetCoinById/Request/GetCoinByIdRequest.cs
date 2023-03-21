namespace TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;

public class GetCoinByIdRequest
{
    public GetCoinByIdRequest(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    public IDictionary<string, string> ToQueryParametersDictionary()
    {
        var parameters = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("id", Id)
        };

        return new Dictionary<string, string>(parameters.Where(p => !string.IsNullOrWhiteSpace(p.Value)));
    }
}