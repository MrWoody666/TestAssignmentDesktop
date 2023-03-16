using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Text.Json;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Response;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Response;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestAssignmentDesktop.CoinCap.Client;

public class CoinCapClient : ICoinCapClient
{
    private const string BaseUri = "https://api.coincap.io/v2/";

    private string ApiKey;

    private readonly HttpClient _httpClient;

    public CoinCapClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        ApiKey = apiKey;
    }

    public async Task<List<CoinsResponse>> AssetsAsync(GetCoinsRequest request)
    {
        if(request is null)
            throw new ArgumentNullException(nameof(request));

        var parametersDictionary = request.ToQueryParametersDictionary();

        var response = await _httpClient.GetAsync(BuildUrlQuery("assets", parametersDictionary));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var coinsResponse = JsonSerializer.Deserialize<JsonElement>(responseBody)
            .GetProperty("data")
            .EnumerateArray()
            .Select(e => JsonSerializer.Deserialize<CoinsResponse>(e.GetRawText()))
            .ToList();

        return coinsResponse;
    }

    public async Task<CoinByIdResponse> AssetsByIdAsync(GetCoinByIdRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var parametersDictionary = request.ToQueryParametersDictionary();

        var response = await _httpClient.GetAsync(BuildUrlQuery($"assets/{request.Id}", parametersDictionary));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var coinByIdResponse = JsonSerializer.Deserialize<JsonElement>(responseBody)
            .GetProperty("data")
            .EnumerateArray()
            .Select(e => JsonSerializer.Deserialize<CoinByIdResponse>(e.GetRawText()))
            .FirstOrDefault();

        return coinByIdResponse;
    }

    private string BuildUrlQuery(string endpoint, IDictionary<string, string> queryParameters)
    {
        queryParameters.Add("key", ApiKey);

        return QueryHelpers.AddQueryString(BaseUri + endpoint, queryParameters);
    }
}