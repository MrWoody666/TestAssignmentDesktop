using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Response;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Response;

namespace TestAssignmentDesktop.CoinCap.Client;

public class CoinCapClient : ICoinCapClient
{
    private const string BaseUri = "https://api.coincap.io/v2/";

    private readonly HttpClient _httpClient;

    public CoinCapClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Coins>> GetAssetsAsync(GetCoinsRequest request)
    {
        if(request is null)
            throw new ArgumentNullException(nameof(request));

        var parametersDictionary = request.ToQueryParametersDictionary();

        var response = await _httpClient.GetAsync(BuildUrlQuery("assets", parametersDictionary));
        
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var coinsResponse = JsonConvert.DeserializeObject<CoinsResponse>(responseBody);

        return coinsResponse.Data;
    }

    public async Task<CoinByIdResponse> GetAssetByIdAsync(GetCoinByIdRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var parametersDictionary = request.ToQueryParametersDictionary();

        var response = await _httpClient.GetAsync(BuildUrlQuery($"assets/{request.Id}", parametersDictionary));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var coinByIdResponse = JsonConvert.DeserializeObject<CoinByIdResponse>(responseBody);

        return coinByIdResponse;
    }

    private string BuildUrlQuery(string endpoint, IDictionary<string, string> queryParameters)
    {
        
        return QueryHelpers.AddQueryString(BaseUri + endpoint, queryParameters);
    }
}