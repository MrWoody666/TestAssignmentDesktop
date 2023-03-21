using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoinById.Response;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Request;
using TestAssignmentDesktop.CoinCap.Domain.Assets.Models.GetCoins.Response;

namespace TestAssignmentDesktop.CoinCap.Client;

public interface ICoinCapClient
{
    public Task<List<Coins>> GetAssetsAsync(GetCoinsRequest request);

    public Task<CoinByIdResponse> GetAssetByIdAsync(GetCoinByIdRequest request);
}